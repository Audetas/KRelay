using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class Status
    {
        public int ObjectId;
        public Location Position;
        public Dictionary<StatType, StatData> Stats;

        public Status()
        {

        }

        public Status(MessageReader r)
        {
            ObjectId = r.ReadInt32();
            Position = new Location(r);
            short amount = r.ReadInt16();
            Stats = new Dictionary<StatType, StatData>();
            for (int i = 0; i < amount; i++)
            {
                StatData data = new StatData(r);
                Stats.Add(data.Id, data);
            }
        }

        public void Write(MessageWriter w)
        {
            w.Write(ObjectId);
            Position.Write(w);
            w.Write((short)Stats.Count);

            foreach (StatData statdata in Stats.Values)
                statdata.Write(w);
        }

        public Status Clone()
        {
            return new Status
            {
                Stats = this.Stats,
                ObjectId = this.ObjectId,
                Position = (Location)this.Position.Clone()
            };
        }
    }
}
