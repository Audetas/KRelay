using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class CheckCreditsPacket : Packet
    {
        public override PacketType Type
        { get { return PacketType.CHECKCREDITS; } }

        public override void Read(PacketReader r)
        {

        }

        public override void Write(PacketWriter w)
        {

        }
    }
}
