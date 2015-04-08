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
        { return new string[] { "/rcon", "/rcon r", "/rcon d", "/rcon v", "/rcon g", "/rcon realm", "/rcon dungeon", "/rcon vault", "/rcon guild", "/rcon ghall" }; }

        public void Initialize(Proxy proxy)
        {
            this.proxy = proxy;

            #region Syntax
            // /r d -- Reconnect to last dungeon
            // /r v -- Reconnect to last vault
            // /r g -- Reconnect to last ghall
            // /r -- Reconnect to last realm
            #endregion

            proxy.ServerPacketRecieved += Proxy_ServerPacketRecieved;
            proxy.HookCommand("/rcon", OnPlayerText);
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

        private void Proxy_ServerPacketRecieved(Client client, Packet packet)
        {
            if (packet.Type == PacketType.RECONNECT)
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
                    ReconnectSettings.Default.LastDungeonData = reconnect.GameId + ";" + reconnect.Name + ";" + reconnect.Host + ";" + reconnect.Port;
                    lastRec = reconnect;
                }

                ReconnectSettings.Default.Save();
                LoadFromSettings();
            }
        }

        private void LoadFromSettings()
        {

            realmRec = LoadPacketFromData(ReconnectSettings.Default.LastRealmData);

            dungRec = LoadPacketFromData(ReconnectSettings.Default.LastDungeonData);

            guildRec = LoadPacketFromData(ReconnectSettings.Default.GuildHallData);

            vaultRec = LoadPacketFromData(ReconnectSettings.Default.VaultData);

        }

        /// <summary>
        /// Loads the packet from the saved data
        /// </summary>
        /// <param name="originalPacket">packet to load from</param>
        /// <param name="packet">packet to load into</param>
        /// <param name="data">Data to load into the packet</param>
        /// <returns>Success/Fail of the operation</returns>
        ReconnectPacket LoadPacketFromData(string data)
        {
            Console.WriteLine(data);
            ReconnectPacket packet = null;
            packet = Packet.Create(PacketType.RECONNECT) as ReconnectPacket;
            packet.GameId = 0;
            packet.Host = "localhost";
            packet.IsFromArena = false;
            packet.Name = "";
            packet.Port = 2050;
            packet.Key = new byte[0];
            packet.KeyTime = 0;

            string[] dataSplit = data.Split(';');

            if (dataSplit.Length != 2 && dataSplit.Length != 4)
                return packet;

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

                return packet;
            }

            else
                return packet;
        }

        private void OnPlayerText(Client client, string command, string[] args)
        {
            ReconnectPacket reconnectPacket;
            reconnectPacket = Packet.Create(PacketType.RECONNECT) as ReconnectPacket;
            reconnectPacket.GameId = 0;
            reconnectPacket.Host = "localhost";
            reconnectPacket.IsFromArena = false;
            reconnectPacket.Name = "";
            reconnectPacket.Port = 2050;
            reconnectPacket.Key = new byte[0];
            reconnectPacket.KeyTime = 0;

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


                //Console.WriteLine(reconnectPacket.ToString());
                proxy.RemoteAddress = reconnectPacket.Host;
                proxy.Port = reconnectPacket.Port;
                reconnectPacket.Host = "localhost";
                reconnectPacket.Port = 2050;
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
