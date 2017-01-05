using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class NewTick : Message
    {
        public int TickId;
        public int TickTime;
        public Dictionary<int, Status> Statuses;

        public override void Read(MessageReader r)
        {
            TickId = r.ReadInt32();
            TickTime = r.ReadInt32();

            int statusCount = r.ReadInt16();
            Statuses = new Dictionary<int, Status>(statusCount);
            for (int i = 0; i < statusCount; i++)
            {
                Status s = new Status(r);
                Statuses.Add(s.ObjectId, s);
            }
        }

        public override void Write(MessageWriter w)
        {
            w.Write(TickId);
            w.Write(TickTime);

            w.Write((short)Statuses.Count);
            foreach (Status s in Statuses.Values) s.Write(w);
        }
    }
}
