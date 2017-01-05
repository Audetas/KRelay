using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class InvitedToGuild : Message
    {
		public string Name;
		public string GuildName;

        public override void Read(MessageReader r)
        {
            Name = r.ReadString();
            GuildName = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Name);
            w.Write(GuildName);
        }
    }
}
