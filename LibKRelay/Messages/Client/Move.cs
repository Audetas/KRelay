using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class Move : Message
    {
        public int TickId;
        public int Time;
        public Location NewPosition;
        public List<LocationRecord> Records;

        public override void Read(MessageReader r)
        {
            TickId = r.ReadInt32();
            Time = r.ReadInt32();
            NewPosition = new Location(r);
            int recordCount = r.ReadInt16();
            Records = new List<LocationRecord>(recordCount);
            for (int i = 0; i < recordCount; i++)
                Records.Add(new LocationRecord(r));
        }

        public override void Write(MessageWriter w)
        {
            w.Write(TickId);
            w.Write(Time);
            NewPosition.Write(w);
            w.Write((short)Records.Count);
            foreach (LocationRecord l in Records)
                l.Write(w);
        }
    }
}
