using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class JoinGuild : Message
    {
        public string GuildName;

        public override void Read(MessageReader r)
        {
            GuildName = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(GuildName);
        }
    }
}
