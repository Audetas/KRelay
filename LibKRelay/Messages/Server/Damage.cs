using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Damage : Message
    {
        public int TargetId;
        public ConditionEffects Effects;
        public ushort DamageAmount;
        public bool Killed;
        public byte BulletId;
        public int ObjectId;

        public override void Read(MessageReader r)
        {
            TargetId = r.ReadInt32();
            byte c = r.ReadByte();
            Effects = 0;
            for (int i = 0; i < c; i++)
                Effects |= (ConditionEffects)(1 << r.ReadByte());
            DamageAmount = r.ReadUInt16();
            Killed = r.ReadBoolean();
            BulletId = r.ReadByte();
            ObjectId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(TargetId);
            List<byte> eff = new List<byte>();
            for (byte i = 1; i < 255; i++)
                if ((Effects & (ConditionEffects)(1 << i)) != 0)
                    eff.Add(i);
            w.Write((byte)eff.Count);
            foreach (byte i in eff) w.Write(i);
            w.Write(DamageAmount);
            w.Write(Killed);
            w.Write(BulletId);
            w.Write(ObjectId);
        }
    }
}
