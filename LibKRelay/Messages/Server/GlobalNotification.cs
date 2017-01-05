using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class GlobalNotification : Message
    {
        public int TypeId ;
        public string Text ;

        public override void Read(MessageReader r)
        {
            TypeId = r.ReadInt32();
            Text = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(TypeId);
            w.Write(Text);
        }
    }
}
