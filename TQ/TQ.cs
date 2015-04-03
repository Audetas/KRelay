using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQ
{
    public class TQ : IPlugin
    {
        private int _questId = -1;
        private Location _questLocation = null;
        private Dictionary<int, Location> _playerLocations;

        private short[] _classes = new short[]
        {
            768, 775, 782, 784, 797, 798, 799,
            800, 801, 802, 803, 804, 805, 806
        };

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Teleport To Quest (TQ)"; }

        public string GetDescription()
        { return "Teleport to the player closest to your current quest using /tq."; }

        public string[] GetCommands()
        { return new string[] { "/tq" }; }

        public void Initialize(Proxy proxy)
        {
            _playerLocations = new Dictionary<int, Location>();
            proxy.HookPacket(PacketType.NEW_TICK, OnNewTick);
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.QUESTOBJID, OnQuestObjId);
            proxy.HookCommand("tq", OnTQCommand);
        }

        private void OnQuestObjId(Client client, Packet packet)
        { _questId = (packet as QuestObjIdPacket).ObjectId; }

        private void OnUpdate(Client client, Packet packet)
        {
            UpdatePacket update = (UpdatePacket)packet;
            // New Objects
            for (int i = 0; i < update.NewObjs.Length; i++)
            {
                Entity entity = update.NewObjs[i];
                
                if (_classes.Contains(entity.ObjectType))
                    _playerLocations.Add(entity.Status.ObjectId, entity.Status.Position);
                else if (entity.Status.ObjectId == _questId)
                    _questLocation = entity.Status.Position;
            }

            // Removed Objects
            for (int i = 0; i < update.Drops.Length; i++)
            {
                int droppedId = update.Drops[i];

                if (_playerLocations.ContainsKey(droppedId))
                    _playerLocations.Remove(droppedId);
                else if (droppedId == _questId)
                {
                    _questId = -1;
                    _questLocation = null;
                }
            }
        }

        private void OnNewTick(Client client, Packet packet)
        {
            // Update player positions
            NewTickPacket newTick = (NewTickPacket)packet;
            for (int i = 0; i < newTick.Statuses.Length; i++)
            {
                Status status = newTick.Statuses[i];
                _playerLocations[status.ObjectId] = status.Position;
            }
        }

        private void OnTQCommand(Client client, string command, string[] args)
        {
            if (_questId == -1 || _questLocation == null) return;

            float minDist = _questLocation.DistanceSquaredTo(_playerLocations[client.ObjectId]);
            int toTp = client.ObjectId;

            foreach (var pair in _playerLocations)
            {
                float distToQuest = _questLocation.DistanceSquaredTo(pair.Value);
                if (distToQuest < minDist)
                {
                    minDist = distToQuest;
                    toTp = pair.Key;
                }
            }

            if (toTp != client.ObjectId)
            {
                TeleportPacket teleport = (TeleportPacket)Packet.CreateInstance(PacketType.TELEPORT);
                teleport.ObjectId = toTp;
                client.SendToServer(teleport);
            }
        }
    }
}
