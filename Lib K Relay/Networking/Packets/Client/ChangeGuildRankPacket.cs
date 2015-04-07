using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class ChangeGuildRankPacket : Packet
    {
        public string Name;
        public int GuildRank;

        public override PacketType Type
        { get { return PacketType.CHANGEGUILDRANK; } }

        public override void Read(PacketReader r)
        {
            Name = r.ReadString();
            GuildRank = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Name);
            w.Write(GuildRank);
        }
    }
}
