using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Client
{
    public class KeyInfoRequest : Message
    {
		public byte[] Request;

        public override void Read(MessageReader r)
        {
			Request = r.ReadBytes((int)r.BaseStream.Length - 5);
        }

        public override void Write(MessageWriter w)
        {
            w.Write(Request);
        }
    }
}
