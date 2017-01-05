using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
	public class KeyInfoResponse : Message
	{
		public byte[] Response;

		public override void Read(MessageReader r)
		{
			Response = r.ReadBytes((int)r.BaseStream.Length - 5);
		}

		public override void Write(MessageWriter w)
		{
			w.Write(Response);
		}
	}
}
