using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData {
	public struct TileData {

		public static Dictionary<ushort, TileData> Tiles;

		public static TileData ByName(string name) {
			try {
				return Tiles.First(tile => tile.Value.Name == name).Value;
			} catch (Exception e) {
				throw new Exception(string.Format("Tile by name '{0}' not found.", name), e);
			}

		}

		public static TileData ByID(ushort id) {
			if (Tiles.ContainsKey(id))
				return Tiles[id];
			else
				throw new Exception(string.Format("Tile by ID 0x{0:X} not found."));
		}

		public static void Load() {
			Tiles = new Dictionary<ushort, TileData>();
			XDocument doc = XDocument.Parse(RawGameData.Tiles);

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

			PluginUtils.Log("Tiles", "Loaded {0} tiles.", Tiles.Count);
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
				Name, ID);
		}
	}
}
