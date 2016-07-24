using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.ObjectStructures {
	public struct EnemyStructure {
		public struct Projectile {
			public byte ID;
			public ushort Damage;
			public ushort Speed;
			public ushort Size;
			public uint Lifetime;

			public bool MultiHit;
			public bool Boomerang;
			public bool ArmorPiercing;

			public Dictionary<string, float> StatusEffects;
			public string Name;
		}

		public ushort ID;
		public ushort Size;
		public ushort ShadowSize;
		public uint MaxHP;
		public ushort Defense;
		public float XPMult;

		public bool Flying;
		public bool God;

		public Projectile[] Projectiles;

		public string Name;

		public EnemyStructure(XElement enemy) {
			ID = (ushort)enemy.AttrDefault("type", "0x0").ParseHex();
			Size = (ushort)enemy.ElemDefault("Size", "0").ParseInt();
			ShadowSize = (ushort)enemy.ElemDefault("ShadowSize", "0").ParseInt();
			MaxHP = (uint)enemy.ElemDefault("MaxHitPoints", "0").ParseInt();
			Defense = (ushort)enemy.ElemDefault("Defense", "0").ParseInt();
			XPMult = enemy.ElemDefault("XpMult", "1").ParseFloat();

			Flying = enemy.HasElement("Flying");
			God = enemy.HasElement("God");

			List<Projectile> projs = new List<Projectile>();
			enemy.Elements("Projectile").ForEach(projectile => {
				Projectile p = new Projectile();

				p.ID = (byte)projectile.AttrDefault("id", "0").ParseInt();
				p.Damage = (ushort)projectile.ElemDefault("Damage", "0").ParseInt();
				p.Speed = (ushort)projectile.ElemDefault("Speed", "0").ParseInt();
				p.Size = (ushort)projectile.ElemDefault("Size", "0").ParseInt();
				p.Lifetime = (uint)projectile.ElemDefault("LifetimeMS", "0").ParseInt();

				p.MultiHit = projectile.HasElement("MultiHit");
				p.Boomerang = projectile.HasElement("Boomerang");
				p.ArmorPiercing = projectile.HasElement("ArmorPiercing");

				p.StatusEffects = new Dictionary<string, float>();
				projectile.Elements("ConditionEffect")
					.ForEach(effect => p.StatusEffects[effect.Value] = effect.AttrDefault("duration", "0").ParseFloat());

				p.Name = projectile.ElemDefault("ObjectId", "");

				projs.Add(p);
			});

			Projectiles = projs.ToArray();
			Name = enemy.AttrDefault("id", "");
		}

		public override string ToString() {
			return string.Format("Enemy: {0} (0x{1:X})", Name, ID);
		}
	}
}
