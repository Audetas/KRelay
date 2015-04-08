using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class PetEvolveResultPacket : Packet
    {
        public int PetId1;
        public int SkinId1;
        public int SkinId2;
        public override PacketType Type
        { get { return PacketType.EVOLVEPET; } }

        public override void Read(PacketReader r)
        {
            PetId1 = r.ReadInt32();
            SkinId1 = r.ReadInt32();
            SkinId2 = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(PetId1);
            w.Write(SkinId1);
            w.Write(SkinId2);
        }
    }
}
