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
            if (packet.Type == PacketType.RECONNECT)
            {
                ReconnectPacket recon = (ReconnectPacket)packet;

            }
            else if (packet.Type == PacketType.HELLO)
            {
                HelloPacket hello = (HelloPacket)packet;
                State = _proxy.GetState(this, hello.Key);
                if (State.ConRealKey.Length != 0)
                {
                    hello.Key = State.ConRealKey;
                    State.ConRealKey = new byte[0];
                }

                _serverConnection = new TcpClient();
                _serverConnection.NoDelay = true;
                _serverConnection.BeginConnect(State.ConTargetAddress, State.ConTargetPort, ServerConnected, packet);
                packet.Send = false;
            }
            else if (packet.Type == PacketType.CREATESUCCESS)
            {
                PlayerData = new PlayerData((packet as CreateSuccessPacket).ObjectId);
                PlayerData.MapName = _mapInfo.Name;
                PlayerData.TeleportAllowed = _mapInfo.AllowPlayerTeleport;
                PlayerData.MapWidth = _mapInfo.Width;
                PlayerData.MapHeight = _mapInfo.Height;
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
            else if (packet.Type == PacketType.PLAYERSHOOT)
            {
                PlayerData.Pos = (packet as PlayerShootPacket).Position;
            }
        }
    }
}
