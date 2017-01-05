using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class InvResult : Message
    {
        public int Result ;

        public override void Read(MessageReader r)
        {
            Result = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Result);
        }
    }
}
