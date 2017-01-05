using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class LocationRecord
    {
        public int Time;
        public Location Position;

        public LocationRecord(int time, float x, float y)
        {
            Time = time;
            Position = new Location(x, y);
        }

        public LocationRecord(int time, Location position)
        {
            Time = time;
            Position = position;
        }

        public LocationRecord(MessageReader r)
        {
            Time = r.ReadInt32();
            Position = new Location(r);
        }

        public void Write(MessageWriter w)
        {
            w.Write(Time);
            Position.Write(w);
        }

        public LocationRecord Clone()
        {
            return new LocationRecord(Time, Position.Clone());
        }

        public override string ToString()
        {
            return "{ Time=" + Time + ", X=" + Position.X + ", Y=" + Position.Y + " }";
        }
    }
}
