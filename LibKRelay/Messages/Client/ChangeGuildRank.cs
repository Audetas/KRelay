using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class ChangeGuildRank : Message
    {
        public string Name;
        public int GuildRank;

        public override void Read(MessageReader r)
        {
            Name = r.ReadString();
            GuildRank = r.ReadInt32();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Name);
            w.Write(GuildRank);
        }
    }
}
