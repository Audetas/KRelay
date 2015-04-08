using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class RemovePetFromListPacket : Packet
    {
        public int PetId;

        public override PacketType Type
        { get { return PacketType.REMOVEPET; } }

        public override void Read(PacketReader r)
        {
            PetId = r.ReadInt32();

        }

        public override void Write(PacketWriter w)
        {
            w.Write(PetId);

        }
    }
}
