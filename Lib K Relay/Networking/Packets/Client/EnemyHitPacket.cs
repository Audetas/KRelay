using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class EnemyHitPacket : Packet
    {
        public int Time;
        public byte BulletId;
        public int TargetId;
        public bool Killed;

        public override PacketType Type
        { get { return PacketType.ENEMYHIT; } }

        public override void Read(PacketReader r)
        {
            Time = r.ReadInt32();
            BulletId = r.ReadByte();
            TargetId = r.ReadInt32();
            Killed = r.ReadBoolean();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Time);
            w.Write(BulletId);
            w.Write(TargetId);
            w.Write(Killed);
        }
    }
}
