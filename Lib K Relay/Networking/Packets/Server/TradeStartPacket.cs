using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class TradeStartPacket : Packet
    {
        public Item[] MyItems;
        public string YourName;
        public Item[] YourItems;

        public override PacketType Type
        { get { return PacketType.TRADESTART; } }

        public override void Read(PacketReader r)
        {
            MyItems = new Item[r.ReadInt16()];
            for (int i = 0; i < MyItems.Length; i++)
                MyItems[i] = (Item) new Item().Read(r);

            YourName = r.ReadString();
            YourItems = new Item[r.ReadInt16()];
            for (int i = 0; i < YourItems.Length; i++)
                YourItems[i] = (Item)new Item().Read(r);
        }

        public override void Write(PacketWriter w)
        {
            w.Write((ushort)MyItems.Length);
            foreach (Item i in MyItems)
                i.Write(w);

            w.Write(YourName);
            w.Write((ushort)YourItems.Length);
            foreach (Item i in YourItems)
                i.Write(w);
        }
    }
}
