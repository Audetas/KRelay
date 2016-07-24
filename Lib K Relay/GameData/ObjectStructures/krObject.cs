using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.ObjectStructures {
	public struct krObject {
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

		public ushort ID;
		public Class ObjectClass;
		public ushort MaxHP;
		public float XPMult;

		public bool Static;
		public bool OccupySquare;
		public bool EnemyOccupySquare;
		public bool FullOccupy;
		public bool BlocksSight;
		public bool Enemy;
		public bool DrawOnGround;

		public string Name;

		public krObject(XElement obj) {
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
