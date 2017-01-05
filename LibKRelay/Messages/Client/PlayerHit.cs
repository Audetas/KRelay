using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class PlayerHit : Message
    {
        public byte BulletId;
        public int ObjectId;

        public override void Read(MessageReader r)
        {
            BulletId = r.ReadByte();
            ObjectId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(BulletId);
            w.Write(ObjectId);
        }
    }
}
