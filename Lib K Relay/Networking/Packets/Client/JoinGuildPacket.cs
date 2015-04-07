using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class JoinGuildPacket : Packet
    {
        public string GuildName;

        public override PacketType Type
        { get { return PacketType.JOINGUILD; } }

        public override void Read(PacketReader r)
        {
            GuildName = r.ReadString();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(GuildName);
        }
    }
}
