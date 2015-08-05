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

        private Dictionary<string, ConClient> clients = new Dictionary<string, ConClient>();

        public string GetAuthor()
        { return "Juix & KrazyShank"; }

        public string GetName()
        { return "Multi-Client Reconnect Handler"; }

        public string GetDescription()
        {
            return "Changes the host name to that of the entered portal.\n" +
                     "Required to be able to enter portals.\n" +
                     "Use /connect ingame to change server.\n" +
                     "Use /recon to reconnect to your last realm.\n" +
                     "Use /drecon to reconnect to your last dungeon.\n" +
                     "Use /serv to display your current server.\n";
        }

        public string[] GetCommands()
        { return new string[] { "/connect", "/recon", "/drecon", "/serv" }; }

        public void Initialize(Proxy proxy)
        {
            _proxy = proxy;
            proxy.HookPacket(PacketType.RECONNECT, OnReconnect);
            proxy.HookPacket(PacketType.CREATESUCCESS, OnCreateSuccess);
            proxy.HookCommand("serv", OnServCommand);
            proxy.HookCommand("connect", OnConnectCommand);
            proxy.HookCommand("recon", OnReconnectCommand);
            proxy.HookCommand("drecon", OnDReconnectCommand);
        }

        private ConClient getClient(Client client)
        {
            if (clients.ContainsKey(client.PlayerData.Name))
            {
                return clients[client.PlayerData.Name];
            }
            else
            {
                clients[client.PlayerData.Name] = new ConClient(_proxy, client);
                return clients[client.PlayerData.Name];
            }
        }

        private void OnCreateSuccess(Client client, Packet packet)
        {
            ConClient _c = getClient(client);

            // Send welcome message to player
            string message = "Welcome to K Relay!";
            foreach (var pair in Serializer.Servers)
            {
                if (pair.Value == _proxy.getRemoteAddress(client))
                {
                    message += "\\n" + pair.Key;
                    _c.setCurrentServer(pair.Key);
                }
            }

            PluginUtils.Delay(1500, () => client.SendToClient(
                PluginUtils.CreateNotification(client.ObjectId, message)));
        }

        private void OnReconnect(Client client, Packet packet)
        {
            ConClient _c = getClient(client);

            ReconnectPacket reconnect = packet as ReconnectPacket;

            if (reconnect.Host.Contains(".com"))
                reconnect.Host = Dns.GetHostEntry(reconnect.Host).AddressList[0].ToString();

            if (reconnect.Name.ToLower().Contains("nexusportal"))
            {
                _c._recon.IsFromArena = false;
                _c._recon.GameId = reconnect.GameId;
                _c._recon.Host = (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
                _c._recon.Port = (reconnect.Port == -1 ? _proxy.Port : reconnect.Port);
                _c._recon.Key = reconnect.Key;
                _c._recon.KeyTime = reconnect.KeyTime;
                _c._recon.Name = reconnect.Name;
                string[] names = reconnect.Name.Split('.');
                _c.setRealm(names[1]);
            }
            else if (reconnect.Name != "" && !reconnect.Name.Contains("vault") && reconnect.GameId != -2)
            {
                _c._drecon.IsFromArena = false;
                _c._drecon.GameId = reconnect.GameId;
                _c._drecon.Host = (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
                _c._drecon.Port = (reconnect.Port == -1 ? _proxy.Port : reconnect.Port);
                _c._drecon.Key = reconnect.Key;
                _c._drecon.KeyTime = reconnect.KeyTime;
                _c._drecon.Name = reconnect.Name;
            }

            if (reconnect.Name.ToLower().Contains("nexus") && !reconnect.Name.ToLower().Contains("portal"))
            {
                _c.setRealm("Nexus");
            }
            else if (reconnect.Name.ToLower().Contains("vault"))
            {
                _c.setRealm("Vault");
            }
            else if (reconnect.Name.ToLower().Contains("pet"))
            {
                _c.setRealm("Pet Yard");
            }
            else if (reconnect.Name.ToLower().Contains("guild"))
            {
                _c.setRealm("Guild Hall");
            }

            if (reconnect.Port != -1)
                _proxy.Port = reconnect.Port;

            if (reconnect.Host != "")
            {
                _proxy.defTempServer = reconnect.Host;
            }
            else
            {
                _proxy.defTempServer = _proxy.getRemoteAddress(client);
            }

            // Tell the client to connect to the proxy
            reconnect.Host = "localhost";
            reconnect.Port = 2050;
        }

        private void OnServCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, _c.currentServ + " " + _c.lastRealm));
        }

        private void OnConnectCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            _c._toConnect = client;
            PluginUtils.ShowGUI(new FrmServerReconnect(this, client));
        }

        private void OnReconnectCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            SendReconnect(_c._recon, client);
        }

        private void OnDReconnectCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            SendReconnect(_c._drecon, client);
        }

        public void ChangeServer(string address, string name, Client client)
        {
            ConClient _c = getClient(client);

            Console.WriteLine("[Reconnect Handler] Changing to server {0}({1}).", name, address);
            ReconnectPacket reconnect = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
            reconnect.Host = address;
            reconnect.Port = 2050;
            reconnect.GameId = -2;
            reconnect.Name = "Connecting to " + name;
            _c.setCurrentServer(name);
            _c.setRealm("Nexus");

            reconnect.IsFromArena = false;
            reconnect.Key = new byte[0];
            reconnect.KeyTime = 0;
            SendReconnect(reconnect, _c._toConnect);
        }

        private void SendReconnect(ReconnectPacket reconnect, Client client)
        {
            string host = reconnect.Host;
            int port = reconnect.Port;
            if (!host.ToLower().Contains("localhost")) _proxy.defTempServer = host;
            _proxy.Port = port;
            reconnect.Host = "localhost";
            reconnect.Port = 2050;

            client.SendToClient(reconnect);

            reconnect.Host = host;
            reconnect.Port = port;
        }

        private string ServerNameFromHost(string host)
        {
            KeyValuePair<string, string>[] servers = Serializer.Servers.ToArray();
            foreach (KeyValuePair<string, string> pair in servers)
            {
                if (pair.Value == host) return pair.Key;
            }
            return "";
        }
    }
}
