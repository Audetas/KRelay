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
        private string lastMessage = "";
        private string[] NPCIgnoreList = { "#Mystery Box Shop", "#The Alchemist", "#Login Seer", "#The Tinkerer", "#Bandit Leader", "#Drake Baby", "#Dwarf King", "#Killer Pillar", "#Haunted Armor", "#Red Demon", "#Cyclops God", "#Belladonna", "#Sumo Master", "#Avatar of the Forgotten King", "#Small Ghost", "#Medium Ghost", "#Ghost Master", "#Ghost King", "#Lich", "#Haunted Spirit", "#Giant Oryx Chicken", "#Rock Construct", "#Phylactery Bearer", "#Big Yeti", "#Esben the Unwilling", "#Creepy Weird Dark Spirit Mirror Image Monster", "#Ent Ancient", "#Kage Kami", "#Twilight Archmage", "#The Forgotten Sentinel", "#Titanum of Cruelty", "#Titanum of Despair", "#Titanum of Lies", "#Titanum of Hate", "#Grand Sphinx", "#Troll Matriarch", "#Dreadstump the Pirate King", "#Stone Mage" };

        public string GetAuthor()
        { return "KrazyShank / Kronks / RotMGHacker"; }

        public string GetName()
        { return "Chat Assist"; }

        public string GetDescription()
        { return "A collection of tools to help your reduce the spam and clutter of chat and make it easier prevent future spam."; }

        public string[] GetCommands()
        { return new string[] { "/chatassist [On/Off]", "/chatassist settings", "/chatassist add [message] - add a string to the spam filter", "/chatassist remove [message] - removes a string from the spam filter", "/chatassist list - list all strings included in the spam filter", "/re - Resends the last message you've typed on chat." }; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookCommand("chatassist", OnChatAssistCommand);
            proxy.HookCommand("re", OnResendCommand);

            proxy.HookPacket(PacketType.TEXT, OnText);
            proxy.HookPacket(PacketType.PLAYERTEXT, OnPlayerText);
        }

        private void OnChatAssistCommand(Client client, string command, string[] args)
        {
            if (args.Length == 0) return;

            if (args[0] == "settings")
            {
                PluginUtils.ShowGUI(new FrmChatAssistSettings());
            }
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
            else if (args[0] == "add")
            {
                if (args.Length > 1)
                {
                    // This is needed because args are seperated by whitespaces and the string to filter coud contain whitespaces so we concatenate them together
                    string toFilter = "";
                    for (int i = 1; i < args.Length; ++i)
                    {
                        toFilter += args[i] + " ";
                    }

                    toFilter = toFilter.Remove(toFilter.Length - 1); // Remove the trailing space

                    ChatAssistConfig.Default.Blacklist.Add(toFilter);
                    client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, toFilter + " added to spam filter!"));

                    ChatAssistConfig.Default.Save(); // Save our changes
                }
                else
                {
                    client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Missing message to filter!"));
                }
            }
            else if (args[0] == "remove" || args[0] == "rem")
            {
                if (args.Length > 1)
                {
                    string toRemove = "";
                    for (int i = 1; i < args.Length; ++i)
                    {
                        toRemove += args[i] + " ";
                    }

                    toRemove = toRemove.Remove(toRemove.Length - 1); // Remove the trailing space

                    if (ChatAssistConfig.Default.Blacklist.Contains(toRemove))
                    {
                        ChatAssistConfig.Default.Blacklist.Remove(toRemove);
                        client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, toRemove + " removed from spam filter!"));

                        ChatAssistConfig.Default.Save(); // Save our changes
                    }
                    else
                    {
                        client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Couldn't find " + toRemove + " in spam filter!"));
                    }
                }
                else
                {
                    client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Missing message to remove!"));
                }
            }
            else if (args[0] == "list")
            {
                if (ChatAssistConfig.Default.Blacklist.Count == 0)
                {
                    client.SendToClient(PluginUtils.CreateNotification(client.ObjectId, "Spam filter is empty!"));
                    return;
                }

                string message = "Spam filter contains: ";

                // Construct our list
                foreach (string filter in ChatAssistConfig.Default.Blacklist)
                {
                    message += filter + ", ";
                }

                message = message.Remove(message.Length - 2);

                client.SendToClient(PluginUtils.CreateOryxNotification("ChatAssist", message));
            }
        }

        private void OnResendCommand(Client client, string command, string[] args)
        {
            if (lastMessage != "")
            {
                PlayerTextPacket playerTextPacket = (PlayerTextPacket)Packet.Create(PacketType.PLAYERTEXT);
                playerTextPacket.Text = lastMessage;
                client.SendToServer(playerTextPacket);
            }
        }

        private void OnText(Client client, Packet packet)
        {
            if (!ChatAssistConfig.Default.Enabled) { return; }

            TextPacket text = packet.To<TextPacket>();

            if (ChatAssistConfig.Default.EnableNPCFilter && text.NumStars == -1) // Not a message from a user
            {
                foreach (string name in NPCIgnoreList)
                {
                    if (text.Name.Contains(name))
                    {
                        text.Send = false;
                        return;
                    }
                }

                //if (text.Name != "" && text.Name != "#Oryx the Mad God")
                //{
                //    PluginUtils.Log("ChatAssist", "Server Message '" + text.Text + "' from: " + text.Name);
                //}
            }

            if ((ChatAssistConfig.Default.DisableMessages && text.Recipient == "") ||
                (text.Recipient == "" && text.NumStars < ChatAssistConfig.Default.StarFilter && text.NumStars != -1) ||
                (text.Recipient != "" && text.NumStars < ChatAssistConfig.Default.StarFilterPM) && text.NumStars != -1)
            {
                text.Send = false;
                return;
            }

            // SpamFilter
            if (ChatAssistConfig.Default.EnableSpamFilter)
            {
                foreach (string filter in ChatAssistConfig.Default.Blacklist)
                {
                    if (text.Text.ToLower().Contains(filter.ToLower()))
                    {
                        // Is spam
                        if (ChatAssistConfig.Default.CensorSpamMessages)
                        {
                            text.Text = "...";
                            text.CleanText = "...";
                        }
                        else { text.Send = false; }

                        if (ChatAssistConfig.Default.AutoIgnoreSpamMessage ||
                           (ChatAssistConfig.Default.AutoIgnoreSpamPM && text.Recipient != ""))
                        {
                            // Ignore
                            PlayerTextPacket playerText = (PlayerTextPacket)Packet.Create(PacketType.PLAYERTEXT);
                            playerText.Text = "/ignore " + text.Name;
                            client.SendToServer(playerText);
                        }

                        return;
                    }
                }
            }

            // ChatLog
            if (ChatAssistConfig.Default.LogChat && text.NumStars != -1)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("ChatAssist.log", true))
                {
                    file.WriteLine("<" + DateTime.Now.ToString() + ">: " + text.Name + "[" + text.NumStars + "]: '" + text.Text + "'");
                }
            }
        }

        private void OnPlayerText(Client client, Packet packet)
        {
            PlayerTextPacket playerTextPacket = (PlayerTextPacket)packet;

            if (!playerTextPacket.Text.StartsWith("/") || playerTextPacket.Text.StartsWith("/t ") || playerTextPacket.Text.StartsWith("/tell ") || playerTextPacket.Text.StartsWith("/w ") || playerTextPacket.Text.StartsWith("/whisper ") || playerTextPacket.Text.StartsWith("/yell "))
            {
                lastMessage = playerTextPacket.Text;
            }

            if (ChatAssistConfig.Default.LogChat)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("ChatAssist.log", true))
                {
                    file.WriteLine("<" + DateTime.Now.ToString() + ">: You: '" + playerTextPacket.Text + "'");
                }
            }
        }
    }
}
