using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.ObjectStructures {
	public struct Packet {
		public byte ID;

		public string Name;

		public Packet(XElement packet) {
			ID = (byte)packet.ElemDefault("PacketID", "").ParseInt();

			Name = packet.ElemDefault("PacketName", "");
		}

		public override string ToString() {
			return string.Format("Packet: {0} (0x{1:X})", Name, ID);
		}
	}
}
