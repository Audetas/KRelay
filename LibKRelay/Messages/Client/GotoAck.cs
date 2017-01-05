using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class GotoAck : Message
    {
        public int Time;

        public override void Read(MessageReader r)
        {
            Time = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Time);
        }
    }
}
