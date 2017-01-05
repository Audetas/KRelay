using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class EnemyShoot : Message
    {
        public byte BulletId;
        public int OwnerId;
        public byte BulletType;
        public Location Location;
        public float Angle;
        public short Damage;
        public byte NumShots;
        public float AngleInc;

        public override void Read(MessageReader r)
        {
            BulletId = r.ReadByte();
            OwnerId = r.ReadInt32();
            BulletType = r.ReadByte();
            Location = new Location(r);
            Angle = r.ReadSingle();
            Damage = r.ReadInt16();

            if (r.BaseStream.Position < r.BaseStream.Length)
            {
                NumShots = r.ReadByte();
                AngleInc = r.ReadSingle();
            }
            else
            {
                NumShots = 1;
                AngleInc = 0.0F;
            }
        }

        public override void Write(MessageWriter w)
        {
            w.Write(BulletId);
            w.Write(OwnerId);
            w.Write(BulletType);
            Location.Write(w);
            w.Write(Angle);
            w.Write(Damage);

            if (NumShots != 1)
            {
                w.Write((byte)NumShots);
                w.Write(AngleInc);
            }
        }
    }
}
