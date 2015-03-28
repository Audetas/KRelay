using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpamFilter
{
    public class SpamFilter : IPlugin
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "SpamFilter"; }

        public string GetDescription()
        { return "Filters other players' messages based on a customizable blacklist. Use '/spamfilter gui' to customize."; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.TEXT, OnTextPacket);
            proxy.HookPacket(PacketType.PLAYERTEXT, OnPlayerTextPacket);
        }

        private void OnTextPacket(ClientInstance client, Packet textPacket)
        {
            if (!SpamFilterConfig.Default.Enabled) return;

            string text = textPacket.Get<string>("Text");
            foreach (string filter in SpamFilterConfig.Default.Blacklist)
            {
                if (SpamFilterConfig.Default.CaseSensitive && text.Contains(filter))
                    textPacket.Send = false;
                else if (text.ToLower().Contains(filter.ToLower()))
                    textPacket.Send = false;
            }
        }

        private void OnPlayerTextPacket(ClientInstance client, Packet playerTextPacket)
        {
            string playerText = playerTextPacket.Get<string>("Text").ToLower();

            if (playerText.StartsWith("/spamfilter"))
                playerTextPacket.Send = false;

            if (playerText == "/spamfilter gui") // PROPERTYGRID IS BUGGED WITH STRING COLLECTION :(
                new Thread(() => new FrmSpamFilter().ShowDialog()).Start();
        }
    }
}
