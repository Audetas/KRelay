using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib_K_Relay
{
    public class Proxy
    {
        public int Port = 2050;
        public string ListenAddress = "127.0.0.1";
        public string RemoteAddress = "54.241.208.233"; // USW
        public string Key0 = "311f80691451c71d09a13a2a6e";
        public string Key1 = "72c5583cafb6818995cdd74b80";

        public delegate void ListenHandler(Proxy proxy);
        public delegate void ConnectionHandler(Client client);
        public delegate void PacketHandler(Client client, Packet packet);
        public delegate void GenericPacketHandler<T>(Client client, T packet) where T : Packet;
        public delegate void CommandHandler(Client client, string command, string[] args);
        public delegate void KeyHandler(Keys key);

        public event ListenHandler ProxyListenStarted;
        public event ListenHandler ProxyListenStopped;
        public event ConnectionHandler ClientBeginConnect;
        public event ConnectionHandler ClientConnected;
        public event ConnectionHandler ClientDisconnected;
        public event PacketHandler ServerPacketRecieved;
        public event PacketHandler ClientPacketRecieved;

        private Dictionary<object, Type> _genericPacketHooks = new Dictionary<object, Type>();
        private Dictionary<PacketHandler, List<PacketType>> _packetHooks = new Dictionary<PacketHandler, List<PacketType>>();
        private Dictionary<CommandHandler, List<string>> _commandHooks = new Dictionary<CommandHandler, List<string>>();
        private Dictionary<KeyHandler, List<Keys>> _keyHooks = new Dictionary<KeyHandler, List<Keys>>();

        private TcpListener _localListener = null;

        /// <summary>
        /// Starts listening for clients on the defined port and host at 127.0.0.1:250
        /// </summary>
        public void Start()
        {
            Console.WriteLine("[Client Listener] Starting local listener at {0} on port {1}...",ListenAddress, Port);
            _localListener = new TcpListener(IPAddress.Parse(ListenAddress), Port);

            // Start listening for client connections.
            _localListener.Start();
            _localListener.BeginAcceptTcpClient(LocalConnect, null);
            HookManager.KeyUp += OnKeyUp;

            try
            {
                if (ProxyListenStarted != null) ProxyListenStarted(this);
            }
            catch (Exception e) { PluginUtils.LogPluginException(e, "ProxyListenStarted"); }
        }

        public void Stop()
        {
            HookManager.KeyUp -= OnKeyUp;
            if (_localListener != null && !_localListener.Server.Connected)
            {
                Console.WriteLine("[Client Listener] Stopping local listener...");
                _localListener.Stop();

                try
                {
                    if (ProxyListenStopped != null) ProxyListenStopped(this);
                }
                catch (Exception e) { PluginUtils.LogPluginException(e, "ProxyListenStopped"); }
            }
        }

        private void LocalConnect(IAsyncResult ar)
        {
            try
            {
                // Finish the accept, and then instantiate a ClientInstance
                // to begin handling IO on that socket and start its own 
                // connection to the server.
                TcpClient client = _localListener.EndAcceptTcpClient(ar);
                Client ci = new Client(this, client);

                try
                {
                    if (ClientBeginConnect != null) ClientBeginConnect(ci);
                }
                catch (Exception e) { PluginUtils.LogPluginException(e, "ClientBeginConnect"); }

                ci.Connect();

                // Listen for new clients.
                _localListener.BeginAcceptTcpClient(LocalConnect, null);
            }
            catch (ObjectDisposedException ignored) { } // This happens when the proxy stops and the callback fires. We'll ignore it.
            catch (Exception e) 
            {
                Console.WriteLine("[Client Listner] ClientListen failed! Here's the exception report:\n{0}", e.Message);
                Stop();
            }
        }

        public void HookPacket(PacketType type, PacketHandler callback)
        {
            if (Serializer.GetPacketId(type) == 255) // TODO: Remove when all structures are defined
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

        public void HookKey(Keys key, KeyHandler callback)
        {
            if (_keyHooks.ContainsKey(callback))
                _keyHooks[callback].Add(key);
            else
                _keyHooks.Add(callback, new List<Keys>() { key });
        }

        public void FireClientConnected(Client client)
        {
            try
            {
                if (ClientConnected != null) ClientConnected(client);
            }
            catch (Exception e) { PluginUtils.LogPluginException(e, "ClientConnected"); }
        }

        public void FireClientDisconnected(Client client)
        {
            try
            {
                if (ClientDisconnected != null) ClientDisconnected(client);
            }
            catch (Exception e) { PluginUtils.LogPluginException(e, "ClientDisconnected"); }
        }

        public void FireServerPacket(Client client, Packet packet)
        {
            try
            {
                // Fire general server packet callbacks
                if (ServerPacketRecieved != null) ServerPacketRecieved(client, packet);

                // Fire specific hook callbacks if applicable
                foreach (var pair in _packetHooks)
                    if (pair.Value.Contains(packet.Type)) pair.Key(client, packet);

                foreach (var pair in _genericPacketHooks)
                    if (pair.Value == packet.GetType())
                        (pair.Key as Delegate).Method.Invoke((pair.Key as Delegate).Target, new object[2] { client, Convert.ChangeType(packet, pair.Value) });
            }
            catch (Exception e) { PluginUtils.LogPluginException(e, "ServerPacket"); }
        }

        public void FireClientPacket(Client client, Packet packet)
        {
            try
            {
                // Fire command callbacks
                if (packet.Type == PacketType.PLAYERTEXT)
                {
                    PlayerTextPacket playerText = (PlayerTextPacket)packet;
                    string text = playerText.Text.Replace("/", "").ToLower();
                    string command = text.Contains(' ')
                                     ? text.Split(' ')[0].ToLower()
                                     : text;
                    string[] args =  text.Contains(' ')
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
            } 
            catch (Exception e) { PluginUtils.LogPluginException(e, "ClientPacket"); }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            foreach (var pair in _keyHooks)
                if (pair.Value.Contains(e.KeyCode)) 
                    pair.Key(e.KeyCode);
        }
    }
}
