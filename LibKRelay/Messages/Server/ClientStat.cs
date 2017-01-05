using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class ClientStat : Message
    {
        public string Name;
        public int Value;

        public override void Read(MessageReader r)
        {
            Name = r.ReadString();
            Value = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Name);
            w.Write(Value);
        }
    }
}
