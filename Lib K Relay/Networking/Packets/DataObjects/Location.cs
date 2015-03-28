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
    }
}
