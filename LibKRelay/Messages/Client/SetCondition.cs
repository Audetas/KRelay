using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class SetCondition : Message
    {
        public byte ConditionEffect;
        public float ConditionDuration;

        public override void Read(MessageReader r)
        {
            ConditionEffect = r.ReadByte();
            ConditionDuration = r.ReadSingle();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(ConditionEffect);
            w.Write(ConditionDuration);
        }
    }
}
