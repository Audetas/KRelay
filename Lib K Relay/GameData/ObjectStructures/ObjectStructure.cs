using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.ObjectStructures {
	public struct ObjectStructure {
		public enum Class : byte {
			GameObject,
			Wall,
			DoubleWall,
			ConnectedWall,
			Sign,
			MoneyChanger,
			CharacterChanger,
			Stalagmite,
			CaveWall,
			NameChanger,
			GuildRegister,
			GuildChronicle,
			GuildBoard,
			ReskinVendor,
			YardUpgrader,
			PetUpgrader,
			ArenaGuard,
			Unknown = 255
		}

		/// <summary>
		/// The numerical identifier for this object
		/// </summary>
		public ushort ID;

		/// <summary>
		/// What kind of object this is
		/// </summary>
		public Class ObjectClass;

		/// <summary>
		/// Maximum HP this object can have (for walls/other destructible terrain)
		/// </summary>
		public ushort MaxHP;

		/// <summary>
		/// How much XP is granted when destroying this object
		/// </summary>
		public float XPMult;

		/// <summary>
		/// Unknown
		/// </summary>
		public bool Static;

		/// <summary>
		/// Whether this object impedes movement (?)
		/// </summary>
		public bool OccupySquare;

		/// <summary>
		/// ???
		/// </summary>
		public bool EnemyOccupySquare;

		/// <summary>
		/// ???
		/// </summary>
		public bool FullOccupy;

		/// <summary>
		/// Whether this object blocks vision
		/// </summary>
		public bool BlocksSight;

		/// <summary>
		/// Whether this object is an enemy (e.g. can be damaged)
		/// </summary>
		public bool Enemy;

		/// <summary>
		/// ???
		/// </summary>
		public bool DrawOnGround;

		/// <summary>
		/// The text identifier for this object
		/// </summary>
		public string Name;

		public ObjectStructure(XElement obj) {
			ID = (ushort)obj.AttrDefault("type", "0x0").ParseHex();

			// if this errors you need to add a new entry to the krObject.Class enum
			ObjectClass = (Class)Enum.Parse(typeof(Class), obj.ElemDefault("Class", "GameObject"));

			MaxHP = (ushort)obj.ElemDefault("MaxHitPoints", "0").ParseHex();
			XPMult = obj.ElemDefault("XpMult", "0").ParseFloat();

			Static = obj.HasElement("Static");
			OccupySquare = obj.HasElement("OccupySquare");
			EnemyOccupySquare = obj.HasElement("EnemyOccupySquare");
			FullOccupy = obj.HasElement("FullOccupy");
			BlocksSight = obj.HasElement("BlocksSight");
			Enemy = obj.HasElement("Enemy");
			DrawOnGround = obj.HasElement("DrawOnGround");

			Name = obj.AttrDefault("id", "");
		}

		public override string ToString() {
			return string.Format("Object: {0} (0x{1:X})", Name, ID);
		}
	}
}
