using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class PetYardCommand : Message
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

        public override void Read(MessageReader r)
        {
            CommandId = r.ReadByte();
            PetId1 = r.ReadInt32();
            PetId2 = r.ReadInt32();
            ObjectId = r.ReadInt32();
            ObjectSlot = new SlotObject(r);
            Currency = r.ReadByte();
        }

        public override void Write(MessageWriter w)
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
