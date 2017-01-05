using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class Pong : Message
    {
        public int Serial;
        public int Time;

        public override void Read(MessageReader r)
        {
            Serial = r.ReadInt32();
            Time = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Serial);
            w.Write(Time);
        }
    }
}
