using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class Location : IDataObject
    {
        public float X;
        public float Y;

        public static Location Empty
        {
            get
            {
                return new Location()
                {
                    X = 0,
                    Y = 0
                };
            }
        }

        public virtual IDataObject Read(PacketReader r)
        {
            X = r.ReadSingle();
            Y = r.ReadSingle();

            return this;
        }

        public virtual void Write(PacketWriter w)
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

        public virtual object Clone()
        {
            return new Location
            {
                X = this.X,
                Y = this.Y
            };
        }

        public override string ToString()
        {
            return "{ X=" + X + ", Y=" + Y + " }";
        }
    }
}
