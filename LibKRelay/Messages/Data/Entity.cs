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

        public int ObjectId
        {
            get { return Status.ObjectId; }
        }

        public Location Position
        {
            get { return Status.Position; }
            set { Status.Position = value; }
        }

        public StatData this[StatType key]
        {
            get { return Status.Stats[key]; }
            set { Status.Stats[key] = value; }
        }

        public Classes Class
        {
            get { return (Classes)ObjectType; }
        }

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

        public void Parse(Status status)
        {
            foreach (var pair in status.Stats)
                Status.Stats.AddOrUpdate(pair.Key, pair.Value);
        }

        public bool HasEffect(ConditionEffects effect)
        {
            return (Status.Stats[StatType.Effects].IntValue & (int)effect) != 0;
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
