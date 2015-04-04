using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class CreatePacket : Packet
    {
        public byte ClassType;
        public byte SkinType;

        public override PacketType Type
        { get { return PacketType.CREATE; } }

        public override void Read(PacketReader r)
        {
            ClassType = r.ReadByte();
            SkinType = r.ReadByte();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(ClassType);
            w.Write(SkinType);
        }
    }
}
