using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class InvitedToGuildPacket : Packet
    {
        public string Name ;
        public string GuildName ;
        public override PacketType Type
        { get { return PacketType.INVITEDTOGUILD; } }

        public override void Read(PacketReader r)
        {
            Name = r.ReadString();
            GuildName = r.ReadString();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Name);
            w.Write(GuildName);
        }
    }
}
