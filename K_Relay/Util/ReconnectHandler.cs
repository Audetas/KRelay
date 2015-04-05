using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K_Relay.Util
{
    public class ReconnectHandler : IPlugin
    {
        private Proxy _proxy;
        private string _originalHost;
        private int _originalPort;
        private Client _toConnect;

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Internal Reconnect Handler"; }

        public string GetDescription()
        { return "Changes the host name to that of the entered portal.\n" +
                 "Required to be able to enter portals.\n" + 
                 "Use /connect ingame to change server.\n" +
                 "You can disable this in the settings if you wish to handle RECONNECT packets yourself."; }

        public string[] GetCommands()
        { return new string[] { "/connect" }; }

        public void Initialize(Proxy proxy)
        {
            _proxy = proxy;
            proxy.HookPacket(PacketType.RECONNECT, OnReconnectPacket);
            proxy.HookPacket(PacketType.CREATE_SUCCESS, OnCreateSuccess);
            proxy.HookCommand("connect", OnConnectCommand);
            _originalPort = _proxy.Port;
            _originalHost = _proxy.RemoteAddress;
        }

        private void OnCreateSuccess(Client client, Packet createSuccessPacket)
        {
            CreateSuccessPacket createSuccess = createSuccessPacket as CreateSuccessPacket;
            // Restore the original connection info so new clients can connect normally
            _proxy.RemoteAddress = _originalHost;
            _proxy.Port = _originalPort;

            // Send welcome message to player
            //PluginUtils.Delay(2000, () => client.SendToClient(
                //PluginUtils.CreateNotification(client.ObjectId, "Welcome to K Relay!")));
        }

        private void OnReconnectPacket(Client client, Packet packet)
        {
            ReconnectPacket reconnect = packet as ReconnectPacket;
            if (reconnect.Port != -1)
            {
                _proxy.Port = reconnect.Port;
                Console.WriteLine("[Reconnect Handler] Changed remote port to {0}.", _proxy.Port);
            }

            if (reconnect.Host != "")
            {
                if (reconnect.Host.Contains(".com"))
                    _proxy.RemoteAddress = Dns.GetHostEntry(reconnect.Host).AddressList[0].ToString();
                else
                    _proxy.RemoteAddress = reconnect.Host;
                Console.WriteLine("[Reconnect Handler] Changed remote address to {0}.", _proxy.RemoteAddress);
            }

            // Tell the client to connect to the proxy
            reconnect.Host = "localhost";
            reconnect.Port = 2050;
        }

        private void OnConnectCommand(Client client, string command, string[] args)
        {
            _toConnect = client;
            PluginUtils.ShowGUI(new FrmServerReconnect(this));
        }

        public void ChangeServer(string address, string name)
        {
            Console.WriteLine("[Reconnect Handler] Changing to server {0}({1}).", name, address);
            ReconnectPacket reconnect = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
            _proxy.Port = 2050;
            _proxy.RemoteAddress = address;

            reconnect.Host = "localhost";
            reconnect.Port = 2050;
            reconnect.GameId = -2;
            reconnect.Name = "Connecting to " + name;

            reconnect.IsFromArena = false;
            reconnect.Key = new byte[0];
            reconnect.KeyTime = 0;
            _toConnect.SendToClient(reconnect);
        }
    }
}
