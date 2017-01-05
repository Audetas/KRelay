using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class Failure : Message
    {
        public int ErrorId;
        public string ErrorMessage;

        public override void Read(MessageReader r)
        {
            ErrorId = r.ReadInt32();
            ErrorMessage = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(ErrorId);
            w.Write(ErrorMessage);
        }
    }
}
