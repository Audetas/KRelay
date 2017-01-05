using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class AllyShoot : Message
    {
        public byte BulletId;
        public int OwnerId;
        public short ContainerType;
        public float Angle;

        public override void Read(MessageReader r)
        {
            BulletId = r.ReadByte();
            OwnerId = r.ReadInt32();
            ContainerType = r.ReadInt16();
            Angle = r.ReadSingle();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(BulletId);
            w.Write(OwnerId);
            w.Write(ContainerType);
            w.Write(Angle);
        }
    }
}
