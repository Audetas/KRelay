using Lib_K_Relay;
using Lib_K_Relay.GameData;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootHelper
{
    class LootState
    {
        public Dictionary<int, int[]> LootBagItems = new Dictionary<int, int[]>();
        public Dictionary<int, Location> LootBagLocations = new Dictionary<int, Location>();
        public int LastLoot = 0;
        public int LastNotif = 0;
        public int CustomQuest = -1;
        public int OriginalQuest = -1;
    }
    public class LootHelper : IPlugin
    {
        private Dictionary<Client, LootState> _states = new Dictionary<Client, LootState>();
        private short[] _desiredBags = { (short)Bags.Blue, (short)Bags.Cyan, (short)Bags.White };

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Loot Helper"; }

        public string GetDescription()
        { return "Allows you to enable loot notifications, loot quests, and autoloot."; }

        public string[] GetCommands()
        { return new string[] { "/loothelper settings" }; }

        public void Initialize(Proxy proxy)
        {
            proxy.ClientConnected += (c) => _states.Add(c, new LootState());
            proxy.ClientDisconnected += (c) => _states.Remove(c);

            proxy.HookPacket(PacketType.MOVE, OnMove);
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.NEWTICK, OnNewTick);
            proxy.HookPacket(PacketType.QUESTOBJID, OnQuestObjId);
            proxy.HookCommand("loothelper", OnLootHelperCommand);
        }

        private void OnLootHelperCommand(Client client, string command, string[] args)
        {
            PluginUtils.ShowGenericSettingsGUI(LootHelperConfig.Default, "Loot Helper Settings");
        }

        private void OnMove(Client client, Packet packet)
        {
            if (!_states.ContainsKey(client)) return;
            LootState state = _states[client];

            foreach (int bagId in state.LootBagItems.Keys)
            {
                float distance = state.LootBagLocations[bagId].DistanceTo(client.PlayerData.Pos);
                if (LootHelperConfig.Default.AutoLoot && Environment.TickCount - state.LastLoot > 900 && distance <= 1)
                {
                    for (int bi = 0; bi < state.LootBagItems[bagId].Length; bi++)
                    {
                        if (state.LootBagItems[bagId][bi] == -1)
                            continue;

                        if (!LootHelperConfig.Default.AutoLootList.Contains(ReverseLookup(state.LootBagItems[bagId][bi])))
                            continue;

                        state.LastLoot = Environment.TickCount;
                        for (int i = 4; i < client.PlayerData.Slot.Length; i++)
                        {
                            if (client.PlayerData.Slot[i] == -1)
                            {
                                InvSwapPacket invSwap = (InvSwapPacket)Packet.Create(PacketType.INVSWAP);
                                invSwap.Time = client.Time + 10;
                                invSwap.Position = client.PlayerData.Pos;

                                invSwap.SlotObject1 = new SlotObject();
                                invSwap.SlotObject1.ObjectId = bagId;
                                invSwap.SlotObject1.SlotId = (byte)bi;
                                invSwap.SlotObject1.ObjectType = state.LootBagItems[bagId][bi];

                                invSwap.SlotObject2 = new SlotObject();
                                invSwap.SlotObject2.ObjectId = client.ObjectId;
                                invSwap.SlotObject2.SlotId = (byte)(i);
                                invSwap.SlotObject2.ObjectType = -1;

                                client.SendToServer(invSwap);
                                break;
                            }
                        }
                    }
                }

                if (LootHelperConfig.Default.LootNotifications && Environment.TickCount - state.LastNotif > 2000 && distance < 15)
                {
                    state.LastNotif = Environment.TickCount;
                    string message = "";

                    foreach (int item in state.LootBagItems[bagId])
                        if (item != -1) message += ReverseLookup(item) + "\\n";

                    if (message.Length > 3)
                        client.SendToClient(PluginUtils.CreateNotification(
                            bagId, LootHelperConfig.Default.NotificationColor.ToArgb(), message));
                }
            }
        }

        private void OnUpdate(Client client, Packet packet)
        {
            LootState state = _states[client];
            UpdatePacket update = (UpdatePacket)packet;
            // New Objects
            foreach (Entity entity in update.NewObjs)
            {
                short type = entity.ObjectType;
                if (_desiredBags.Contains(type))
                {
                    if (LootHelperConfig.Default.LootQuests)
                    {
                        state.CustomQuest = entity.Status.ObjectId;
                        QuestObjIdPacket questObjId = (QuestObjIdPacket)Packet.Create(PacketType.QUESTOBJID);
                        questObjId.ObjectId = entity.Status.ObjectId;
                        client.SendToClient(questObjId);
                    }

                    int bagId = entity.Status.ObjectId;
                    if (!state.LootBagItems.ContainsKey(bagId))
                        state.LootBagItems.Add(bagId, new int[] { -1, -1, -1, -1, -1, -1, -1, -1 });
                    else
                        state.LootBagItems[bagId] = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };

                    if (!state.LootBagLocations.ContainsKey(bagId))
                        state.LootBagLocations.Add(bagId, entity.Status.Position);
                    else
                        state.LootBagLocations[bagId] = entity.Status.Position;

                    foreach (StatData statData in entity.Status.Data)
                    {
                        if (statData.Id >= 8 && statData.Id <= 15)
                            state.LootBagItems[bagId][statData.Id - 8] = statData.IntValue;
                    }
                }
            }

            // Removed Objects
            foreach (int drop in update.Drops)
            {
                if (drop == state.CustomQuest && state.OriginalQuest != -1)
                {
                    QuestObjIdPacket questObjId = (QuestObjIdPacket)Packet.Create(PacketType.QUESTOBJID);
                    questObjId.ObjectId = state.OriginalQuest;
                    client.SendToClient(questObjId);

                    state.OriginalQuest = -1;
                    state.CustomQuest = -1;
                }

                if (state.LootBagItems.ContainsKey(drop))
                {
                    state.LootBagItems.Remove(drop);
                    state.LootBagLocations.Remove(drop);
                }
            }
        }

        private void OnNewTick(Client client, Packet packet)
        {
            LootState state = _states[client];
            NewTickPacket newTick = (NewTickPacket)packet;

            // Updated Objects
            foreach (Status status in newTick.Statuses)
            {
                if (state.LootBagItems.ContainsKey(status.ObjectId))
                {
                    foreach (StatData statData in status.Data)
                    {
                        if (statData.Id >= 8 && statData.Id <= 15)
                            state.LootBagItems[status.ObjectId][statData.Id - 8] = statData.IntValue;
                    }
                }
            }
        }

        private void OnQuestObjId(Client client, Packet packet)
        {
            _states[client].OriginalQuest = (packet as QuestObjIdPacket).ObjectId;
        }

        private string ReverseLookup(int itemId)
        {
            return GameData.Items.ByID((ushort)itemId).Name;
        }
    }
}
