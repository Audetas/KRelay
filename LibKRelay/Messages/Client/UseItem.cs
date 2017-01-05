using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class UseItem : Message
    {
        public int Time;
        public SlotObject SlotObject;
        public Location ItemUsePos;
        public byte UseType;

        public override void Read(MessageReader r)
        {
            Time = r.ReadInt32();
            SlotObject = new SlotObject(r);
            ItemUsePos = new Location(r);
            UseType = r.ReadByte();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Time);
            SlotObject.Write(w);
            ItemUsePos.Write(w);
            w.Write(UseType);
        }
    }
}
