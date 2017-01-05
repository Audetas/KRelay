using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class PetEvolveResult : Message
    {
        public int PetId;
        public int SkinId1;
        public int SkinId2;

        public override void Read(MessageReader r)
        {
            PetId = r.ReadInt32();
            SkinId1 = r.ReadInt32();
            SkinId2 = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(PetId);
            w.Write(SkinId1);
            w.Write(SkinId2);
        }
    }
}