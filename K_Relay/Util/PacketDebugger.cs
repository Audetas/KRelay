using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Relay.Util
{
    class PacketDebugger : IPlugin
    {
        private List<PacketType> _reportId = new List<PacketType>()
        {

        };

        private List<PacketType> _printString = new List<PacketType>()
        {
            PacketType.NEW_TICK
        };

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Packet Debugger"; }

        public string GetDescription()
        { return "Helps track what packets have been received."; }

        public void Initialize(Proxy proxy)
        {
            proxy.ClientPacketRecieved += OnPacket;
            proxy.ServerPacketRecieved += OnPacket;
        }

        private void OnPacket(Proxy proxy, ClientInstance client, Packet packet)
        {
            if (_reportId.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] Received {0} packet.", packet.Type);
            if (_printString.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] {0}", packet);
        }
    }
}
