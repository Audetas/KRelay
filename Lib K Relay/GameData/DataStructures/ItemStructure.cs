using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.DataStructures {
	public struct ItemStructure : IDataStructure<ushort> {
		internal static Dictionary<ushort, ItemStructure> Load(XDocument doc) {
			Dictionary<ushort, ItemStructure> map = new Dictionary<ushort, ItemStructure>();

			doc.Element("Objects")
				.Elements("Object")
				.Where(elem => elem.HasElement("Item"))
				.ForEach(item => {
					ItemStructure i = new ItemStructure(item);
					map[i.ID] = i;
				});

			return map;
		}

		public enum Tiers : byte {
			T0 = 0,
			T1,
			T2,
			T3,
			T4,
			T5,
			T6,
			T7,
			T8,
			T9,
			T10,
			T11,
			T12,
			T13,

			// unused, but included for future proofing
			T14,
			T15,

			UT = 255
		}

		/// <summary>
		/// The numerical identifier for this item
		/// </summary>
		public ushort ID { get; private set; }

		/// <summary>
		/// What tier the item is
		/// </summary>
		public Tiers Tier;

		/// <summary>
		/// What slot the item goes into
		/// </summary>
		public byte SlotType; // todo - make enum?

		/// <summary>
		/// How fast this item fires (for weapons)
		/// </summary>
		public float RateOfFire;
		
		/// <summary>
		/// How much feed power this item has
		/// </summary>
		public uint FeedPower;

		/// <summary>
		/// What kind of bag this item drops in
		/// </summary>
		public byte BagType;

		/// <summary>
		/// How much MP the item costs to use (for abilities)
		/// </summary>
		public byte MPCost;

		/// <summary>
		/// How much extra fame is awarded when dying with this item equipped
		/// </summary>
		public byte FameBonus;

		/// <summary>
		/// Whether the item is soulbound or not
		/// </summary>
		public bool Soulbound;

		/// <summary>
		/// Whether the item can be used as an ability
		/// </summary>
		public bool Usable;

		/// <summary>
		/// Whether the item is consumable
		/// </summary>
		public bool Consumable;

		/// <summary>
		/// The text identifier for this item
		/// </summary>
		public string Name { get; private set; }

		public ItemStructure(XElement item) {
			ID = (ushort)item.AttrDefault("type", "0x0").ParseHex();
			Tier = item.HasElement("Tier") ? (Tiers)item.Element("Tier").Value.ParseInt() : Tiers.UT;
			SlotType = (byte)item.ElemDefault("SlotType", "0").ParseInt();
			RateOfFire = item.ElemDefault("RateOfFire", "1").ParseFloat();
			FeedPower = (uint)item.ElemDefault("feedPower", "0").ParseInt();
			BagType = (byte)item.ElemDefault("BagType", "0").ParseInt();
			MPCost = (byte)item.ElemDefault("MpCost", "0").ParseInt();
			FameBonus = (byte)item.ElemDefault("FameBonus", "0").ParseInt();

			Soulbound = item.HasElement("Soulbound");
			Usable = item.HasElement("Usable");
			Consumable = item.HasElement("Consumable");

			Name = item.AttrDefault("id", "");
		}

		public override string ToString() {
			return string.Format("Item: {0} (0x{1:X})", Name, ID);
		}
	}
}
