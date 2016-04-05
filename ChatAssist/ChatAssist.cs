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
using System.Text;
using System.Threading.Tasks;

namespace ChatAssist
{
    public class ChatAssist : IPlugin
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
            proxy.HookCommand("chatassist", OnChatAssistCommand);
            proxy.HookPacket(PacketType.TEXT, OnText);
        }

        private void OnChatAssistCommand(Client client, string command, string[] args)
        {
            if (args.Length == 0) return;

            if (args[0] == "settings")
                PluginUtils.ShowGUI(new FrmChatAssistSettings());
            else if (args[0] == "on")
            {
                ChatAssistConfig.Default.Enabled = true;
                client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Chat Assist Enabled!"));
            }
            else if (args[0] == "off")
            {
                ChatAssistConfig.Default.Enabled = false;
                client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Chat Assist Disabled!"));
            }
        }

        private void OnText(Client client, Packet packet)
        {
            if (!ChatAssistConfig.Default.Enabled) return;
            //TextPacket text = (TextPacket)packet;
            TextPacket text = packet.To<TextPacket>();

            if (ChatAssistConfig.Default.DisableMessages && text.Recipient == "")
            {
                text.Send = false;
                return;
            }

            foreach (string filter in ChatAssistConfig.Default.Blacklist)
            {
                if (text.Text.ToLower().Contains(filter.ToLower()))
                {
                    // Is spam
                    if (ChatAssistConfig.Default.CensorSpamMessages)
                    {
                        text.Text = "...";
                        text.CleanText = "...";
                        return;
                    }

                    text.Send = false;

                    if (ChatAssistConfig.Default.AutoIgnoreSpamMessage ||
                       (ChatAssistConfig.Default.AutoIgnoreSpamPM && text.Recipient != ""))
                    {
                        // Ignore
                        PlayerTextPacket playerText = new PlayerTextPacket();
                        playerText.Text = "/ignore " + text.Name;
                        client.SendToServer(playerText);
                        return;
                    }
                }
            }
        }
    }
}
