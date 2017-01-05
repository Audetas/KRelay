using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
    public class TradeDone : Message
    {
		/*
		TradeSuccessful = 0
		PlayerCanceled = 1
		*/

        public int Result;
        public string Message;

        public override void Read(MessageReader r)
        {
            Result = r.ReadInt32();
            Message = r.ReadString();
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Result);
            w.Write(Message);
        }
    }
}
