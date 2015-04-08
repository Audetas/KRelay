using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class Shoot2Packet : Packet
    {
        public byte BulletId;
        public int OwnerId;
        public int ContainerType;
        public Location StartingLoc;
        public float Angle;
        public short Damage;
        public override PacketType Type
        { get { return PacketType.SHOOT2; } }

        public override void Read(PacketReader r)
        {
            BulletId = r.ReadByte();
            OwnerId = r.ReadInt32();
            ContainerType = r.ReadInt32();
            StartingLoc = (Location) new Location().Read(r);
            Angle = r.ReadSingle();
            Damage = r.ReadInt16();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(BulletId);
            w.Write(OwnerId);
            w.Write(ContainerType);
            StartingLoc.Write(w);
            w.Write(Angle);
            w.Write(Damage);
        }
    }
}
