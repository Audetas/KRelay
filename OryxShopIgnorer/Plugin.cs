using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OryxShopIgnorer
{
    public class Plugin : IPlugin
    {
        public string GetAuthor()
        {
            return "ossimc82";
        }

        public string GetName()
        {
            return "OryxShop Blocker";
        }

        public string GetDescription()
        {
            return "This plugin blocks the OryxShop spammers";
        }

        public string[] GetCommands()
        {
            return new string[0];
        }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket<TextPacket>(OnTextPacketReceived);
        }

        private void OnTextPacketReceived(Client client, TextPacket packet)
        {
            if (packet.Text.Contains("ORYXSH0P.NET"))
            {
                var editAccListPacket = Packet.Create(PacketType.EDITACCOUNTLIST) as EditAccountListPacket;
                editAccListPacket.Add = true;
                editAccListPacket.AccountListId = 1;
                editAccListPacket.ObjectId = packet.ObjectId;
                client.SendToServer(editAccListPacket);
                packet.Send = false;
            }
        }
    }
}
