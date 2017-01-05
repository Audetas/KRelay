using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class EnterArena : Message
    {
        public int Currency;

        public override void Read(MessageReader r)
        {
            Currency = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Currency);
        }
    }
}
