using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class NameResult : Message
    {
		public bool Success;
		public string ErrorText;

        public override void Read(MessageReader r)
        {
            Success = r.ReadBoolean();
            ErrorText = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Success);
            w.Write(ErrorText);
        }
    }
}
