using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class PetYardCommandPacket : Packet
    {
        public const int UPGRADE_PET_YARD = 1;
        public const int FEED_PET = 2;
        public const int FUSE_PET = 3;

        public byte CommandId;
        public int PetId1;
        public int PetId2;
        public int ObjectId;
        public SlotObject ObjectSlot;
        public byte Currency;

        public override PacketType Type
        { get { return PacketType.PETYARDCOMMAND; } }

        public override void Read(PacketReader r)
        {
            CommandId = r.ReadByte();
            PetId1 = r.ReadInt32();
            PetId2 = r.ReadInt32();
            ObjectId = r.ReadInt32();
            ObjectSlot = (SlotObject)new SlotObject().Read(r);
            Currency = r.ReadByte();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(CommandId);
            w.Write(PetId1);
            w.Write(PetId2);
            w.Write(ObjectId);
            ObjectSlot.Write(w);
            w.Write((byte)Currency);
        }
    }
}
