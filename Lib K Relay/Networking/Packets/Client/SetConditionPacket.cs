using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class SetConditionPacket : Packet
    {
        public int ConditionEffect;
        public float ConditionDuration;

        public override PacketType Type
        { get { return PacketType.SETCONDITION; } }

        public override void Read(PacketReader r)
        {
            ConditionEffect = r.ReadInt32();
            ConditionDuration = r.ReadSingle();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(ConditionEffect);
            w.Write(ConditionDuration);
        }
    }
}
