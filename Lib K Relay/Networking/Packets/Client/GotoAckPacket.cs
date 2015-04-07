using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class GotoAckPacket : Packet
    {
        public int Time;

        public override PacketType Type
        { get { return PacketType.GOTOACK; } }

        public override void Read(PacketReader r)
        {
            Time = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Time);
        }
    }
}
