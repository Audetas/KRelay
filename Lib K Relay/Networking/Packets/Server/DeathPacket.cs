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
        public string Killer;
        public int obf0;
        public int obf1;

        public override PacketType Type
        { get { return PacketType.DEATH; } }

        public override void Read(PacketReader r)
        {
            AccountId = r.ReadString();
            CharId = r.ReadInt32();
            Killer = r.ReadString();
            obf0 = r.ReadInt32();
            obf1 = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(AccountId);
            w.Write(CharId);
            w.Write(Killer);
            w.Write(obf0);
            w.Write(obf1);
        }
    }
}
