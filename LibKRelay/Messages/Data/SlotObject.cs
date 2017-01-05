using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class SlotObject
    {
        public int ObjectId;
        public byte SlotId;
        public int ObjectType;

        public static SlotObject Parse(string input)
        {
            string[] splits = input.Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
            return new SlotObject(
                int.Parse(splits[0]),
                byte.Parse(splits[1]),
                int.Parse(splits[2])
            );
        }

        public SlotObject(int objectId, byte slotId, int objectType)
        {
            ObjectId = objectId;
            SlotId = slotId;
            ObjectType = objectType;
        }

        public SlotObject(MessageReader r)
        {
            ObjectId = r.ReadInt32();
            SlotId = r.ReadByte();
            ObjectType = r.ReadInt32();
        }

        public void Write(MessageWriter w)
        {
            w.Write(ObjectId);
            w.Write(SlotId);
            w.Write(ObjectType);
        }

        public SlotObject Clone()
        {
            return new SlotObject(ObjectId, SlotId, ObjectType);
        }

        public override string ToString()
        {
            return "{ ObjectId=" + ObjectId + ", SlotId=" + SlotId + ", ObjectType=" + ObjectType + " }";
        }
    }
}
