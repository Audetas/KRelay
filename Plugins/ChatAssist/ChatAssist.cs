using LibKRelay;
using LibKRelay.Messages;
using LibKRelay.Messages.Client;
using LibKRelay.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAssist
{
    public class ChatAssist : Plugin
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Chat Assist"; }

        public string GetDescription()
        { return "A collection of tools to help your reduce the spam and clutter of chat and make it easier prevent future spam."; }

        public string[] GetCommands()
        { return new string[] { "/chatassist off", "/chatassist on", "/chatassist settings" }; }

        public override void Initialize(ClientListener proxy)
        {
            Message.Hook<Text>(OnText);
            Message.Hook<PlayerText>(OnPlayerText);
        }

        private void OnPlayerText(Connection con, PlayerText message)
        {
            if (message.IsCommand("chatassist"))
            {
                var args = message.GetArgs();
                if (args.Length == 0) return;

                if (args[0] == "settings")
                    ShowGUI(new FrmChatAssistSettings());
                else if (args[0] == "on")
                {
                    ChatAssistConfig.Default.Enabled = true;
                    con.Client.Send(CreateNotification(con.Self.ObjectId, "Chat Assist Enabled!"));
                }
                else if (args[0] == "off")
                {
                    ChatAssistConfig.Default.Enabled = false;
                    con.Client.Send(CreateNotification(con.Self.ObjectId, "Chat Assist Disabled!"));
                }
            }
        }

        private void OnText(Connection con, Text text)
        {
            if (!ChatAssistConfig.Default.Enabled) return;

            if (ChatAssistConfig.Default.DisableMessages && text.Recipient == "")
            {
                text.Send = false;
                return;
            }

            foreach (string filter in ChatAssistConfig.Default.Blacklist)
            {
                if (text.RawText.ToLower().Contains(filter.ToLower()))
                {
                    // Is spam
                    if (ChatAssistConfig.Default.CensorSpamMessages)
                    {
                        text.RawText = "...";
                        text.CleanText = "...";
                        return;
                    }

                    text.Send = false;

                    if (ChatAssistConfig.Default.AutoIgnoreSpamMessage ||
                       (ChatAssistConfig.Default.AutoIgnoreSpamPM && text.Recipient != ""))
                    {
                        // Ignore
                        PlayerText playerText = new PlayerText();
                        playerText.Text = "/ignore " + text.Name;
                        con.Server.Send(playerText);
                        return;
                    }
                }
            }
        }
    }
}
