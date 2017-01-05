using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay.Messages.Server
{
	public class ReskinUnlock : Message
	{
		public int SkinId;

		public override void Read(MessageReader r)
		{
			SkinId = r.ReadInt32();
		}

		public override void Write(MessageWriter w)
		{
			w.Write(SkinId);
		}
	}
}
