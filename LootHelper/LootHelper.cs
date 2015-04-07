using Lib_K_Relay;
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
    public class LootHelper : IPlugin
    {
        private Dictionary<int, int[]> _lootBagItems = new Dictionary<int, int[]>();
        private Dictionary<int, Location> _lootBagLocations = new Dictionary<int, Location>();

        private int _originalQuest = -1;
        private int _customQuest = -1;

        private int _cooldown = 0;
        private int _notifCooldown = 0;

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
            proxy.HookPacket(PacketType.MOVE, OnMove);
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.NEW_TICK, OnNewTick);
            proxy.HookPacket(PacketType.QUESTOBJID, OnQuestObjId);
            proxy.HookCommand("loothelper", OnLootHelperCommand);
        }

        private void OnLootHelperCommand(Client client, string command, string[] args)
        {
            PluginUtils.ShowGenericSettingsGUI(LootHelperConfig.Default, "Loot Helper Settings");
        }

        private void OnMove(Client client, Packet packet)
        {
            foreach (int bagId in _lootBagItems.Keys)
            {
                float distanceSquared = _lootBagLocations[bagId].DistanceSquaredTo(client.PlayerData.Pos);
                if (LootHelperConfig.Default.AutoLoot && _cooldown == 0 && distanceSquared <= 2.5)
                {
                    for (int bi = 0; bi < _lootBagItems[bagId].Length; bi++)
                    {
                        if (_lootBagItems[bagId][bi] == -1)
                            continue;

                        if (!LootHelperConfig.Default.AutoLootList.Contains(ReverseLookup(_lootBagItems[bagId][bi])))
                            continue;

                        _cooldown = LootHelperConfig.Default.AutoLootRetryDelay;
                        for (int i = 4; i < client.PlayerData.Slot.Length; i++)
                        {
                            if (client.PlayerData.Slot[i] == -1)
                            {
                                Console.WriteLine("[Loot Helper] Swapping bagslot {0} with my slot {1}", bi, i - 4);
                                InvSwapPacket invSwap = (InvSwapPacket)Packet.Create(PacketType.INVSWAP);
                                invSwap.Time = client.Time;
                                invSwap.Position = client.PlayerData.Pos;

                                invSwap.SlotObject1 = new SlotObject();
                                invSwap.SlotObject1.ObjectId = bagId;
                                invSwap.SlotObject1.SlotId = (byte)bi;
                                invSwap.SlotObject1.ObjectType = (short)_lootBagItems[bagId][bi];

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
            }

            if (_cooldown != 0) return;
            else _cooldown = 15; // So we dont spam notifs

            foreach (int bagId in _lootBagItems.Keys)
            {
                float distanceSquared = _lootBagLocations[bagId].DistanceSquaredTo(client.PlayerData.Pos);
                if (LootHelperConfig.Default.LootNotifications && distanceSquared < 200)
                {
                    string message = "";

                    foreach (int item in _lootBagItems[bagId])
                        if (item != -1) message += ReverseLookup(item) + "\\n";

                    if (message.Length > 3)
                        client.SendToClient(PluginUtils.CreateNotification(
                            bagId, LootHelperConfig.Default.NotificationColor.ToArgb(), message));
                }
            }
        }

        private void OnUpdate(Client client, Packet packet)
        {
            if (_cooldown > 0) _cooldown--;
            if (_notifCooldown > 0) _notifCooldown--;

            UpdatePacket update = (UpdatePacket)packet;
            // New Objects
            foreach (Entity entity in update.NewObjs)
            {
                int type = entity.ObjectType;

                if (LootHelperConfig.Default.LootQuests && type >= 1286 && type <= 1296) // Light Blue or better
                {
                    Console.WriteLine("[Loot Helper] Changing your quest to a loot bag!");
                    QuestObjIdPacket questObjId = (QuestObjIdPacket)Packet.Create(PacketType.QUESTOBJID);
                    questObjId.ObjectId = entity.Status.ObjectId;
                    client.SendToClient(questObjId);
                }

                if (type == 1280 || type == 1283 || (type >= 1286 && type <= 1296))
                {
                    // Is a loot bag
                    int bagId = entity.Status.ObjectId;

                    if (!_lootBagItems.ContainsKey(bagId))
                        _lootBagItems.Add(bagId, new int[] { -1, -1, -1, -1, -1, -1, -1, -1});
                    else
                        _lootBagItems[bagId] = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };

                    if (!_lootBagLocations.ContainsKey(bagId))
                        _lootBagLocations.Add(bagId, entity.Status.Position);
                    else
                        _lootBagLocations[bagId] = entity.Status.Position;

                    foreach (StatData statData in entity.Status.Data)
                    {
                        if (statData.Id >= 8 && statData.Id <= 15)
                            _lootBagItems[bagId][statData.Id - 8] = statData.IntValue;
                    }
                }
            }

            // Removed Objects
            foreach (int drop in update.Drops)
            {
                if (drop == _customQuest && _originalQuest != -1)
                {
                    QuestObjIdPacket questObjId = (QuestObjIdPacket)Packet.Create(PacketType.QUESTOBJID);
                    questObjId.ObjectId = _originalQuest;
                    client.SendToClient(questObjId);

                    _originalQuest = -1;
                    _customQuest = -1;
                }

                if (LootHelperConfig.Default.UpdateBagContentsCPUIntensive)
                {
                    if (_lootBagItems.ContainsKey(drop))
                    {
                        _lootBagItems.Remove(drop);
                        _lootBagLocations.Remove(drop);
                    }
                }
            }
        }

        private void OnNewTick(Client client, Packet packet)
        {
            NewTickPacket newTick = (NewTickPacket)packet;

            // Updated Objects
            foreach (Status status in newTick.Statuses)
            {
                if (_lootBagItems.ContainsKey(status.ObjectId))
                {
                    foreach (StatData statData in status.Data)
                    {
                        if (statData.Id >= 8 && statData.Id <= 15)
                            _lootBagItems[status.ObjectId][statData.Id - 8] = statData.IntValue;
                    }
                }
            }
        }

        private void OnQuestObjId(Client client, Packet packet)
        {
            _originalQuest = (packet as QuestObjIdPacket).ObjectId;
        }

        private string ReverseLookup(int itemId)
        {
            foreach (var pair in Serializer.Items)
                if (pair.Value == itemId) return pair.Key;

            return itemId.ToString();
        }
    }
}
