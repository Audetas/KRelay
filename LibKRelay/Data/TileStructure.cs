using LibKRelay.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibKRelay.Data
{
	public class TileStructure
    {
        private static Dictionary<ushort, TileStructure> map;
        static TileStructure()
        {
            map = new Dictionary<ushort, TileStructure>();
            XDocument.Parse(Resources.Tiles).Element("GroundTypes")
                .Elements("Ground")
                .ForEach(tile => {
                    TileStructure t = new TileStructure(tile);
                    map[t.ID] = t;
                });
        }

        public static TileStructure ById(ushort id)
        {
            if (map.ContainsKey(id))
                return map[id];
            return null;
        }

        public static TileStructure ByName(string name)
        {
            foreach (TileStructure obj in map.Values)
                if (obj.Name == name)
                    return obj;
            return null;
        }

        /// <summary>
        /// The numerical identifier of this tile
        /// </summary>
        public ushort ID { get; private set; }

		/// <summary>
		/// Whether this tile can be walked on
		/// </summary>
		public bool NoWalk;

		/// <summary>
		/// How fast you walk across this tile
		/// </summary>
		public float Speed;

		/// <summary>
		/// Whether you sink into this tile visually (e.g. water)
		/// </summary>
		public bool Sink;

		/// <summary>
		/// The minimum damage this tile can deal (e.g. lava)
		/// </summary>
		public ushort MinDamage;

		/// <summary>
		/// The maximum damage this tile can deal (e.g. lava)
		/// </summary>
		public ushort MaxDamage;

        /// <summary>
        /// The texture for this tile
        /// </summary>
        public TextureStructure Texture;

		/// <summary>
		/// The text identifier for this tile
		/// </summary>
		public string Name { get; private set; }

		public TileStructure(XElement tile)
        {
			ID = (ushort)tile.AttrDefault("type", "0x0").ParseHex();
			NoWalk = tile.HasElement("NoWalk");
			Speed = tile.ElemDefault("Speed", "1").ParseFloat();
			Sink = tile.HasElement("Sink");
			MinDamage = (ushort)tile.ElemDefault("MinDamage", "0").ParseInt();
			MaxDamage = (ushort)tile.ElemDefault("MaxDamage", "0").ParseInt();

            // Not even nearly complete, just for testing
            if (tile.HasElement("Texture"))
                Texture = new TextureStructure(tile.Elements("Texture").First());
            else if (tile.HasElement("RandomTexture"))
                Texture = new TextureStructure(tile.Elements("RandomTexture").Elements("Texture").First());

            Name = tile.AttrDefault("id", "");
		}

		public override string ToString()
        {
			return string.Format("Tile: {0} (0x{1:X})", Name, ID);
		}
	}
}
