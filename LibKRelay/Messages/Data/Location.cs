using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class Location
    {
        public float X;
        public float Y;

        public static Location Parse(string input)
        {
            string[] splits = input.Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
            return new Location(
                float.Parse(splits[0]),
                float.Parse(splits[1])
            );
        }

        public Location(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Location Empty
        {
            get
            {
                return new Location(0, 0);
            }
        }

        public Location(MessageReader r)
        {
            X = r.ReadSingle();
            Y = r.ReadSingle();
        }

        public void Write(MessageWriter w)
        {
            w.Write(X);
            w.Write(Y);
        }

        public float DistanceSquaredTo(Location location)
        {
            float dx = location.X - X;
            float dy = location.Y - Y;
            return dx * dx + dy * dy;
        }

        public float DistanceTo(Location location)
        {
            return (float)Math.Sqrt(DistanceSquaredTo(location));
        }

        private float GetAngle(Location l1, Location l2)
        {
            float dX = l2.X - l1.X;
            float dY = l2.Y - l1.Y;
            return (float)Math.Atan2(dY, dX);
        }

        private float GetAngle(float x1, float y1, float x2, float y2)
        {
            float dX = x2 - x1;
            float dY = y2 - y1;
            return (float)Math.Atan2(dY, dX);
        }

        public Location Clone()
        {
            return new Location(X, Y);
        }

        public override string ToString()
        {
            return "{ X=" + X + ", Y=" + Y + " }";
        }
    }
}
