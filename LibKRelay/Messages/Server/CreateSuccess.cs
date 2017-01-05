using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class CreateSuccess : Message
    {
        public int ObjectId;
        public int CharId;

        public override void Read(MessageReader r)
        {
            ObjectId = r.ReadInt32();
            CharId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(ObjectId);
            w.Write(CharId);
        }
    }
}
