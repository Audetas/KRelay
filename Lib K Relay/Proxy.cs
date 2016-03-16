using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace Lib_K_Relay
{
    public delegate void ListenHandler(Proxy proxy);
    public delegate void ConnectionHandler(Client client);
    public delegate void PacketHandler(Client client, Packet packet);
    public delegate void GenericPacketHandler<T>(Client client, T packet) where T : Packet;
    public delegate void CommandHandler(Client client, string command, string[] args);

    public class Proxy
    {
        public event ListenHandler ProxyListenStarted;
        public event ListenHandler ProxyListenStopped;
        public event ConnectionHandler ClientBeginConnect;
        public event ConnectionHandler ClientConnected;
        public event ConnectionHandler ClientDisconnected;
        public event PacketHandler ServerPacketRecieved;
        public event PacketHandler ClientPacketRecieved;

        public static string DefaultServer = "54.241.208.233"; // USWest

        public Dictionary<string, State> States;

        private Dictionary<object, Type> _genericPacketHooks;
        private Dictionary<PacketHandler, List<PacketType>> _packetHooks;
        private Dictionary<CommandHandler, List<string>> _commandHooks;
        private TcpListener _localListener = null;

        public Proxy()
        {
            States = new Dictionary<string, State>();
            _genericPacketHooks = new Dictionary<object, Type>();
            _packetHooks = new Dictionary<PacketHandler, List<PacketType>>();
            _commandHooks = new Dictionary<CommandHandler, List<string>>();

            new StateManager().Attach(this);
            new ReconnectHandler().Attach(this);
        }

        public void Start()
        {
            Console.WriteLine("[Client Listener] Starting local listener...");

            bool success = PluginUtils.ProtectedInvoke(() =>
            {
                _localListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 2050);
                _localListener.Start();
                _localListener.BeginAcceptTcpClient(LocalConnect, null);
                Console.WriteLine("[Client Listener] Local listener started");
            }, "ClientListenerStart");

            if (!success) return;

            PluginUtils.ProtectedInvoke(() =>
            {
                ProxyListenStarted?.Invoke(this);
            }, "ProxyListenStarted");
        }

        public void Stop()
        {
            if (_localListener == null) return;

            Console.WriteLine("[Client Listener] Stopping local listener...");
            _localListener.Stop();

            PluginUtils.ProtectedInvoke(() =>
            {
                ProxyListenStopped?.Invoke(this);
            }, "ProxyListenStopped");
        }

        public State GetState(Client client, byte[] key)
        {
            string guid = key.Length == 0 ? "n/a" : Encoding.UTF8.GetString(key);

            State newState = new State(client, Guid.NewGuid().ToString("n"));
            States[newState.GUID] = newState;

            if (guid != "n/a")
            {
                State lastState = States[guid];
                newState.ConTargetAddress = lastState.ConTargetAddress;
                newState.ConTargetPort = lastState.ConTargetPort;
                newState.ConRealKey = lastState.ConRealKey;
            }

            return newState;
        }

        private void LocalConnect(IAsyncResult ar)
        {
            PluginUtils.ProtectedInvoke(() =>
            {
                TcpClient client = _localListener.EndAcceptTcpClient(ar);
                Client ci = new Client(this, client);
                Console.WriteLine("[Client Listener] Client recieved");

                PluginUtils.ProtectedInvoke(() =>
                {
                    ClientBeginConnect?.Invoke(ci);
                }, "ClientBeginConnect");

                ci.Connect();
            }, "LocalConnect");

            _localListener.BeginAcceptTcpClient(LocalConnect, null);
        }

        #region Hook Calls
        public void HookPacket(PacketType type, PacketHandler callback)
        {
            if (Serializer.GetPacketId(type) == 255)
                throw new InvalidOperationException("[Plugin Error] A plugin attempted to register callback " +
                                                    callback.GetMethodInfo().ReflectedType + "." + callback.Method.Name +
                                                    " for packet type " + type + " that doesn't have a structure defined.");
            else if (_packetHooks.ContainsKey(callback))
                _packetHooks[callback].Add(type);
            else
                _packetHooks.Add(callback, new List<PacketType>() { type });
        }

        public void HookPacket<T>(GenericPacketHandler<T> callback) where T : Packet
        {
            if (!_genericPacketHooks.ContainsKey(callback))
                _genericPacketHooks.Add(callback, typeof(T));
            else
                throw new InvalidOperationException("Callback already bound");
        }

        public void HookCommand(string command, CommandHandler callback)
        {
            if (_commandHooks.ContainsKey(callback))
                _commandHooks[callback].Add(command);
            else
                _commandHooks.Add(callback, new List<string>() { command[0] == '/' 
                    ? new string(command.Skip(1).ToArray()).ToLower() 
                    : command.ToLower() } );
        }
        #endregion

        #region Event Calls
        public void FireClientConnected(Client client)
        {
            PluginUtils.ProtectedInvoke(() =>
            {
                ClientConnected?.Invoke(client);
            }, "ClientConnected");
        }

        public void FireClientDisconnected(Client client)
        {
            PluginUtils.ProtectedInvoke(() =>
            {
                ClientDisconnected?.Invoke(client);
            }, "ClientDisconnected");
        }

        public void FireServerPacket(Client client, Packet packet)
        {
            PluginUtils.ProtectedInvoke(() =>
            {
                // Fire general server packet callbacks
                if (ServerPacketRecieved != null) ServerPacketRecieved(client, packet);

                // Fire specific hook callbacks if applicable
                foreach (var pair in _packetHooks)
                    if (pair.Value.Contains(packet.Type)) pair.Key(client, packet);

                foreach (var pair in _genericPacketHooks)
                    if (pair.Value == packet.GetType())
                        (pair.Key as Delegate).Method.Invoke((pair.Key as Delegate).Target, new object[2] { client, Convert.ChangeType(packet, pair.Value) });
            }, "ServerPacket");
        }

        public void FireClientPacket(Client client, Packet packet)
        {
            PluginUtils.ProtectedInvoke(() =>
            {
                // Fire command callbacks
                if (packet.Type == PacketType.PLAYERTEXT)
                {
                    PlayerTextPacket playerText = (PlayerTextPacket)packet;
                    string text = playerText.Text.Replace("/", "").ToLower();
                    string command = text.Contains(' ')
                                     ? text.Split(' ')[0].ToLower()
                                     : text;
                    string[] args = text.Contains(' ')
                                     ? text.Split(' ').Skip(1).ToArray()
                                     : new string[0];

                    foreach (var pair in _commandHooks)
                    {
                        if (pair.Value.Contains(command))
                        {
                            packet.Send = false;
                            pair.Key(client, command, args);
                        }
                    }
                }

                // Fire general client packet callbacks
                if (ClientPacketRecieved != null) ClientPacketRecieved(client, packet);

                // Fire specific hook callbacks if applicable
                foreach (var pair in _packetHooks)
                    if (pair.Value.Contains(packet.Type)) pair.Key(client, packet);

                foreach (var pair in _genericPacketHooks)
                    if (pair.Value == packet.GetType()) (pair.Key as Delegate).Method.Invoke((pair.Key as Delegate).Target, new object[2] { client, Convert.ChangeType(packet, pair.Value) });
            }, "ClientPacket");
        }

        #endregion
    }
}
