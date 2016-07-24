using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking
{
    public class ReconnectHandler
    {
        private Proxy _proxy;

        public void Attach(Proxy proxy)
        {
            _proxy = proxy;
            proxy.HookPacket<CreateSuccessPacket>(OnCreateSuccess);
            proxy.HookPacket<ReconnectPacket>(OnReconnect);
            proxy.HookPacket<HelloPacket>(OnHello);

            proxy.HookCommand("con", OnConnectCommand);
            proxy.HookCommand("connect", OnConnectCommand);
            proxy.HookCommand("server", OnConnectCommand);
            proxy.HookCommand("recon", OnReconCommand);
            proxy.HookCommand("drecon", OnDreconCommand);
        }

        private void OnHello(Client client, HelloPacket packet)
        {
            client.State = _proxy.GetState(client, packet.Key);
            if (client.State.ConRealKey.Length != 0)
            {
                packet.Key = client.State.ConRealKey;
                client.State.ConRealKey = new byte[0];
            }
            client.Connect(packet);
            packet.Send = false;
        }

        private void OnCreateSuccess(Client client, CreateSuccessPacket packet)
        {
            PluginUtils.Delay(1000, () =>
            {
                string message = "Welcome to K Relay!";
				string server = GameData.GameData.Servers.Match(s => s.Address == client.State.ConTargetAddress).Name;

                if (server != "")
                    message += "\\n" + server;

                client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, message));
            });
        }

        private void OnReconnect(Client client, ReconnectPacket packet)
        {
            if (packet.Host.Contains(".com"))
                packet.Host = Dns.GetHostEntry(packet.Host).AddressList[0].ToString();

            if (packet.Name.ToLower().Contains("nexusportal"))
            {
                ReconnectPacket recon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
                recon.IsFromArena = false;
                recon.GameId = packet.GameId;
                recon.Host = packet.Host == "" ? client.State.ConTargetAddress : packet.Host;
                recon.Port = packet.Port == -1 ? client.State.ConTargetPort : packet.Port;
                recon.Key = packet.Key;
                recon.KeyTime = packet.KeyTime;
                recon.Name = packet.Name;
                client.State.LastRealm = recon;
            }
            else if (packet.Name != "" && !packet.Name.Contains("vault") && packet.GameId != -2)
            {
                ReconnectPacket drecon = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
                drecon.IsFromArena = false;
                drecon.GameId = packet.GameId;
                drecon.Host = packet.Host == "" ? client.State.ConTargetAddress : packet.Host;
                drecon.Port = packet.Port == -1 ? client.State.ConTargetPort : packet.Port;
                drecon.Key = packet.Key;
                drecon.KeyTime = packet.KeyTime;
                drecon.Name = packet.Name;
                client.State.LastDungeon = drecon;
            }

            if (packet.Port != -1)
                client.State.ConTargetPort = packet.Port;

            if (packet.Host != "")
                client.State.ConTargetAddress = packet.Host;

            if (packet.Key.Length != 0)
                client.State.ConRealKey = packet.Key;

            // Tell the client to connect to the proxy
            packet.Key = Encoding.UTF8.GetBytes(client.State.GUID);
            packet.Host = "localhost";
            packet.Port = 2050;
        }

        private void OnConnectCommand(Client client, string command, string[] args)
        {

            if (args.Length == 1 && GameData.GameData.Servers.Map.Where(s => s.Value.Abbreviation == args[0].ToUpper()).Count() == 1)
            {
                ReconnectPacket reconnect = (ReconnectPacket)Packet.Create(PacketType.RECONNECT);
                reconnect.Host = GameData.GameData.Servers.ByID(args[0].ToUpper()).Address;
                reconnect.Port = 2050;
                reconnect.GameId = -2;
                reconnect.Name = "Nexus";
                reconnect.IsFromArena = false;
                reconnect.Key = new byte[0];
                reconnect.KeyTime = 0;
                SendReconnect(client, reconnect);
            }
            else
                client.SendToClient(PluginUtils.CreateOryxNotification("K Relay", "Unknown server!"));
        }

        private void OnReconCommand(Client client, string command, string[] args)
        {
            if (client.State.LastRealm != null)
                SendReconnect(client, client.State.LastRealm);
            else
                client.SendToClient(PluginUtils.CreateOryxNotification("K Relay", "Last realm is unknown!"));
        }

        private void OnDreconCommand(Client client, string command, string[] args)
        {
            if (client.State.LastDungeon != null)
                SendReconnect(client, client.State.LastDungeon);
            else
                client.SendToClient(PluginUtils.CreateOryxNotification("K Relay", "Last dungeon is unknown!"));
        }

        public static void SendReconnect(Client client, ReconnectPacket reconnect)
        {
            string host = reconnect.Host;
            int port = reconnect.Port;
            byte[] key = reconnect.Key;
            client.State.ConTargetAddress = host;
            client.State.ConTargetPort = port;
            client.State.ConRealKey = key;
            reconnect.Key = Encoding.UTF8.GetBytes(client.State.GUID);
            reconnect.Host = "localhost";
            reconnect.Port = 2050;

            client.SendToClient(reconnect);

            reconnect.Key = key;
            reconnect.Host = host;
            reconnect.Port = port;
        }
    }
}
