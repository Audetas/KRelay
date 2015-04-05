using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class InvSwapPacket : Packet
    {
        public int Time;
        public Location Position;
        public SlotObject SlotObject1;
        public SlotObject SlotObject2;

        public override PacketType Type
        { get { return PacketType.INVSWAP; } }

        public override void Read(PacketReader r)
        {
            Time = r.ReadInt32();
            Position = (Location)new Location().Read(r);
            SlotObject1 = (SlotObject)new SlotObject().Read(r);
            SlotObject2 = (SlotObject)new SlotObject().Read(r);
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Time);
            Position.Write(w);
            SlotObject1.Write(w);
            SlotObject2.Write(w);
        }
    }
}
