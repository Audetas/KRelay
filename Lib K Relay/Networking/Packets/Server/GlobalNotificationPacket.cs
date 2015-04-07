using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class GlobalNotificationPacket : Packet
    {
        public int TypeId ;
        public string Text ;

        public override PacketType Type
        { get { return PacketType.GLOBALNOTIFICATION; } }

        public override void Read(PacketReader r)
        {
            TypeId = r.ReadInt32();
            Text = r.ReadString();
        }

        public override void Write(PacketWriter w)
        {
            w.Write(TypeId);
            w.Write(Text);
        }
    }
}
