using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class HatchEggPacket : Packet
    {
        public string PetName;
        public int PetSkinId;
        public override PacketType Type
        { get { return PacketType.HATCHEGG; } }

        public override void Read(PacketReader r)
        {
            PetName = r.ReadString();
            PetSkinId = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(PetName);
            w.Write(PetSkinId);
        }
    }
}
