using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    [Serializable]
    public class Location
    {
        public double X;
        public double Y;

        public float SingleX { get { return (float)X; } }
        public float SingleY { get { return (float)Y; } }

        public static Location Empty { get { return new Location(0d, 0d); } }

        public static Location Parse(string input)
        {
            string[] splits = input.Split(new[] { ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
            return new Location(
                double.Parse(splits[0]),
                double.Parse(splits[1])
            );
        }

        public Location(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Location(MessageReader r)
        {
            X = r.ReadSingle();
            Y = r.ReadSingle();
        }

        public void Write(MessageWriter w)
        {
            w.Write(SingleX);
            w.Write(SingleY);
        }

        public double DistanceSquaredTo(Location location)
        {
            double dx = location.X - X;
            double dy = location.Y - Y;
            return dx * dx + dy * dy;
        }

        public double DistanceTo(Location location)
        {
            return Math.Sqrt(DistanceSquaredTo(location));
        }

        private double GetAngle(Location l1, Location l2)
        {
            double dX = l2.X - l1.X;
            double dY = l2.Y - l1.Y;
            return Math.Atan2(dY, dX);
        }

        private double GetAngle(double x1, double y1, double x2, double y2)
        {
            double dX = x2 - x1;
            double dY = y2 - y1;
            return Math.Atan2(dY, dX);
        }

        public Location Clone()
        {
            return new Location(X, Y);
        }

        public static Location operator +(Location vec1, Location vec2)
        {
            return Add(vec1, vec2);
        }

        public static Location operator -(Location vec1, Location vec2)
        {
            return Subtract(vec1, vec2);
        }

        public static Location Add(Location vec1, Location vec2)
        {
            Location result = new Location(vec1.X, vec1.Y);
            result.X += vec2.X;
            result.Y += vec2.Y;
            return result;
        }

        public static Location Subtract(Location loc1, Location loc2)
        {
            Location result = new Location(loc1.X, loc1.Y);
            result.X -= loc2.X;
            result.Y -= loc2.Y;
            return result;
        }

        public static double DotProduct(Location loc1, Location loc2)
        {
            return (loc1.X * loc2.X) + (loc1.Y * loc2.Y);
        }

        public void Scale(double num)
        {
            X *= num;
            Y *= num;
        }

        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public void Normalize()
        {
            double m = Magnitude();
            if (m > 0)
            {
                X = X / m;
                Y = Y / m;
            }
            else
            {
                X = 0;
                Y = 0;
            }
        }

        public override string ToString()
        {
            return "{ X=" + X + ", Y=" + Y + " }";
        }
    }
}
