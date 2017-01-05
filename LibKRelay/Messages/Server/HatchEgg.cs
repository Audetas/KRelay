using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class HatchEgg : Message
    {
        public string PetName;
        public int PetSkinId;

        public override void Read(MessageReader r)
        {
            PetName = r.ReadString();
            PetSkinId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(PetName);
            w.Write(PetSkinId);
        }
    }
}
