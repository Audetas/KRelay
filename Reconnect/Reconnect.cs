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

namespace Reconnect
{
    public class Reconnect : IPlugin
    {

        //..\K_Relay\bin\Debug\Plugins\Reconnect\

        List<string> Realms = new List<string>() { "NexusPortal.Bat", "NexusPortal.Beholder", "NexusPortal.Blob", "NexusPortal.Chimera", "NexusPortal.Cube", "NexusPortal.Cyclops", "NexusPortal.Deathmage", "NexusPortal.Demon", "NexusPortal.Djinn", "NexusPortal.Dragon", "NexusPortal.Drake", "NexusPortal.Flayer", "NexusPortal.Gargoyle", "NexusPortal.Ghost", "NexusPortal.Giant", "NexusPortal.Goblin", "NexusPortal.Golem", "NexusPortal.Gorgon", "NexusPortal.Harpy", "NexusPortal.Hobbit", "NexusPortal.Hydra", "NexusPortal.Imp", "NexusPortal.Kraken", "NexusPortal.Leviathan", "NexusPortal.Lich", "NexusPortal.Medusa", "NexusPortal.Minotaur", "NexusPortal.Mummy", "NexusPortal.Ogre", "NexusPortal.Orc", "NexusPortal.Phoenix", "NexusPortal.Pirate", "NexusPortal.Reaper", "NexusPortal.Satyr", "NexusPortal.Scorpion", "NexusPortal.Skeleton", "NexusPortal.Slime", "NexusPortal.Snake", "NexusPortal.Spectre", "NexusPortal.Spider", "NexusPortal.Sprite", "NexusPortal.Titan", "NexusPortal.Unicorn", "NexusPortal.Wyrm" };

        public string help = "Welcome to Reconnect Plugin";

        Proxy proxy;
        ReconnectPacket vaultRec, dungRec, guildRec, realmRec, defaultRecPacket;

        public string GetAuthor()
        { return "TheMrNobody"; }

        public string GetName()
        { return "Reconnect"; }

        public string GetDescription()
        { return "placeholder"; }

        public void Initialize(Proxy proxy)
        {
            this.proxy = proxy;
            proxy.HookPacket(PacketType.USEPORTAL, OnUsePortal);
            proxy.HookPacket(PacketType.RECONNECT, OnReconnect);
            proxy.HookPacket(PacketType.PLAYERTEXT, OnPlayerText);

            //Initialise the reconnectPackets
            defaultRecPacket = Packet.CreateInstance(PacketType.RECONNECT) as ReconnectPacket;
            defaultRecPacket.GameId = 0;
            defaultRecPacket.Host = "localhost";
            defaultRecPacket.IsFromArena = false;
            defaultRecPacket.Name = "";
            defaultRecPacket.Port = 2050;
            defaultRecPacket.Key = new byte[0];
            defaultRecPacket.KeyTime = 0;

            vaultRec = defaultRecPacket; dungRec = defaultRecPacket; guildRec = defaultRecPacket; realmRec = defaultRecPacket;

            LoadFromSettings();
        }

        private void LoadFromSettings()
        {
            bool initializedFine = true;
            if (!LoadPacketFromData(vaultRec, out vaultRec, ReconnectSettings.Default.VaultData))
                initializedFine = false;
            if (!LoadPacketFromData(guildRec, out guildRec, ReconnectSettings.Default.GuildHallData))
                initializedFine = false;
            if (!LoadPacketFromData(dungRec, out dungRec, ReconnectSettings.Default.LastDungeonData))
                initializedFine = false;
            if (!LoadPacketFromData(realmRec, out realmRec, ReconnectSettings.Default.LastRealmData))
                initializedFine = false;

            if (!initializedFine)
                Console.WriteLine("[Reconnect] Ran into a problem or few while setting up");
        }

        /// <summary>
        /// Loads the packet from the saved data
        /// </summary>
        /// <param name="originalPacket">packet to load from</param>
        /// <param name="packet">packet to load into</param>
        /// <param name="data">Data to load into the packet</param>
        /// <returns>Success/Fail of the operation</returns>
        private bool LoadPacketFromData(ReconnectPacket originalPacket, out ReconnectPacket packet, string data)
        {
            packet = null;
            string[] dataSplit = data.Split(';');

            if (dataSplit.Length != 2)
                return false;

            int recId = 0;
            string realmName = "";

            if (int.TryParse(dataSplit[0], out recId))
            {
                realmName = dataSplit[1];

                packet = originalPacket;
                packet.Name = realmName;
                packet.GameId = recId;
                return true;
            }

            else
                return false;
        }

