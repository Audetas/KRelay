using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class ShowEffect : Message
    {
        public EffectType EffectType;
        public int TargetId;
        public Location PosA;
        public Location PosB;
        public ARGB Color;

        public override void Read(MessageReader r)
        {
            EffectType = (EffectType)r.ReadByte();
            TargetId = r.ReadInt32();
            PosA = new Location(r);
            PosB = new Location(r);
            Color = ARGB.Read(r);
        }

        public override void Write(MessageWriter w)
        {
            w.Write((byte)EffectType);
            w.Write(TargetId);
            PosA.Write(w);
            PosB.Write(w);
            Color.Write(w);
        }
    }
}
