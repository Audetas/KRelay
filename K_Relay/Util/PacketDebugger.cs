using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking;
using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace K_Relay.Util
{
    class PacketDebugger : IPlugin
    {
        private List<PacketType> _reportId = new List<PacketType>()
        {PacketType.USEPORTAL, PacketType.RECONNECT
        };

        private List<PacketType> _printString = new List<PacketType>()
        {
        };

        public string GetAuthor()
        { return "KrazyShank / Kronks"; }

        public string GetName()
        { return "Packet Debugger"; }

        public string GetDescription()
        { return "Helps track what packets have been received."; }

        public string[] GetCommands()
        { return new string[] { }; }

        public void Initialize(Proxy proxy)
        {
            proxy.ClientPacketRecieved += OnPacket;
            proxy.ServerPacketRecieved += OnPacket;
            proxy.HookPacket(PacketType.UPDATE, OnUpdatePacket);
            proxy.HookCommand("test", OnTestCommand);
        }

        private void OnPacket(Client client, Packet packet)
        {
            if (_reportId.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] Received {0} packet.", packet.Type);
            if (_printString.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] {0}", packet);
        }

        private void OnUpdatePacket(Client client, Packet packet)
        {
            UpdatePacket update = (UpdatePacket)packet;

            for (int i = 0; i < update.Tiles.Length; i++)
            {
                update.Tiles[i].Type = Serializer.Tiles["SpiderDirt"];
            }
        }

        private void OnTestCommand(Client client, string command, string[] args)
        {
            Console.WriteLine("Client {0} command {1} args {2}", client.ObjectId, command, args.Length);
        }
    }
}
