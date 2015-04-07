using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class InvResultPacket : Packet
    {
        public int Result ;

        public override PacketType Type
        { get { return PacketType.INVRESULT; } }

        public override void Read(PacketReader r)
        {
            Result = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Result);
        }
    }
}
