using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class ArenaNextWave : Message
    {
        public int TypeId;

        public override void Read(MessageReader r)
        {
            TypeId = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(TypeId);
        }
    }
}
