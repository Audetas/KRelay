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

namespace ChatAssist
{
    public class ChatAssist
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Chat Assist"; }

        public string GetDescription()
        { return "A collection of tools to help your reduce the spam and clutter of chat and make it easier prevent future spam."; }

        public string[] GetCommands()
        { return new string[] { "/chatassist off", "/chatassist on", "/chatassist settings" }; }

        public void Initialize(Proxy proxy)
        {

        }
    }
}
