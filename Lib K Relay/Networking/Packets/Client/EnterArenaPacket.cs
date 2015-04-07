using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class EnterArenaPacket : Packet
    {
        public int Currency;

        public override PacketType Type
        { get { return PacketType.ENTERARENA; } }

        public override void Read(PacketReader r)
        {
            Currency = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Currency);
        }
    }
}
