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


namespace Reconnect
{

    public class Reconnect : IPlugin
    {

        //..\K_Relay\bin\Debug\Plugins\Reconnect\

        /*List<string> Realms = new List<string>() { "NexusPortal.Bat", "NexusPortal.Beholder", "NexusPortal.Blob", "NexusPortal.Chimera", "NexusPortal.Cube", "NexusPortal.Cyclops", "NexusPortal.Deathmage", "NexusPortal.Demon", "NexusPortal.Djinn", "NexusPortal.Dragon", "NexusPortal.Drake", "NexusPortal.Flayer", "NexusPortal.Gargoyle", "NexusPortal.Ghost", "NexusPortal.Giant", "NexusPortal.Goblin", "NexusPortal.Golem", "NexusPortal.Gorgon", "NexusPortal.Harpy", "NexusPortal.Hobbit", "NexusPortal.Hydra", "NexusPortal.Imp", "NexusPortal.Kraken", "NexusPortal.Leviathan", "NexusPortal.Lich", "NexusPortal.Medusa", "NexusPortal.Minotaur", "NexusPortal.Mummy", "NexusPortal.Ogre", "NexusPortal.Orc", "NexusPortal.Phoenix", "NexusPortal.Pirate", "NexusPortal.Reaper", "NexusPortal.Satyr", "NexusPortal.Scorpion", "NexusPortal.Skeleton", "NexusPortal.Slime", "NexusPortal.Snake", "NexusPortal.Spectre", "NexusPortal.Spider", "NexusPortal.Sprite", "NexusPortal.Titan", "NexusPortal.Unicorn", "NexusPortal.Wyrm" };
        */

        public string help = "Usage: /<reconnect command> [option] \n";

        Proxy proxy;
        ReconnectPacket vaultRec, dungRec, guildRec, realmRec, lastRec, defaultRecPacket;

        public string GetAuthor()
        { return "TheMrNobody"; }

        public string GetName()
        { return "Map Reconnect"; }

        public string GetDescription()
        {
            return "Reconnects you to the last realm/dungeon you went to.\n" +
                   "Usage: /<reconnect command> [option] \n" +
                   "Reconnect commands: /r, /reconect \n" +
                   "Options: r, realm, d, dungeon, g, ghall, guild, v, vault.\n" +
                   "Without any options, you will connect to the last realm/dungeon you visited\n" +
                   "Examples: /r g; /r dungeon; /reconnect";
        }

        public string[] GetCommands()
        { return new string[] { "/r", "/r r", "/r d", "/r v", "/r g", "/reconnect", "/reconnect r", "/reconnect d", "/reconnect g", "/reconnect v", "/reconnect realm", "/reconnect dungeon", "/reconnect vault", "/reconnect guild", "/reconnect ghall" }; }

        public void Initialize(Proxy proxy)
        {
            this.proxy = proxy;
            proxy.HookPacket(PacketType.RECONNECT, OnReconnect);

            #region Syntax
            // /r d -- Reconnect to last dungeon
            // /r v -- Reconnect to last vault
            // /r g -- Reconnect to last ghall
            // /r -- Reconnect to last realm
            #endregion
            proxy.HookCommand("/rc", OnPlayerText);
            proxy.HookCommand("/reconnect", OnPlayerText);

            //Initialise the reconnectPackets
            defaultRecPacket = Packet.Create(PacketType.RECONNECT) as ReconnectPacket;
            defaultRecPacket.GameId = 0;
            defaultRecPacket.Host = "localhost";
            defaultRecPacket.IsFromArena = false;
            defaultRecPacket.Name = "";
            defaultRecPacket.Port = 2050;
            defaultRecPacket.Key = new byte[0];
            defaultRecPacket.KeyTime = 0;

            vaultRec = defaultRecPacket; dungRec = defaultRecPacket; guildRec = defaultRecPacket; realmRec = defaultRecPacket; lastRec = defaultRecPacket;

            LoadFromSettings();
        }

