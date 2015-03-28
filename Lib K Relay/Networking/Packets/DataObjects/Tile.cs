using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class Tile : IDataObject
    {
        public short X;
        public short Y;
        public int Type;

        public IDataObject Read(PacketReader r)
        {
            X = r.ReadInt16();
            Y = r.ReadInt16();
            Type = r.ReadInt32();

            return this;
        }

        public void Write(PacketWriter w)
        {
            w.Write(X);
            w.Write(Y);
            w.Write(Type);
        }

        public override string ToString()
        {
            return "{ X=" + X + ", Y=" + Y + ", Type=" + Type + " }";
        }
    }
}
