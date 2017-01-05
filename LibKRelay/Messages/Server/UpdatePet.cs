using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class UpdatePet : Message
    {
        public int PetId;

        public override void Read(MessageReader r)
        {
            PetId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(PetId);
        }
    }
}
