using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class Entity : IDataObject
    {
        public short ObjectType;
        public Status Status = new Status();

        public IDataObject Read(PacketReader r)
        {
            ObjectType = r.ReadInt16();
            Status.Read(r);

            return this;
        }

        public void Write(PacketWriter w)
        {
            w.Write(ObjectType);
            Status.Write(w);
        }

        public object Clone()
        {
            return new Entity
            {
                ObjectType = this.ObjectType,
                Status = (Status)this.Status.Clone()
            };
        }
    }
}
