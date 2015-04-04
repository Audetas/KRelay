using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientStatAnnouncer
{
    public class ClientStatAnnouncer : IPlugin
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "ClientStat Announcer"; }

        public string GetDescription()
        { return "Lets you know when you progress on in-game achievments."; }

        public string[] GetCommands()
        { return new string[] { }; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.CLIENTSTAT, OnClientStat);
        }

        private void OnClientStat(Client client, Packet packet)
        {
            ClientStatPacket clientStat = (ClientStatPacket)packet;
            client.SendToClient(
                PluginUtils.CreateOryxNotification(
                    "ClientStat Announcer", clientStat.Name + " has increased to " + clientStat.Value + "!"));
        }
    }
}
