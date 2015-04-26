using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class Item : IDataObject
    {
        public int ItemItem;
        public int SlotType;
        public bool Tradable;
        public bool Included;

        public IDataObject Read(PacketReader r)
        {
            ItemItem = r.ReadInt32();
            SlotType = r.ReadInt32();
            Tradable = r.ReadBoolean();
            Included = r.ReadBoolean();

            return this;
        }

        public void Write(PacketWriter w)
        {
            w.Write(ItemItem);
            w.Write(SlotType);
            w.Write(Tradable);
            w.Write(Included);
        }

        public object Clone()
        {
            return new Item
            {
                ItemItem = this.ItemItem,
                SlotType = this.SlotType,
                Tradable = this.Tradable,
                Included = this.Included
            };
        }

        public override string ToString()
        {
            return "{ ItemItem=" + ItemItem + ", SlotType=" + SlotType + ", Tradable=" + Tradable + ", Included=" + Included + " }";
        }
    }
}
