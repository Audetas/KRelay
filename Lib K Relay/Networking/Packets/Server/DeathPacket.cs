using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class DeathPacket : Packet
    {
        public string AccountId;
        public int CharId;
        public string KilledBy;
        public int ZombieType;
        public int ZombieId;

        public override PacketType Type
        { get { return PacketType.DEATH; } }

        public override void Read(PacketReader r)
        {
            AccountId = r.ReadString();
            CharId = r.ReadInt32();
            KilledBy = r.ReadString();
            ZombieType = r.ReadInt32();
            ZombieId = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(AccountId);
            w.Write(CharId);
            w.Write(KilledBy);
            w.Write(ZombieType);
            w.Write(ZombieId);
        }
    }
}
