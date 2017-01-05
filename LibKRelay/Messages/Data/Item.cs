using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class Item
    {
        public int ItemItem;
        public int SlotType;
        public bool Tradable;
        public bool Included;

        public static Item Parse(string input)
        {
            string[] splits = input.Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
            return new Item(
                int.Parse(splits[0]),
                int.Parse(splits[1]),
                bool.Parse(splits[2]),
                bool.Parse(splits[3])
            );
        }

        public Item(int itemItem, int slotType, bool tradable, bool included)
        {
            ItemItem = itemItem;
            SlotType = slotType;
            Tradable = tradable;
            Included = included;
        }

        public Item(MessageReader r)
        {
            ItemItem = r.ReadInt32();
            SlotType = r.ReadInt32();
            Tradable = r.ReadBoolean();
            Included = r.ReadBoolean();
        }

        public void Write(MessageWriter w)
        {
            w.Write(ItemItem);
            w.Write(SlotType);
            w.Write(Tradable);
            w.Write(Included);
        }

        public Item Clone()
        {
            return new Item(ItemItem, SlotType, Tradable, Included);
        }

        public override string ToString()
        {
            return "{ ItemItem=" + ItemItem + ", SlotType=" + SlotType + ", Tradable=" + Tradable + ", Included=" + Included + " }";
        }
    }
}
