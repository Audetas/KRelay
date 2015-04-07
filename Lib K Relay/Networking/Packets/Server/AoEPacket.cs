using Lib_K_Relay.Networking.Packets.DataObjects;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class AoEPacket : Packet
    {
        public Location Location;
        public float Radius;
        public ushort Damage;
        public ConditionEffectIndex Effects;
        public float EffectDuration;
        public short OriginType;
        public override PacketType Type
        { get { return PacketType.AOE; } }

        public override void Read(PacketReader r)
        {
            Location = (Location) new Location().Read(r);
            Radius = r.ReadSingle();
            Damage = r.ReadUInt16();
            Effects = (ConditionEffectIndex)r.ReadByte();
            EffectDuration = r.ReadSingle();
            OriginType = r.ReadInt16();
        }

        public override void Write(PacketWriter w)
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
