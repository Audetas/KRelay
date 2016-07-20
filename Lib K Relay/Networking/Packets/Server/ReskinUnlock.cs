using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.Server
{
	public class ReskinUnlock : Packet
	{
		public int SkinId;

		public override PacketType Type
		{ get { return PacketType.RESKINUNLOCK; } }

		public override void Read(PacketReader r)
		{
			SkinId = r.ReadInt32();
		}

		public override void Write(PacketWriter w)
		{
			w.Write(SkinId);
		}
	}
}