        private void LoadFromSettings()
        {
            bool initializedFine = true;

            Tuple<bool, ReconnectPacket> tempTuple;

            tempTuple = LoadPacketFromData(ReconnectSettings.Default.LastRealmData);
            if (!tempTuple.Item1)
                initializedFine = false;
            realmRec = tempTuple.Item2;

            tempTuple = LoadPacketFromData(ReconnectSettings.Default.LastDungeonData);
            if (!tempTuple.Item1)
                initializedFine = false;
            dungRec = tempTuple.Item2;

            tempTuple = LoadPacketFromData(ReconnectSettings.Default.GuildHallData);
            if (!tempTuple.Item1)
                initializedFine = false; realmRec = tempTuple.Item2;
            guildRec = tempTuple.Item2;

            tempTuple = LoadPacketFromData(ReconnectSettings.Default.VaultData);
            if (!tempTuple.Item1)
                initializedFine = false;
            vaultRec = tempTuple.Item2;

            Console.WriteLine(ReconnectSettings.Default.LastRealmData);
            Console.WriteLine(ReconnectSettings.Default.LastDungeonData);
            Console.WriteLine(ReconnectSettings.Default.GuildHallData);
            Console.WriteLine(ReconnectSettings.Default.VaultData);


            if (!initializedFine)
                Console.WriteLine("[Reconnect] Reconnect could not load some of the settings");
        }

        /// <summary>
        /// Loads the packet from the saved data
        /// </summary>
        /// <param name="originalPacket">packet to load from</param>
        /// <param name="packet">packet to load into</param>
        /// <param name="data">Data to load into the packet</param>
        /// <returns>Success/Fail of the operation</returns>
        Tuple<bool, ReconnectPacket> LoadPacketFromData(string data)
        {
            ReconnectPacket packet = null;
            packet = defaultRecPacket;

            string[] dataSplit = data.Split(';');

            if (dataSplit.Length != 2 && dataSplit.Length != 4)
                return new Tuple<bool, ReconnectPacket>(false, packet);

            int recId = 0;
            string realmName = "";

            if (int.TryParse(dataSplit[0], out recId))
            {
                realmName = dataSplit[1];

                packet.Name = realmName;
                packet.GameId = recId;

                if (dataSplit.Length == 4)
                {
                    packet.Host = dataSplit[2];
                    packet.Port = int.Parse(dataSplit[3]);
                }

                return new Tuple<bool, ReconnectPacket>(true, packet);
            }

            else
                return new Tuple<bool, ReconnectPacket>(false, packet);
        }

        private void OnPlayerText(Client client, string command, string[] args)
        {
            ReconnectPacket reconnectPacket = defaultRecPacket;

            try
            {
                if (args.Length == 1)
                {
                    string argument = args[0];
                    switch (argument)
                    {
                        case "r":
                        case "realm":
                            reconnectPacket = realmRec;
                            break;
                        case "dungeon":
                        case "d":
                            reconnectPacket = dungRec;
                            break;
                        case "vault":
                        case "v":
                            reconnectPacket = vaultRec;
                            break;
                        case "guild":
                        case "ghall":
                        case "g":
                            reconnectPacket = guildRec;
                            break;
                        default:
                            throw new UnknownArgumentException();
                    }
                }
                else if (args.Length == 0)
                    reconnectPacket = lastRec;
                else
                    throw new UnknownArgumentException("Too Many arguments");


                Console.WriteLine(reconnectPacket.ToString());
                client.SendToClient(reconnectPacket);

            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(UnknownArgumentException))
                {
                    client.SendToClient(PluginUtils.CreateOryxNotification("Reconnect Plugin", help));
                }
                else
                {
                    //Send a message saying we found a problem
                    Console.WriteLine("Reconnect ran into a problem " + ex.Message);
                }
            }


        }

        private void OnReconnect(Client client, Packet packet)
        {
            ReconnectPacket reconnect = packet as ReconnectPacket;
            Console.WriteLine(reconnect.ToString());

            if (reconnect.Name.ToLower() == "guild hall")
                //Connected to guild hall: save the guild hall
                ReconnectSettings.Default.GuildHallData = reconnect.GameId + ";" + reconnect.Name;

            else if (reconnect.Name.ToLower().Contains("nexusportal"))
            //Connected to a realm: Save the realm 
            {
                ReconnectSettings.Default.LastRealmData = reconnect.GameId + ";" + reconnect.Name + ";" + reconnect.Host + ";" + reconnect.Port;
                lastRec = reconnect;
            }

            else if (reconnect.Name != "" && !reconnect.Name.Contains("vault"))
            //Connected to a dungeon: Save the dungeon
            {
                ReconnectSettings.Default.LastDungeonData = reconnect.GameId + ";" + reconnect.Name  + ";" + reconnect.Host + ";" + reconnect.Port;
                lastRec = reconnect;
            }

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

