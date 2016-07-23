using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData {
	public struct ItemData {

		public static Dictionary<ushort, ItemData> Items;

		public static ItemData ByName(string name) {
			try {
				return Items.First(item => item.Value.Name == name).Value;
			} catch (Exception e) {
				throw new Exception(string.Format("Item by name '{0}' not found.", name), e);
			}

		}

		public static ItemData ByID(ushort id) {
			if (Items.ContainsKey(id))
				return Items[id];
			else
				throw new Exception(string.Format("Item by ID 0x{0:X} not found."));
		}

		public enum ItemTier : byte {
			Tier0 = 0,
			Tier1,
			Tier2,
			Tier3,
			Tier4,
			Tier5,
			Tier6,
			Tier7,
			Tier8,
			Tier9,
			Tier10,
			Tier11,
			Tier12,
			Tier13,

			// not yet used but here for future proofing
			Tier14,
			Tier15,

			Untiered = 255
		}

		public static void Load() {
			Items = new Dictionary<ushort, ItemData>();
			XDocument doc = XDocument.Parse(RawGameData.Items);

			foreach (XElement item in doc.Elements()
				.First(elem => elem.Name == "Objects")
				.Elements("Object")
				.Where(elem => elem.Elements("Item").Any())) {

				ItemData t = new ItemData();
				t.ID = Convert.ToUInt16(item.Attribute("type").Value, 16);
				t.Tier = (ItemTier)(item.Elements("Tier").Any() ? sbyte.Parse(item.Element("Tier").Value) : (sbyte)-1);
				t.SlotType = item.Elements("SlotType").Any() ? byte.Parse(item.Element("SlotType").Value) : (byte)0;
				t.RateOfFire = item.Elements("RateOfFire").Any() ? float.Parse(item.Element("RateOfFire").Value) : 1;
				t.FeedPower = item.Elements("feedPower").Any() ? uint.Parse(item.Element("feedPower").Value) : 0;
				t.BagType = item.Elements("BagType").Any() ? byte.Parse(item.Element("BagType").Value) : (byte)0;
				t.MPCost = item.Elements("MpCost").Any() ? byte.Parse(item.Element("MpCost").Value) : (byte)0;
				t.FameBonus = item.Elements("FameBonus").Any() ? byte.Parse(item.Element("FameBonus").Value) : (byte)0;

				t.Soulbound = item.Elements("Soulbound").Any();
				t.Usable = item.Elements("Usable").Any();
				t.Consumable = item.Elements("Consumable").Any();

				t.Name = item.Attribute("id").Value;

				Items[t.ID] = t;
			}

			PluginUtils.Log("Items", "Loaded {0} items.", Items.Count);
		}

		public ushort ID;
		public ItemTier Tier;
		public byte SlotType;
		public float RateOfFire;
		public uint FeedPower;
		public byte BagType;
		public byte MPCost;
		public byte FameBonus;
		public bool Soulbound;
		public bool Usable;
		public bool Consumable;

		public string Name;

		public override string ToString() {
			return string.Format("{0} (0x{1:X})",
				Name, ID);
		}
	}
}
