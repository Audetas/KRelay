using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages
{
    public class StatData
    {
        public StatType Id;
        public int IntValue;
        public string StringValue;

        public StatData(StatType type, string value)
        {
            Id = type;
            StringValue = value;
        }

        public StatData(StatType type, int value)
        {
            Id = type;
            IntValue = value;
        }

        public StatData(MessageReader r)
        {
            Id = (StatType)r.ReadByte();
            if (IsUTF())
                StringValue = r.ReadString();
            else
                IntValue = r.ReadInt32();
        }

        public void Write(MessageWriter w)
        {
            w.Write((byte)Id);
            if (IsUTF())
                w.Write(StringValue);
            else
                w.Write(IntValue);
        }

        public bool IsUTF()
        {
            if (Id == StatType.Name || Id == StatType.AccountId || Id == StatType.OwnerAccountId
               || Id == StatType.GuildName || Id == StatType.PetName)
                return true;
            return false;
        }

        public StatData Clone()
        {
            if (IsUTF()) return new StatData(Id, StringValue);
            else return new StatData(Id, IntValue);
        }
    }
}
