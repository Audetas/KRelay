using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class PlayerShootPacket : Packet
    {
        public int Time;
        public byte BulletId;
        public short ContainerType;
        public Location Position;
        public float Angle;

        public override PacketType Type
        { get { return PacketType.PLAYERSHOOT; } }

        public override void Read(PacketReader r)
        {
            Time = r.ReadInt32();
            BulletId = r.ReadByte();
            ContainerType = r.ReadInt16();
            Position = (Location)new Location().Read(r);
            Angle = r.ReadSingle();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Time);
            w.Write(BulletId);
            w.Write(ContainerType);
            Position.Write(w);
            w.Write(Angle);
        }
    }
}
