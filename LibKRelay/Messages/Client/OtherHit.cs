using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class OtherHit : Message
    {
        public int Time;
        public byte BulletId;
        public int ObjectId;
        public int TargetId;

        public override void Read(MessageReader r)
        {
            Time = r.ReadInt32();
            BulletId = r.ReadByte();
            ObjectId = r.ReadInt32();
            TargetId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Time);
            w.Write(BulletId);
            w.Write(ObjectId);
            w.Write(TargetId);
        }
    }
}
