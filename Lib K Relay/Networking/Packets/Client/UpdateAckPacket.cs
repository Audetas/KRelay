using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    class UpdateAckPacket : Packet
    {
        public override PacketType Type
        { get { return PacketType.UPDATEACK; } }

        public override void Read(PacketReader r) { }
        public override void Write(PacketWriter w) { }
    }
}
