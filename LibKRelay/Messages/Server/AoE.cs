using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class AoE : Message
    {
        public Location Location;
        public float Radius;
        public ushort Damage;
        public ConditionEffectIndex Effects;
        public float EffectDuration;
        public short OriginType;

        public override void Read(MessageReader r)
        {
            Location = new Location(r);
            Radius = r.ReadSingle();
            Damage = r.ReadUInt16();
            Effects = (ConditionEffectIndex)r.ReadByte();
            EffectDuration = r.ReadSingle();
            OriginType = r.ReadInt16();
        }

        public override void Write(MessageWriter w)
        {
            Location.Write(w);
            w.Write(Radius);
            w.Write(Damage);
            w.Write((byte)Effects);
            w.Write(EffectDuration);
            w.Write(OriginType);
        }
    }
}
