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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpamFilter
{
    public class SpamFilter : IPlugin
    {
        private bool _enabled = true;

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "SpamFilter"; }

        public string GetDescription()
        { return "Filters other players' messages based on a customizable blacklist."; }
           
        public string[] GetCommands()
        { return new string[]{"/spamfilter toggle", "/spamfilter add <filter>", "/spamfilter remove <filter>"}; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.TEXT, OnTextPacket);
            proxy.HookCommand("spamfilter", OnSpamFilterCommand);
        }

        private void OnTextPacket(Client client, Packet textPacket)
        {
            if (!_enabled) return;
            TextPacket text = (TextPacket)textPacket;

            foreach (string filter in SpamFilterConfig.Default.Blacklist)
            {
                if (text.Text.ToLower().Contains(filter.ToLower()))
                    textPacket.Send = false;
            }
        }

        private void OnSpamFilterCommand(Client client, string command, string[] args)
        {
            if (args.Length < 2) return;

            if (args[0] == "toggle")
            {
                _enabled = !_enabled;
            }
            else if (args[0] == "add")
            {
                SpamFilterConfig.Default.Blacklist.Add(args[1]);
                SpamFilterConfig.Default.Save();
            }
            else if (args[1] == "remove" && SpamFilterConfig.Default.Blacklist.Contains(args[1]))
            {
                SpamFilterConfig.Default.Blacklist.Remove(args[1]);
                SpamFilterConfig.Default.Save();
            }
        }
    }
}
