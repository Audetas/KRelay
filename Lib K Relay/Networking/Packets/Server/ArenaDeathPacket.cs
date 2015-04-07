using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class ArenaDeathPacket : Packet
    {
        public int RestartPrice;

        public override PacketType Type
        { get { return PacketType.ARENADEATH; } }

        public override void Read(PacketReader r)
        {
            RestartPrice = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(RestartPrice);
        }
    }
}
