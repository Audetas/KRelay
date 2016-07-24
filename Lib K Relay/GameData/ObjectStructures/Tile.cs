using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.ObjectStructures {
	public struct Tile {

		public ushort ID;
		public bool NoWalk;
		public float Speed;
		public bool Sink;
		public ushort MinDamage;
		public ushort MaxDamage;

		public string Name;

		public Tile(XElement tile) {
			ID = (ushort)tile.AttrDefault("type", "0x0").ParseHex();
			NoWalk = tile.HasElement("NoWalk");
			Speed = tile.ElemDefault("Speed", "1").ParseFloat();
			Sink = tile.HasElement("Sink");
			MinDamage = (ushort)tile.ElemDefault("MinDamage", "0").ParseInt();
			MaxDamage = (ushort)tile.ElemDefault("MaxDamage", "0").ParseInt();

			Name = tile.AttrDefault("id", "");
		}

		public override string ToString() {
			return string.Format("Tile: {0} (0x{1:X})", Name, ID);
		}
	}
}
