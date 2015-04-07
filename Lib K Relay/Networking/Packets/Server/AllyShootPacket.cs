using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class AllyShootPacket : Packet
    {
        public byte BulletId;
        public int OwnerId;
        public short ContainerType;
        public float Angle;

        public override PacketType Type
        { get { return PacketType.ALLYSHOOT; } }

        public override void Read(PacketReader r)
        {
            BulletId = r.ReadByte();
            OwnerId = r.ReadInt32();
            ContainerType = r.ReadInt16();
            Angle = r.ReadSingle();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(BulletId);
            w.Write(OwnerId);
            w.Write(ContainerType);
            w.Write(Angle);
        }
    }
}
