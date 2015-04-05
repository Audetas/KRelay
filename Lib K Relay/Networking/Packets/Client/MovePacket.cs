using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Client
{
    public class MovePacket : Packet
    {
        public int TickId;
        public int Time;
        public Location NewPosition;
        public LocationRecord[] Records;

        public override PacketType Type
        { get { return PacketType.MOVE; } }

        public override void Read(PacketReader r)
        {
            TickId = r.ReadInt32();
            Time = r.ReadInt32();
            NewPosition = (Location)new Location().Read(r);
            Records = new LocationRecord[r.ReadInt16()];
            for (int i = 0; i < Records.Length; i++)
                Records[i] = (LocationRecord)new LocationRecord().Read(r);
        }

        public override void Write(PacketWriter w)
        {
            w.Write(TickId);
            w.Write(Time);
            NewPosition.Write(w);
            w.Write((short)Records.Length);
            foreach (LocationRecord l in Records)
                l.Write(w);
        }
    }
}
