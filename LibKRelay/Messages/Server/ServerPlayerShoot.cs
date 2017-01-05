using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class ServerPlayerShoot : Message
    {
        public byte BulletId;
        public int OwnerId;
        public int ContainerType;
        public Location StartingLoc;
        public float Angle;
        public short Damage;

        public override void Read(MessageReader r)
        {
            BulletId = r.ReadByte();
            OwnerId = r.ReadInt32();
            ContainerType = r.ReadInt32();
            StartingLoc = new Location(r);
            Angle = r.ReadSingle();
            Damage = r.ReadInt16();
        }

        public override void Write(MessageWriter w)
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
