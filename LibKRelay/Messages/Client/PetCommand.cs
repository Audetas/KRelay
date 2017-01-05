using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class PetCommand : Message
    {
        public const int FOLLOW_PET = 1;
        public const int UNFOLLOW_PET = 2;
        public const int RELEASE_PET = 3;

        public int CommandId;
        public uint PetId;

        public override void Read(MessageReader r)
        {
            CommandId = (int)r.ReadByte();
            PetId = (uint)r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write((byte)CommandId);
            w.Write((int)PetId);
        }
    }
}
