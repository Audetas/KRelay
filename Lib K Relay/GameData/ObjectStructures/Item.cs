using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.ObjectStructures {
	public struct Item {
		public enum Tiers: byte {
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

		public ushort ID;
		public Tiers Tier;
		public byte SlotType; // todo - make enum?
		public float RateOfFire;
		public uint FeedPower;
		public byte BagType;
		public byte MPCost;
		public byte FameBonus;

		public bool Soulbound;
		public bool Usable;
		public bool Consumable;

		public string Name;

		public Item(XElement item) {
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
