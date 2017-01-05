using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class ArenaDeath : Message
    {
        public int RestartPrice;

        public override void Read(MessageReader r)
        {
            RestartPrice = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(RestartPrice);
        }
    }
}
