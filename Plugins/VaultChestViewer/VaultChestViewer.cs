using LibKRelay;
using LibKRelay.Messages;
using LibKRelay.Messages.Client;
using LibKRelay.Messages.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultChestViewer
{
    public class VaultChestViewer : Plugin
    {
        private int currentGameId;
        private Dictionary<int, int[]> chests;

        public string GetAuthor()
        {
            return "creepylava / ossimc82";
        }

        public string GetName()
        {
            return "Vault Highlight";
        }

        public string GetDescription()
        {
            return "Lost in vault, trying to find empty chest? Look for the indicators added under the chests!";
        }

        public string[] GetCommands()
        {
            return new string[0];
        }

        public override void Initialize(ClientListener listener)
        {
            Message.Hook<Hello>(OnHello);
            Message.Hook<MapInfo>(OnMapInfo);
            Message.Hook<Update>(OnUpdate);
            Message.Hook<NewTick>(OnNewTick);
        }

        private void OnHello(Connection con, Hello hello)
        {
            currentGameId = hello.GameId;
            chests = new Dictionary<int, int[]>();
        }

        private void OnMapInfo(Connection con, MapInfo mapInfo)
        {
            if (currentGameId != -5) return;

            mapInfo.ClientXML.Add(
                    @"	<Objects>
		<Object type=""0x0504"" id=""Vault Chest"">
			<Class>Container</Class>
			<Container/>
			<CanPutNormalObjects/>
			<CanPutSoulboundObjects/>
			<ShowName/>
			<Texture><File>lofiObj2</File><Index>0x0e</Index></Texture>
			<SlotTypes>0, 0, 0, 0, 0, 0, 0, 0</SlotTypes>
		</Object>
	</Objects>");
        }        

        private void OnUpdate(Connection client, Update packet)
        {
            if (currentGameId != -5) return;
            foreach (Entity e in packet.NewObjs.Values)
                if (e.ObjectType == 0x0504) //Vault Chest
                    UpdateStats(e.Status);
        }

        private void OnNewTick(Connection client, NewTick packet)
        {
            if (currentGameId != -5) return;
            foreach (Status s in packet.Statuses.Values)
                if (chests.ContainsKey(s.ObjectId))
                    UpdateStats(s);
        }

        private void UpdateStats(Status stats)
        {
            ParseChestData(stats);

            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                if (chests.ContainsKey(stats.ObjectId) && chests[stats.ObjectId][i] != -1)
                    count++;
            }
            string name = string.Format("Items: {0}/8", count);
            stats.Stats.Add(StatType.Name, new StatData(StatType.Name, name));
        }

        private void ParseChestData(Status stats)
        {
            foreach (var stat in stats.Stats.Values)
            {
                if (stat.Id - StatType.Inventory0 < 8 && stat.Id - StatType.Inventory0 > -1)
                {
                    if (!chests.ContainsKey(stats.ObjectId))
                        chests.Add(stats.ObjectId, new int[8]);

                    chests[stats.ObjectId][stat.Id - StatType.Inventory0] = stat.IntValue;
                }
            }
        }
    }
}
