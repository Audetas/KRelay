using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class EnemyHit : Message
    {
        public int Time;
        public byte BulletId;
        public int TargetId;
        public bool Killed;

        public override void Read(MessageReader r)
        {
            Time = r.ReadInt32();
            BulletId = r.ReadByte();
            TargetId = r.ReadInt32();
            Killed = r.ReadBoolean();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Time);
            w.Write(BulletId);
            w.Write(TargetId);
            w.Write(Killed);
        }
    }
}
