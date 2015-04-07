using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class AoEAckPacket : Packet
    {
        public int Time;
        public Location Position;

        public override PacketType Type
        { get { return PacketType.AOEACK; } }

        public override void Read(PacketReader r)
        {
            Time = r.ReadInt32();
            Position = (Location)new Location().Read(r);
        }

        public override void Write(PacketWriter w)
        {
            w.Write(Time);
            Position.Write(w);
        }
    }
}
