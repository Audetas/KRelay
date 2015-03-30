using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class NotificationPacket : Packet
    {
        public int ObjectId;
        public string Message;
        public int Color;

        public override PacketType Type
        { get { return PacketType.NOTIFICATION; } }

        public override void Read(PacketReader r)
        {
            ObjectId = r.ReadInt32();
            Message = r.ReadString();
            Color = r.ReadInt32();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(ObjectId);
            w.Write(Message);
            w.Write(Color);
        }
    }
}
