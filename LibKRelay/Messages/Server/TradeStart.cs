using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class TradeStart : Message
    {
        public List<Item> MyItems;
        public string YourName;
        public List<Item> YourItems;

        public override void Read(MessageReader r)
        {
            int myCount = r.ReadInt16();
            MyItems = new List<Item>(myCount);
            for (int i = 0; i < myCount; i++)
                MyItems.Add(new Item(r));

            YourName = r.ReadString();
            int yourCount = r.ReadInt16();
            YourItems = new List<Item>(yourCount);
            for (int i = 0; i < yourCount; i++)
                YourItems.Add(new Item(r));
        }

        public override void Write(MessageWriter w)
        {
            w.Write((ushort)MyItems.Count);
            foreach (Item i in MyItems)
                i.Write(w);

            w.Write(YourName);
            w.Write((ushort)YourItems.Count);
            foreach (Item i in YourItems)
                i.Write(w);
        }
    }
}
