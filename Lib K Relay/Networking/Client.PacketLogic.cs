using Lib_K_Relay.Networking.Packets;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Networking.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking
{
    public partial class Client
    {
        private MapInfoPacket _mapInfo;
        private void HandlePacketInternal(Packet packet)
        {
            if (packet.Type == PacketType.CREATESUCCESS)
            {
                PlayerData = new PlayerData((packet as CreateSuccessPacket).ObjectId, _mapInfo);
            }
            else if (packet.Type == PacketType.MAPINFO)
            {
                _mapInfo = (MapInfoPacket)packet;
            }
            else if (packet.Type == PacketType.UPDATE)
            {
                PlayerData.Parse(packet as UpdatePacket);
            }
            else if (packet.Type == PacketType.NEWTICK)
            {
                PlayerData.Parse(packet as NewTickPacket);
            }
            else if (packet.Type == PacketType.PLAYERSHOOT)
            {
                PlayerData.Pos = (packet as PlayerShootPacket).Position;
            }
            else if (packet.Type == PacketType.PONG)
            {
                _lastTime = (packet as PongPacket).Time;
                _lastTimeTime = Environment.TickCount;
            }
            else if (packet.Type == PacketType.MOVE)
            {
                _lastTime = (packet as MovePacket).Time;
                _lastTimeTime = Environment.TickCount;
                PlayerData.Pos = (packet as MovePacket).NewPosition;
            }
        }
    }
}
