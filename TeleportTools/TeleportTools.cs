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

namespace TeleportTools
{
    class TeleportState
    {
        public int QuestId = -1;
        public Location QuestLocation = null;
        public Dictionary<int, string> PlayerNames = new Dictionary<int, string>();
        public Dictionary<int, Location> PlayerLocations = new Dictionary<int, Location>();
    }

    public class TeleportTools : IPlugin
    {
        private Dictionary<Client, TeleportState> _states = new Dictionary<Client, TeleportState>();
        private short[] _classes;

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Teleport Tools"; }

        public string GetDescription()
        { return "Teleport to the player closest to your current quest using /tq." +
                 "Teleport to a player without typing their full name with /tp"; }

        public string[] GetCommands()
        { return new string[] { "/tq", "/tp <partial name>"}; }

        public void Initialize(Proxy proxy)
        {
            _classes = (short[])Enum.GetValues(typeof(Classes));

            proxy.ClientConnected += (c) => _states.Add(c, new TeleportState());
            proxy.ClientDisconnected += (c) => _states.Remove(c);

            proxy.HookPacket(PacketType.NEWTICK, OnNewTick);
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.QUESTOBJID, OnQuestObjId);
            proxy.HookCommand("tq", OnTQCommand);
            proxy.HookCommand("tp", OnTPCommand);
        }

        private void OnQuestObjId(Client client, Packet packet)
        {
            _states[client].QuestId = (packet as QuestObjIdPacket).ObjectId; 
        }

        private void OnUpdate(Client client, Packet packet)
        {
            UpdatePacket update = (UpdatePacket)packet;
            TeleportState state = _states[client];
            // New Objects
            foreach (Entity entity in update.NewObjs)
            {
                if (_classes.Contains(entity.ObjectType))
                {
                    state.PlayerLocations.Add(entity.Status.ObjectId, entity.Status.Position);
                    foreach (StatData statData in entity.Status.Data)
                    {
                        if (statData.Id == 31)
                            state.PlayerNames.Add(entity.Status.ObjectId, statData.StringValue);
                    }
                }
                else if (entity.Status.ObjectId == state.QuestId)
                {
                    state.QuestLocation = entity.Status.Position;
                }
            }

            // Removed Objects
            foreach (int drop in update.Drops)
            {
                if (state.PlayerLocations.ContainsKey(drop))
                {
                    state.PlayerLocations.Remove(drop);
                    state.PlayerNames.Remove(drop);
                }
            }
        }

        private void OnNewTick(Client client, Packet packet)
        {
            // Update player positions
            NewTickPacket newTick = (NewTickPacket)packet;
            TeleportState state = _states[client];
            foreach (Status status in newTick.Statuses)
            {
                if (state.PlayerLocations.ContainsKey(status.ObjectId))
                    state.PlayerLocations[status.ObjectId] = status.Position;
                else if (status.ObjectId == state.QuestId)
                    state.QuestLocation = status.Position;
            }
        }

        private void OnTQCommand(Client client, string command, string[] args)
        {
            TeleportState state = _states[client];
            if (state.QuestId != -1 && state.QuestLocation != null)
            {
                float minDistance = state.QuestLocation.DistanceSquaredTo(client.PlayerData.Pos);
                int target = client.ObjectId;

                foreach (var pair in state.PlayerLocations)
                {
                    float distance = pair.Value.DistanceSquaredTo(state.QuestLocation);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = pair.Key;
                    }
                }

                if (target != client.ObjectId)
                {
                    TeleportPacket teleport = (TeleportPacket)Packet.Create(PacketType.TELEPORT);
                    teleport.ObjectId = target;
                    client.SendToServer(teleport);
                    client.SendToClient(PluginUtils.CreateNotification(
                        client.ObjectId, "Teleported to " + state.PlayerNames[target]));
                }
                else
                {
                    client.SendToClient(PluginUtils.CreateNotification(
                        client.ObjectId, "You're the closest to your quest!"));
                }
            }
        }

        private void OnTPCommand(Client client, string command, string[] args)
        {
            TeleportState state = _states[client];
            if (args.Length == 0) return;

            foreach (var pair in state.PlayerNames)
            {
                if (pair.Value.ToLower().Contains(args[0].ToLower()))
                {
                    TeleportPacket teleport = (TeleportPacket)Packet.Create(PacketType.TELEPORT);
                    teleport.ObjectId = pair.Key;
                    client.SendToServer(teleport);
                    client.SendToClient(PluginUtils.CreateNotification(
                        client.ObjectId, "Teleported to " + pair.Value));
                    return; ;
                }
            }

            client.SendToClient(PluginUtils.CreateNotification(
                client.ObjectId, "Player with \"" + args[0] + "\" in their name was not found!"));
        }
    }
}
