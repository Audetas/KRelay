using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
    public class PicPacket : Packet
    {
        public BitmapData BitmapData;

        public override PacketType Type
        { get { return PacketType.PIC; } }

        public override void Read(PacketReader r)
        {
            BitmapData = (BitmapData)BitmapData.Read(r);
        }

        public override void Write(PacketWriter w)
        {
            BitmapData.Write(w);
        }
    }
}
