using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class PongPacket : Packet
    {
        public int Serial;
        public int Time;

        public override PacketType Type
        { get { return PacketType.PONG; } }

        public override void Read(PacketReader r)
        {
            Serial = r.ReadInt32();
            Time = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Serial);
            w.Write(Time);
        }
    }
}
