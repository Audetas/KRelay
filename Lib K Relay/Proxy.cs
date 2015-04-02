using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay
{
    public class Proxy
    {
        public int Port = 2050;
        public string ListenAddress = "127.0.0.1";
        public string RemoteAddress = Servers["USW"];

        public event Action<Proxy> ProxyListenStarted;
        public event Action<Proxy> ProxyListenStopped;
        public event Action<ClientInstance> ClientConnected;
        public event Action<ClientInstance> ClientDisconnected;
        public event Action<ClientInstance, Packet> ServerPacketRecieved;
        public event Action<ClientInstance, Packet> ClientPacketRecieved;

        private List<Tuple<PacketType, Action<ClientInstance, Packet>>> _packetHooks = 
            new List<Tuple<PacketType, Action<ClientInstance, Packet>>>();

        private TcpListener _localListener = null;

        public static Dictionary<string, string> Servers = new Dictionary<string, string>() // TODO MOVE TO SETTINGS
        {
            {"USW", "54.241.208.233"},
            {"EUW", "54.195.57.43"},
            //{"USNW", "54.80.67.112"},
            {"USE", "54.224.68.81"},
            {"ASE", "54.225.15.39"},
            {"USS", "23.22.180.212"},
            {"USSW", "54.241.53.42"},
            {"EUE", "46.137.30.179"},
            {"EUN", "54.195.96.152"},
            {"EUSW", "54.218.63.70"},
            {"USE3", "54.226.214.216"},
            {"USW2", "54.193.168.4"},
            {"USMW2", "50.17.143.165"},
            {"USE2", "54.204.50.57"},
            {"USNW", "50.18.24.120"},
            {"AE", "175.41.201.80"},
            {"USS3", "54.80.250.47"},
            {"EUN2", "54.216.200.98"},
            {"EUW2", "54.216.27.65"},
            {"EUS", "54.195.179.215"},
            {"USS2", "50.19.7.133"},
            {"USW3", "54.241.223.240"}
        };

        public void Start()
        {
            Console.WriteLine("[Client Listener] Starting local listener at {0} on port {1}...",
                ListenAddress, Port);

            _localListener = new TcpListener(
                IPAddress.Parse(ListenAddress), Port);

            // Start listening for client connections.
            _localListener.Start();
            _localListener.BeginAcceptTcpClient(LocalConnect, null);

            try
            {
                if (ProxyListenStarted != null)
                    ProxyListenStarted(this);
            } catch (Exception e) { PrintPluginCallbackException("ProxyListenStarted", e); }
        }

        public void Stop()
        {
            Console.WriteLine("[Client Listener] Stopping local listener...");

            if (_localListener != null && !_localListener.Server.Connected)
                _localListener.Stop();

            try
            {
                if (ProxyListenStopped != null)
                    ProxyListenStopped(this);
            } catch (Exception e) { PrintPluginCallbackException("ProxyListenStopped", e); }
        }

        private void LocalConnect(IAsyncResult ar)
        {
            try
            {
                // Finish the accept, and then instantiate a ClientInstance
                // to begin handling IO on that socket and start its own 
                // connection to the server.
                TcpClient client = _localListener.EndAcceptTcpClient(ar);
                ClientInstance ci = new ClientInstance(this, client);

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

        public void HookPacket(PacketType type, Action<ClientInstance, Packet> callback)
        {
            /*
            if (Serializer.GetPacketId(type) == 255)
                throw new InvalidOperationException("[Plugin Error] A plugin attempted to register callback " +
                                                    callback.GetMethodInfo().ReflectedType + "." + callback.Method.Name +
                                                    " for packet type " + type + " that doesn't have a structure defined.");
            else*/
                _packetHooks.Add(new Tuple<PacketType, Action<ClientInstance, Packet>>(type, callback));
        }

        public void FireClientConnected(ClientInstance client)
        {
            try
            {
                if (ClientConnected != null)
                    ClientConnected(client);
            } catch (Exception e) { PrintPluginCallbackException("ClientConnected", e); }
        }

        public void FireClientDisconnected(ClientInstance client)
        {
            try
            {
                if (ClientDisconnected != null)
                    ClientDisconnected(client);
            } catch (Exception e) { PrintPluginCallbackException("ClientDisconnected", e); }
        }

        public void FireServerPacket(ClientInstance client, Packet packet)
        {
            try
            {
                // Fire specific hook callbacks if applicable
                foreach (var hook in _packetHooks)
                {
                    if (hook.Item1 == packet.Type && hook.Item2 != null)
                        hook.Item2(client, packet);
                    else if (hook.Item2 == null)
                        _packetHooks.Remove(hook);
                }

                // Fire general server packet callbacks
                if (ServerPacketRecieved != null)
                    ServerPacketRecieved(client, packet);
            } catch (Exception e) { PrintPluginCallbackException("ServerPacket", e); }
        }

        public void FireClientPacket(ClientInstance client, Packet packet)
        {
            try
            {
                // Fire specific hook callbacks if applicable
                foreach (var hook in _packetHooks)
                {
                    if (hook.Item1 == packet.Type && hook.Item2 != null)
                        hook.Item2(client, packet);
                    else if (hook.Item2 == null)
                        _packetHooks.Remove(hook);
                }

                // Fire general client packet callbacks
                if (ClientPacketRecieved != null)
                    ClientPacketRecieved(client, packet);
            } catch (Exception e) { PrintPluginCallbackException("ClientPacket", e); }
        }

        private void PrintPluginCallbackException(string caller, Exception e)
        {
            MethodBase site = e.TargetSite;
            string methodName = site == null ? "<null method reference>" : site.Name;
            string className = site == null ? "" : site.ReflectedType.Name;

            Console.WriteLine("[Plugin Error] An exception was thrown\n   within a {0} callback\n   at {1}\nHere's the exception report:\n{2}",
                caller, className + "." + methodName, e.Message);
        }
    }
}
