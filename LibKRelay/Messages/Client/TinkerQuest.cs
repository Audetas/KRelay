using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class TinkerQuest : Message
    {
        public SlotObject Slot;

        public override void Read(MessageReader r)
        {
            Slot = new SlotObject(r);
        }

        public override void Write(MessageWriter w)
        {
            Slot.Write(w);
        }
    }
}
