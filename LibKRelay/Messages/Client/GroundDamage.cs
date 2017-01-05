using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class GroundDamage : Message
    {
        public int Time;
        public Location Position;

        public override void Read(MessageReader r)
        {
            Time = r.ReadInt32();
            Position = new Location(r);
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Time);
            Position.Write(w);
        }
    }
}
