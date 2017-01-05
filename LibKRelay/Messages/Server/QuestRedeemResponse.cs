using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class QuestRedeemResponse : Message
    {
        public bool Success;
        public string Message;

        public override void Read(MessageReader r)
        {
            Success = r.ReadBoolean();
            Message = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Success);
            w.Write(Message);
        }
    }
}
