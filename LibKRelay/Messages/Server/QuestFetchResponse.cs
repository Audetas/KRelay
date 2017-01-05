using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class QuestFetchResponse : Message
    {
        public int Tier;
        public string Goal;
        public string Description;
        public string Image;

        public override void Read(MessageReader r)
        {
            Tier = r.ReadInt32();
            Goal = r.ReadString();
            Description = r.ReadString();
            Image = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Tier);
            w.Write(Goal);
            w.Write(Description);
            w.Write(Image);
        }
    }
}
