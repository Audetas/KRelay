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
using System.IO;

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
                     "Use /serv to display your current server.\n" +
                     "Use /vault to go to your vault. (BUGGY!)\n";
        }

        public string[] GetCommands()
        { return new string[] { "/connect", "/recon", "/drecon", "/serv", "/vault" }; }

        public void Initialize(Proxy proxy)
        {
            _proxy = proxy;
            proxy.HookPacket(PacketType.RECONNECT, OnReconnect);
            proxy.HookPacket(PacketType.CREATESUCCESS, OnCreateSuccess);
            proxy.HookPacket(PacketType.NEWTICK, OnNewTick);
            proxy.HookCommand("serv", OnServCommand);
            proxy.HookCommand("connect", OnConnectCommand);
            proxy.HookCommand("recon", OnReconnectCommand);
            proxy.HookCommand("drecon", OnDReconnectCommand);
            proxy.HookCommand("vault", OnVaultCommand);
            //proxy.HookCommand("yard", OnYardCommand);
            //proxy.HookCommand("hall", OnHallCommand);
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

        private ConClient getClientFromName(string name)
        {
            if (clients.ContainsKey(name))
            {
                return clients[name];
            }
            else
            {
                return null;
            }
        }

        private void OnNewTick(Client client, Packet packet)
        {
            ConClient _c = getClient(client);
            _c._toConnect = client;

            if (!_c.hasWelcomed)
            {
                if (client.PlayerData != null)
                {
                    if (client.PlayerData.Name != null && client.PlayerData.Name != "")
                    {
                        // Send welcome message to player
                        string message = "Welcome to K Relay!";
                        message += "\\n";
                        if (_c.currentServ == null)
                        {
                            foreach (var pair in Serializer.Servers)
                            {
                                if (pair.Value == _proxy.getRemoteAddress(client))
                                {
                                    message += pair.Key;
                                    _c.setCurrentServer(pair.Key);
                                }
                            }
                        }
                        else
                        {
                            message += _c.currentServ;
                        }
                        message += " " + _c.lastRealm;

                        PluginUtils.Delay(1500, () => client.SendToClient(
                            PluginUtils.CreateNotification(client.ObjectId, message)));
                        _c.hasWelcomed = true;
                    }
                }
            }
            
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Plugins\\Reconnect\\");
            if (File.Exists(Directory.GetCurrentDirectory() + "\\Plugins\\Reconnect\\Change.text"))
            {
                string[] lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Plugins\\Reconnect\\Change.text");
                if (lines.Length > 0)
                {
                    if (lines[0] != null && lines[0] != "")
                    {
                        string[] data = lines[0].Split(',');
                        if (getClientFromName(data[1]) != null)
                        {
                            ConClient cl = getClientFromName(data[1]);
                            cl.reconnectData = data;
                            cl.reconnectReady = true;
                        }
                    }
                }
                File.Delete(Directory.GetCurrentDirectory() + "\\Plugins\\Reconnect\\Change.text");
            }

            if (_c.reconnectReady)
            {
                if (_c.reconnectData[0] == "server")
                {
                    ChangeServer(Serializer.Servers[_c.reconnectData[2]], _c.reconnectData[2], _c._toConnect);
                }
                else if (_c.reconnectData[0] == "realm")
                {
                    //ReconnectPacket _recon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
                    _c._recon.IsFromArena = false;
                    _c._recon.GameId = 0;
                    _c._recon.Host = _c.reconnectData[3];
                    _c._recon.Port = 2050;
                    _c._recon.Key = new byte[0];
                    _c._recon.KeyTime = 0;//int.Parse(_c.reconnectData[4]);
                    _c._recon.Name = "NexusPortal." + _c.reconnectData[2];

                    Console.WriteLine(_c._recon.ToString());

                    SendReconnect(_c._recon, _c._toConnect);
                }
                _c.reconnectReady = false;
            }
            
        }

        private void OnCreateSuccess(Client client, Packet packet)
        {
            ConClient _c = getClient(client);

            /*
            // Send welcome message to player
            string message = "Welcome to K Relay!";
            message += "\\n";
            foreach (var pair in Serializer.Servers)
            {
                if (pair.Value == _proxy.getRemoteAddress(client))
                {
                    message += pair.Key;
                    _c.setCurrentServer(pair.Key);
                }
            }

            message += " " + _c.lastRealm;

            PluginUtils.Delay(1500, () => client.SendToClient(
                PluginUtils.CreateNotification(client.ObjectId, message)));
            */
        }

        private void OnReconnect(Client client, Packet packet)
        {
            ConClient _c = getClient(client);
            _c.hasWelcomed = false;

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
                _c.lastRealmIP = reconnect.Host;
                _c.lastRealmTime = reconnect.KeyTime.ToString();
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
                /*
                _c._vault.IsFromArena = false;
                _c._vault.GameId = reconnect.GameId;
                _c._vault.Host = (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
                _c._vault.Port = (reconnect.Port == -1 ? _proxy.Port : reconnect.Port);
                _c._vault.Key = reconnect.Key;
                _c._vault.KeyTime = reconnect.KeyTime;
                _c._vault.Name = reconnect.Name;
                */
            }
            else if (reconnect.Name.ToLower().Contains("pet"))
            {
                _c.setRealm("Pet Yard");
                
                _c._yard.IsFromArena = false;
                _c._yard.GameId = reconnect.GameId;
                _c._yard.Host = (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
                _c._yard.Port = (reconnect.Port == -1 ? _proxy.Port : reconnect.Port);
                _c._yard.Key = reconnect.Key;
                _c._yard.KeyTime = reconnect.KeyTime;
                _c._yard.Name = reconnect.Name;
                
            }
            else if (reconnect.Name.ToLower().Contains("guild"))
            {
                _c.setRealm("Guild Hall");
                
                _c._ghall.IsFromArena = false;
                _c._ghall.GameId = reconnect.GameId;
                _c._ghall.Host = (reconnect.Host == "" ? _proxy.getRemoteAddress(client) : reconnect.Host);
                _c._ghall.Port = (reconnect.Port == -1 ? _proxy.Port : reconnect.Port);
                _c._ghall.Key = reconnect.Key;
                _c._ghall.KeyTime = reconnect.KeyTime;
                _c._ghall.Name = reconnect.Name;
                
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

            //Console.WriteLine(reconnect.ToString());

            // Tell the client to connect to the proxy
            reconnect.Host = "localhost";
            reconnect.Port = 2050;
        }

        private void OnServCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, _c.currentServ + " " + _c.lastRealm));
        }

        private void OnVaultCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            SendReconnect(_c._vault, client);
        }

        private void OnYardCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            SendReconnect(_c._yard, client);
        }

        private void OnHallCommand(Client client, string command, string[] args)
        {
            ConClient _c = getClient(client);

            SendReconnect(_c._ghall, client);
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
            ConClient _c = getClient(client);
            _c.hasWelcomed = false;

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
