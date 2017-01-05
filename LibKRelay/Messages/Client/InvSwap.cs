using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class InvSwap : Message
    {
        public int Time;
        public Location Position;
        public SlotObject SlotObject1;
        public SlotObject SlotObject2;

        public override void Read(MessageReader r)
        {
            Time = r.ReadInt32();
            Position = new Location(r);
            SlotObject1 = new SlotObject(r);
            SlotObject2 = new SlotObject(r);
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Time);
            Position.Write(w);
            SlotObject1.Write(w);
            SlotObject2.Write(w);
        }
    }
}
