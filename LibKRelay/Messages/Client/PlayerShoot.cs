using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class PlayerShoot : Message
    {
        public int Time;
        public byte BulletId;
        public short ContainerType;
        public Location Position;
        public float Angle;

        public override void Read(MessageReader r)
        {
            Time = r.ReadInt32();
            BulletId = r.ReadByte();
            ContainerType = r.ReadInt16();
            Position = new Location(r);
            Angle = r.ReadSingle();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Time);
            w.Write(BulletId);
            w.Write(ContainerType);
            Position.Write(w);
            w.Write(Angle);
        }
    }
}
