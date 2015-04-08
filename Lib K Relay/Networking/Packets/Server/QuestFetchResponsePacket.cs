using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class QuestFetchResponsePacket : Packet
    {
        public int Tier;
        public string Goal;
        public string Description;
        public string Image;
        public override PacketType Type
        { get { return PacketType.QUESTFETCHRESPONSE; } }

        public override void Read(PacketReader r)
        {
            Tier = r.ReadInt32();
            Goal = r.ReadString();
            Description = r.ReadString();
            Image = r.ReadString();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Tier);
            w.Write(Goal);
            w.Write(Description);
            w.Write(Image);
        }
    }
}
