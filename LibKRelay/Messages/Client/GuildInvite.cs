using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class GuildInvite : Message
    {
        public string Name;

        public override void Read(MessageReader r)
        {
            Name = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Name);
        }
    }
}
