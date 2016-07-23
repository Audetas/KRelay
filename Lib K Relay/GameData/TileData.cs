using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData {
	public struct TileData {

		public static Dictionary<int, TileData> Tiles;

		public static TileData ByName(string name) {
			return Tiles.First(tile => tile.Value.Name == name).Value;
		}

		public static TileData ByID(uint id) {
			return Tiles[(ushort)id];
		}

		public static void Load() {
			Tiles = new Dictionary<int, TileData>();
			XDocument doc = XDocument.Parse(RawGameData.Tiles);

			// there's probably a better way to do this with LINQ
			foreach (XElement tile in doc.Elements()
				.First(elem => elem.Name == "GroundTypes")
				.Elements("Ground")
				.Where(elem => elem.Attribute("type") != null && elem.Attribute("id") != null)) {

				TileData t = new TileData();
				t.ID = Convert.ToUInt16(tile.Attribute("type").Value, 16);
				t.NoWalk = tile.Elements("NoWalk").Any();
				t.Speed = tile.Elements("Speed").Any() ? float.Parse(tile.Elements("Speed").First().Value) : 1f;
				t.Sink = tile.Elements("Sink").Any();
				t.MinDamage = tile.Elements("MinDamage").Any() ? ushort.Parse(tile.Element("MinDamage").Value) : (ushort)0;
				t.MaxDamage = tile.Elements("MaxDamage").Any() ? ushort.Parse(tile.Element("MaxDamage").Value) : (ushort)0;
				t.Name = tile.Attribute("id").Value;

				Tiles[t.ID] = t;
			}

			PluginUtils.Log("Tiles", "Parsed {0} tiles.", Tiles.Count);
		}

		public ushort ID;
		public bool NoWalk;
		public float Speed;
		public bool Sink;
		public ushort MinDamage;
		public ushort MaxDamage;
		public string Name;

		public override string ToString() {
			return string.Format("{0} (0x{1:X})",
				Name, ID, NoWalk, Speed, Sink, MinDamage, MaxDamage);
		}
	}
}
