using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class NewAbilityUnlocked : Message
    {
		public Ability AbilityType;

        public override void Read(MessageReader r)
        {
            AbilityType = (Ability)r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write((int)AbilityType);
        }
    }
}
