using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class ArenaNextWavePacket : Packet
    {
        public int TypeId;

        public override PacketType Type
        { get { return PacketType.ARENANEXTWAVE; } }

        public override void Read(PacketReader r)
        {
            TypeId = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(TypeId);
        }
    }
}
