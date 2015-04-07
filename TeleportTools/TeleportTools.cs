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
    public class TeleportTools : IPlugin
    {
        private int _questId = -1;
        private Location _questLocation = null;
        private Dictionary<int, string> _playerNames = new Dictionary<int,string>();
        private Dictionary<int, Location> _playerLocations = new Dictionary<int, Location>();

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

            proxy.ClientDisconnected += OnClientDisconnected;
            proxy.HookPacket(PacketType.NEWTICK, OnNewTick);
            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.QUESTOBJID, OnQuestObjId);
            proxy.HookCommand("tq", OnTQCommand);
            proxy.HookCommand("tp", OnTPCommand);
        }

        private void OnClientDisconnected(Client client)
        {
            _playerNames.Clear();
            _playerLocations.Clear();
            _questId = -1;
            _questLocation = null;
        }

        private void OnQuestObjId(Client client, Packet packet)
        {
            _questId = (packet as QuestObjIdPacket).ObjectId; 
        }

        private void OnUpdate(Client client, Packet packet)
        {
            UpdatePacket update = (UpdatePacket)packet;
            // New Objects
            foreach (Entity entity in update.NewObjs)
            {
                if (_classes.Contains(entity.ObjectType))
                {
                    _playerLocations.Add(entity.Status.ObjectId, entity.Status.Position);
                    foreach (StatData statData in entity.Status.Data)
                    {
                        if (statData.Id == 31)
                            _playerNames.Add(entity.Status.ObjectId, statData.StringValue);
                    }
                }
                else if (entity.Status.ObjectId == _questId)
                {
                    _questLocation = entity.Status.Position;
                }
            }

            // Removed Objects
            foreach (int drop in update.Drops)
            {
                if (_playerLocations.ContainsKey(drop))
                {
                    _playerLocations.Remove(drop);
                    _playerNames.Remove(drop);
                }
            }
        }

        private void OnNewTick(Client client, Packet packet)
        {
            // Update player positions
            NewTickPacket newTick = (NewTickPacket)packet;
            foreach (Status status in newTick.Statuses)
            {
                if (_playerLocations.ContainsKey(status.ObjectId))
                    _playerLocations[status.ObjectId] = status.Position;
                else if (status.ObjectId == _questId)
                    _questLocation = status.Position;
            }
        }

        private void OnTQCommand(Client client, string command, string[] args)
        {
            if (_questId != -1 && _questLocation != null)
            {
                float minDistance = _questLocation.DistanceSquaredTo(client.PlayerData.Pos);
                int target = client.ObjectId;

                foreach (var pair in _playerLocations)
                {
                    float distance = pair.Value.DistanceSquaredTo(_questLocation);
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
                        client.ObjectId, "Teleported to " + _playerNames[target]));
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
            if (args.Length == 0) return;

            foreach (var pair in _playerNames)
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
