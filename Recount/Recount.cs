using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recount
{
    public class Recount : IPlugin
    {
        private short[] _classes;

        public Dictionary<int, string> PlayerNames = new Dictionary<int, string>();
        private Dictionary<int, Location> _playerLocations = new Dictionary<int, Location>();

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Recount"; }

        public string GetDescription()
        { return "Live DPS meter for your and the people around you."; }

        public string[] GetCommands()
        { return new string[] { "/dps" }; }

        public void Initialize(Proxy proxy)
        {
            _classes = (short[])Enum.GetValues(typeof(Classes));

            proxy.HookPacket(PacketType.UPDATE, OnUpdate);
            proxy.HookPacket(PacketType.NEWTICK, OnNewTick);
            proxy.HookCommand("dps", OnDPSCommand);
        }

        private void OnDPSCommand(Client client, string command, string[] args)
        {
            PluginUtils.ShowGUI(new FrmRecount(this));
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
                        if (statData.Id == 31)
                            PlayerNames.Add(entity.Status.ObjectId, statData.StringValue);
                }
            }
        }

        private void OnNewTick(Client client, Packet packet)
        {
            // Update player positions
            NewTickPacket newTick = (NewTickPacket)packet;
            foreach (Status status in newTick.Statuses)
                if (_playerLocations.ContainsKey(status.ObjectId))
                    _playerLocations[status.ObjectId] = status.Position;
        }
    }
}
