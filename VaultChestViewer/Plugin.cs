using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultChestViewer
{
    public class Plugin : IPlugin
    {
        private int m_currentGameId;
        private Dictionary<int, int[]> m_chests;

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

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket<HelloPacket>(OnHelloPacket);
            proxy.HookPacket<MapInfoPacket>(OnMapInfoPacket);
            proxy.HookPacket<UpdatePacket>(OnUpdatePacket);
            proxy.HookPacket<NewTickPacket>(OnNewTickPacket);
        }

        private void OnHelloPacket(Client client, HelloPacket packet)
        {
            this.m_currentGameId = packet.GameId;
            this.m_chests = new Dictionary<int, int[]>();
        }

        private void OnMapInfoPacket(Client client, MapInfoPacket packet)
        {
            if (this.m_currentGameId != -5) return;

            packet.ClientXML = packet.ClientXML.Concat(new[]
				{
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
	</Objects>"
				}).ToArray();
        }        

        private void OnUpdatePacket(Client client, UpdatePacket packet)
        {
            if (this.m_currentGameId != -5) return;
            for (int i = 0; i < packet.NewObjs.Length; i++)
                if (packet.NewObjs[i].ObjectType == 0x0504) //Vault Chest
                    UpdateStats(ref packet.NewObjs[i].Status);
        }

        private void OnNewTickPacket(Client client, NewTickPacket packet)
        {
            if (this.m_currentGameId != -5) return;
            for (int i = 0; i < packet.Statuses.Length; i++)
                if (this.m_chests.ContainsKey(packet.Statuses[i].ObjectId))
                    UpdateStats(ref packet.Statuses[i]);
        }

        private void UpdateStats(ref Status stats)
        {
            ParseChestData(stats);

            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                if (this.m_chests.ContainsKey(stats.ObjectId) && this.m_chests[stats.ObjectId][i] != -1)
                    count++;
            }
            string name = string.Format("Items: {0}/8", count);
            stats.Data = stats.Data.Concat(new StatData[]
			{
				new StatData 
                {
                     Id = StatsType.Name,
                     StringValue = name
                }
			}).ToArray();
        }

        private void ParseChestData(Status stats)
        {
            foreach (var stat in stats.Data)
            {
                if (stat.Id - StatsType.Inventory0 < 8 && stat.Id - StatsType.Inventory0 > -1)
                {
                    if (!this.m_chests.ContainsKey(stats.ObjectId))
                        this.m_chests.Add(stats.ObjectId, new int[8]);

                    this.m_chests[stats.ObjectId][stat.Id - StatsType.Inventory0] = stat.IntValue;
                }
            }
        }
    }
}
