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

        public void Initialize(Proxy proxy)
        {
            proxy.ClientPacketRecieved += OnPacket;
            proxy.ServerPacketRecieved += OnPacket;
            proxy.HookPacket(PacketType.UPDATE, OnUpdatePacket);
        }

        private void OnPacket(ClientInstance client, Packet packet)
        {
            if (_reportId.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] Received {0} packet.", packet.Type);
            if (_printString.Contains(packet.Type)) Console.WriteLine("[Packet Debugger] {0}", packet);

            if (_reportId.Contains(packet.Type))
            {
                string text = "";
                switch (packet.Type)
                {
                    /*case PacketType.HELLO:
                        HelloPacket hPacket = packet as HelloPacket;
                        text = string.Format("Packet Structure: BuildVersion : {0}, GameId: {1}, GUID: {2}, Id: {3}, Key: {4}, KeyTime: {5}, Obf1: {6}, Obf2 :{7}, Obf3: {8}, Obf4: {9}, Obf5: {10}, Obf6: {11}, Password: {12}, Random1: {13}, Random2: {14}, Secret: {15}.", hPacket.BuildVersion, hPacket.GameId, hPacket.GUID, hPacket.Id, hPacket.Key, hPacket.KeyTime, hPacket.Obf1, hPacket.Obf2, hPacket.Obf3, hPacket.Obf4, hPacket.Obf5, hPacket.Obf6, hPacket.Password, hPacket.Random1, hPacket.Random2, hPacket.Secret, hPacket.Send);
                        break;
                        */
                    case PacketType.RECONNECT:
                        ReconnectPacket rPacket = packet as ReconnectPacket;

                        string bytestringified = "";
                        foreach (byte eiii in rPacket.Key)
                            bytestringified += ", " + eiii;

                        text = string.Format("Packet Structure: GameId: {0}, Host: {1}, Id: {2}, IsFromArena: {3}, Key: {4}, KeyTime: {5}, Name: {6}, Port: {7}, Send: {8}", rPacket.GameId, rPacket.Host, rPacket.Id, rPacket.IsFromArena, bytestringified, rPacket.KeyTime, rPacket.Name, rPacket.Port, rPacket.Send);
                        break;
                    case PacketType.USEPORTAL:
                        UsePortalPacket uPacket = packet as UsePortalPacket;
                        text = string.Format("Packet Structure: Id: {0}, ObjectId: {1}, Send: {2}", uPacket.Id, uPacket.ObjectId, uPacket.Send);
                        break;
                }
                Console.WriteLine("[Packet Debugger] " + text);
            }

        }

        private void OnUpdatePacket(ClientInstance client, Packet packet)
        {
            UpdatePacket update = (UpdatePacket)packet;

            for (int i = 0; i < update.Tiles.Length; i++)
            {
                update.Tiles[i].Type = Serializer.Tiles["SpiderDirt"];
            }
        }
    }
}
