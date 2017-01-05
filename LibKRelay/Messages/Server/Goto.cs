using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Goto : Message
    {
        public int ObjectId;
        public Location Location;

        public override void Read(MessageReader r)
        {
            ObjectId = r.ReadInt32();
            Location = new Location(r);
        }

        public override void Write(MessageWriter w)
        {
            w.Write(ObjectId);
            Location.Write(w);
        }
    }
}
