using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class PlayerText : Message
    {
        public string Text;

        public override void Read(MessageReader r)
        {
            Text = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Text);
        }
    }
}