        private void OnPlayerText(ClientInstance client, Packet packet)
        {
            PlayerTextPacket text = packet as PlayerTextPacket;
            ReconnectPacket reconnectPacket = defaultRecPacket;

            #region Syntax
            /* /r d -- Reconnect to last dungeon
            * /r v -- Reconnect to last vault
            * /r g -- Reconnect to last ghall
            * /r -- Reconnect to last realm
            */
            #endregion

            //The first and third checks are so that commands starting with "/r" wouldnt be trigerred in this 
            if (text.Text.ToLower().StartsWith("/r ") || text.Text.ToLower().StartsWith("/reconnect") || text.Text.ToLower() == "/r")
            {//Cancel the packet sending
                packet.Send = false;

                Console.WriteLine("Gotcha!" + text.Text);

                try {
                    string[] commandSplit = text.Text.ToLower().Split(' ');
                    if (commandSplit.Length == 2)
                    {
                        string argument = commandSplit[1];
                        switch (argument)
                        {
                            case "d":
                                reconnectPacket = dungRec;
                                break;
                            case "v":
                                reconnectPacket = vaultRec;
                                break;
                            case "g":
                                reconnectPacket = guildRec;
                                break;
                            default:
                                throw new UnknownArgumentException();
                        }
                    }
                    else if (commandSplit.Length == 1)
                        reconnectPacket = realmRec;
                    else
                        throw new UnknownArgumentException("Too Many arguments");

                    client.SendToClient(reconnectPacket);

                }
                catch (Exception ex)
                {
                    if(ex.GetType() == typeof(UnknownArgumentException))
                    {
                        //Send a message saying showing the usage
                        NotificationPacket notificationPacket = Packet.CreateInstance(PacketType.NOTIFICATION) as NotificationPacket;
                        //Cannot Send a packet without the id of the player how do i get that?
                        notificationPacket.ObjectId = 23;
                        notificationPacket.Message = "{\"key\":\"blank\",\"tokens\":{\"data\":\"" + help + "\"}}";
                        notificationPacket.Color = 0xFF8000;
                        client.SendToClient(notificationPacket);
                        Console.WriteLine("Reconnect recieved too many arguments ");
                    }
                    else
                    {
                        //Send a message saying we found a problem
                        Console.WriteLine("Reconnect ran into a problem " + ex.Message);
                    }
                }
                //PlayerTextPacket textPacket = Packet.CreateInstance(PacketType.TEXT) as PlayerTextPacket;

            }
        }

        private void OnUsePortal(ClientInstance client, Packet packet)
        {

        }

        private void OnReconnect(ClientInstance client, Packet packet)
        {
            ReconnectPacket reconnect = packet as ReconnectPacket;

            if (reconnect.Name.ToLower() == "guild hall")
                //Connected to guild hall: save the guild hall(just in case again)
                ReconnectSettings.Default.GuildHallData = reconnect.GameId + ';' + reconnect.Name;

            else if (reconnect.Name.ToLower() == "server.vault")
                //Connected to vault: Save the vault (just in case)
                ReconnectSettings.Default.VaultData = reconnect.GameId + ';' + reconnect.Name;

            else if (reconnect.Name.ToLower().Contains("nexusportal"))
                //Connected to a realm: Save the realm 
                ReconnectSettings.Default.LastRealmData = reconnect.GameId + ';' + reconnect.Name;

            else if (reconnect.Name != "")
                //Connected to a dungeon: Save the dungeon
                ReconnectSettings.Default.LastDungeonData = reconnect.GameId + ';' + reconnect.Name;

            ReconnectSettings.Default.Save();
            LoadFromSettings();
        }
    }

    [Serializable]
    public class UnknownArgumentException : Exception
    {
        public UnknownArgumentException() { }
        public UnknownArgumentException(string message) : base(message) { }
        public UnknownArgumentException(string message, Exception inner) : base(message, inner) { }
        protected UnknownArgumentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class CannotLoadSettingsException : Exception
    {
        public CannotLoadSettingsException() { }
        public CannotLoadSettingsException(string message) : base(message) { }
        public CannotLoadSettingsException(string message, Exception inner) : base(message, inner) { }
        protected CannotLoadSettingsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
