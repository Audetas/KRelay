using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class StatData : IDataObject
    {
        public int Id;
        public int IntValue;
        public string StringValue;

        public bool IsStringData()
        {
            switch (Id)
            {
                case 31:
                case 62:
                case 82:
                case 38:
                case 54: return true;
            }
            return false;
        }

        public IDataObject Read(PacketReader r)
        {
            Id = r.ReadInt32();
            if (IsStringData()) StringValue = r.ReadString();
            else IntValue = r.ReadInt32();

            return this;
        }

        public void Write(PacketWriter w)
        {
            w.Write(Id);
            if (IsStringData()) w.Write(StringValue);
            else w.Write(IntValue);
        }

        public override string ToString()
        {
            return "{ " + " }";
        }
    }
}
