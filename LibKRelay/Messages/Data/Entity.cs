using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class Entity
    {
        public short ObjectType;
        public Status Status;

        public Entity(short objectType, Status status)
        {
            ObjectType = objectType;
            Status = status;
        }

        public Entity(MessageReader r)
        {
            ObjectType = r.ReadInt16();
            Status = new Status(r);
        }

        public void Write(MessageWriter w)
        {
            w.Write(ObjectType);
            Status.Write(w);
        }

        public object Clone()
        {
            return new Entity(ObjectType, Status.Clone());
        }

        public override string ToString()
        {
            return "{ Object Type: " + ObjectType + " }";
        }
    }
}
