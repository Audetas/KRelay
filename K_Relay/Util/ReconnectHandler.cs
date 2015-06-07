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
using System.Windows.Forms;

namespace K_Relay.Util
{
    public class ReconnectHandler : IPlugin
    {
        private Proxy _proxy;
        private Client _toConnect;

        private ReconnectPacket _recon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
        private ReconnectPacket _drecon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Internal Reconnect Handler"; }

        public string GetDescription()
        { return "Changes the host name to that of the entered portal.\n" +
                   "Required to be able to enter portals.\n" +
                   "Use /connect ingame to change server.\n" +
                   "Use /recon to reconnect to your last realm.\n" +
                   "Use /drecon to reconnect to your last dungeon.\n"; }

        public string[] GetCommands()
        { return new string[] { "/connect", "/recon", "/drecon" }; }

        public void Initialize(Proxy proxy)
        {
            _proxy = proxy;
            proxy.HookPacket(PacketType.RECONNECT, OnReconnect);
            proxy.HookPacket(PacketType.CREATESUCCESS, OnCreateSuccess);
            proxy.HookCommand("connect", OnConnectCommand);
            proxy.HookCommand("recon", OnReconnectCommand);
            proxy.HookCommand("drecon", OnDReconnectCommand);
        }

        private void OnCreateSuccess(Client client, Packet packet)
        {
            // Send welcome message to player
            string message = "Welcome to K Relay!";
            foreach (var pair in Serializer.Servers)
                if (pair.Value == _proxy.RemoteAddress)
                    message += "\\n" + pair.Key;

            PluginUtils.Delay(1500, () => client.SendToClient(
                PluginUtils.CreateNotification(client.ObjectId, message)));
        }

        private void OnReconnect(Client client, Packet packet)
        {
            ReconnectPacket reconnect = packet as ReconnectPacket;

            if (reconnect.Host.Contains(".com"))
                reconnect.Host = Dns.GetHostEntry(reconnect.Host).AddressList[0].ToString();

            if (reconnect.Name.ToLower().Contains("nexusportal"))
            {
                _recon.IsFromArena = false;
                _recon.GameId = reconnect.GameId;
                _recon.Host = (reconnect.Host == "" ? _proxy.RemoteAddress : reconnect.Host);
                _recon.Port = (reconnect.Port == -1 ? _proxy.Port : reconnect.Port);
                _recon.Key = reconnect.Key;
                _recon.KeyTime = reconnect.KeyTime;
                _recon.Name = reconnect.Name;
            }
            else if (reconnect.Name != "" && !reconnect.Name.Contains("vault") && reconnect.GameId != -2)
            {
                _drecon.IsFromArena = false;
                _drecon.GameId = reconnect.GameId;
                _drecon.Host = (reconnect.Host == "" ? _proxy.RemoteAddress : reconnect.Host);
                _drecon.Port = (reconnect.Port == -1 ? _proxy.Port : reconnect.Port);
                _drecon.Key = reconnect.Key;
                _drecon.KeyTime = reconnect.KeyTime;
                _drecon.Name = reconnect.Name;
            }

            if (reconnect.Port != -1)
                _proxy.Port = reconnect.Port;

            if (reconnect.Host != "")
                _proxy.RemoteAddress = reconnect.Host;

            // Tell the client to connect to the proxy
            reconnect.Host = "localhost";
            reconnect.Port = 2050;
        }

        private void OnConnectCommand(Client client, string command, string[] args)
        {
            _toConnect = client;
            PluginUtils.ShowGUI(new FrmServerReconnect(this));
        }

        private void OnReconnectCommand(Client client, string command, string[] args)
        {
            SendReconnect(_recon, client);
        }

        private void OnDReconnectCommand(Client client, string command, string[] args)
        {
            SendReconnect(_drecon, client);
        }

        public void ChangeServer(string address, string name)
        {
            Console.WriteLine("[Reconnect Handler] Changing to server {0}({1}).", name, address);
            ReconnectPacket reconnect = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
            reconnect.Host = address;
            reconnect.Port = 2050;
            reconnect.GameId = -2;
            reconnect.Name = "Connecting to " + name;

            reconnect.IsFromArena = false;
            reconnect.Key = new byte[0];
            reconnect.KeyTime = 0;
            SendReconnect(reconnect, _toConnect);
        }

        private void SendReconnect(ReconnectPacket reconnect, Client client)
        {
            string host = reconnect.Host;
            int port = reconnect.Port;
            _proxy.RemoteAddress = host;
            _proxy.Port = port;
            reconnect.Host = "localhost";
            reconnect.Port = 2050;

            client.SendToClient(reconnect);

            reconnect.Host = host;
            reconnect.Port = port;
        }
    }
}
