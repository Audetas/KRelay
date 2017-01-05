using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Notification : Message
    {
        public int ObjectId;
        public string Message;
        public int Color;

        public override void Read(MessageReader r)
        {
            ObjectId = r.ReadInt32();
            Message = r.ReadString();
            Color = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(ObjectId);
            w.Write(Message);
            w.Write(Color);
        }
    }
}
