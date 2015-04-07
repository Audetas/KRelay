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

namespace ClientStatAnnouncer
{
    public class ClientStatAnnouncer : IPlugin
    {
        public string GetAuthor()
        { return "KrazyShank / Kronks & Alde"; }

        public string GetName()
        { return "ClientStat Announcer"; }

        public string GetDescription()
        { return "Lets you know when you progress on in-game achievements."; }

        public string[] GetCommands()
        { return new string[] { }; }

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket(PacketType.CLIENTSTAT, OnClientStat);
        }

        private void OnClientStat(Client client, Packet packet)
        {
            ClientStatPacket clientStat = (ClientStatPacket)packet;

            string toDisplay = clientStat.Name + "  has increased to  " + clientStat.Value;


            if (clientStat.Name.Equals("Shots")) //0: 'Shots',
            {
                toDisplay = "Bullets shot : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("ShotsThatDamage")) //1: 'ShotsThatDamage',
            {
                toDisplay = "Bullets that damaged : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("SpecialAbilityUses")) //2: 'SpecialAbilityUses',
            {
                toDisplay = "Ability uses : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("TilesUncovered")) //3: 'TilesUncovered',
            {
                toDisplay = "Tiles uncovered : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("Teleports")) //4: 'Teleports',
            {
                toDisplay = "Teleports : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("PotionsDrunk")) //5: 'PotionsDrunk',
            {
                toDisplay = "Potions drank : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("MonsterKills")) //6: 'MonsterKills',
            {
                toDisplay = "Monster kills : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("MonsterAssists")) //7: 'MonsterAssists',
            {
                toDisplay = "Monster assists : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("GodKills")) //8: 'GodKills',
            {
                toDisplay = "God kills : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("GodAssists")) //9: 'GodAssists',
            {
                toDisplay = "God assists : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("CubeKills")) //10: 'CubeKills',
            {
                toDisplay = "Cube kills : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("OryxKills")) //11: 'OryxKills',
            {
                toDisplay = "Oryx kills : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("QuestsCompleted")) //12: 'QuestsCompleted',
            {
                toDisplay = "Quests completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("PirateCavesCompleted")) //13: 'PirateCavesCompleted',
            {
                toDisplay = "Pirate Cave(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("UndeadLairsCompleted")) //14: 'UndeadLairsCompleted',
            {
                toDisplay = "Undead Lair(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("AbyssOfDemonsCompleted")) //15: 'AbyssOfDemonsCompleted',
            {
                toDisplay = "Abyss of Demon(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("SnakePitsCompleted")) //16: 'SnakePitsCompleted',
            {
                toDisplay = "Snake Pit(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("SpiderDensCompleted")) //17: 'SpiderDensCompleted',
            {
                toDisplay = "Spider Den(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("SpriteWorldsCompleted")) //18: 'SpriteWorldsCompleted',
            {
                toDisplay = "Sprite World(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("LevelUpAssists")) //19: 'LevelUpAssists',
            {
                toDisplay = "Level-up assist(s) : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("MinutesActive")) //20: 'MinutesActive',
            {
                toDisplay = "Minute(s) active : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("TombsCompleted")) //21: 'TombsCompleted',
            {
                toDisplay = "Tomb(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("TrenchesCompleted")) //22: 'TrenchesCompleted',
            {
                toDisplay = "Trenche(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("JunglesCompleted")) //23: 'JunglesCompleted',
            {
                toDisplay = "Jungle(s) completed : " + clientStat.Value;
            }
            else if (clientStat.Name.Equals("ManorsCompleted")) //24: 'ManorsCompleted',
            {
                toDisplay = "Manor(s) completed : " + clientStat.Value;
            }
            else
            {
                //print(toDisplay = "Unknown -> Name :" + clientStat.Name + " Value :" + clientStat.Value);
            }


            client.SendToClient(
                PluginUtils.CreateOryxNotification(
                    "ClientStat Announcer", toDisplay));
        }
    }
}
