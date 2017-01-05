using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Ping : Message
    {
        public int Serial;

        public override void Read(MessageReader r)
        {
            Serial = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Serial);
        }
    }
}
