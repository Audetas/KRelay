using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class UsePortal : Message
    {
        public int ObjectId;

        public override void Read(MessageReader r)
        {
            this.ObjectId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(ObjectId);
        }
    }
}
