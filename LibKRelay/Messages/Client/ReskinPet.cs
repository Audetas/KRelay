using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class ReskinPet : Message
    {
        public int PetId;
		public int NewPetType;
		public SlotObject Item;

        public override void Read(MessageReader r)
        {
            PetId = r.ReadInt32();
			NewPetType = r.ReadInt32();
            Item = new SlotObject(r);
		}

        public override void Write(MessageWriter w)
        {
            w.Write(PetId);
			w.Write(NewPetType);
			Item.Write(w);
        }
    }
}
