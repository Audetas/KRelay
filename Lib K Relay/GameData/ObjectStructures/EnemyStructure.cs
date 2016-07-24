using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.ObjectStructures {
	public struct EnemyStructure {
		public struct Projectile {
			/// <summary>
			/// The numerical identifier for this projectile
			/// </summary>
			public byte ID;

			/// <summary>
			/// How much damage the projectile deals
			/// </summary>
			public ushort Damage;

			/// <summary>
			/// How fast the projectile moves
			/// </summary>
			public ushort Speed;

			/// <summary>
			/// The size of the projectile
			/// </summary>
			public ushort Size;

			/// <summary>
			/// The lifetime of the projectile, in milliseconds
			/// </summary>
			public uint Lifetime;

			/// <summary>
			/// Whether the projectile hits multiple targets
			/// </summary>
			public bool MultiHit;

			/// <summary>
			/// Whether the projectile returns to the owner
			/// </summary>
			public bool Boomerang;

			/// <summary>
			/// Whether the projectile pierces armor (ignores defense)
			/// </summary>
			public bool ArmorPiercing;

			/// <summary>
			/// What status effects, if any, the projectile applies (name: duration in seconds)
			/// </summary>
			public Dictionary<string, float> StatusEffects;
			
			/// <summary>
			/// The text identifier for this projectile
			/// </summary>
			public string Name;
		}

		/// <summary>
		/// The numerical identifier for this enemy
		/// </summary>
		public ushort ID;

		/// <summary>
		/// The size of the enemy
		/// </summary>
		public ushort Size;

		/// <summary>
		/// The size of the shadow of the enemy
		/// </summary>
		public ushort ShadowSize;

		/// <summary>
		/// The maximum HP this enemy can have
		/// </summary>
		public uint MaxHP;

		/// <summary>
		/// How much defense the enemy has
		/// </summary>
		public ushort Defense;

		/// <summary>
		/// How much XP the enemy grants
		/// </summary>
		public float XPMult;

		/// <summary>
		/// Whether the enemy flies
		/// </summary>
		public bool Flying;

		/// <summary>
		/// Whether the enemy is a god (e.g. contributes to god kills)
		/// </summary>
		public bool God;

		/// <summary>
		/// What projectiles this enemy can fire
		/// </summary>
		public Projectile[] Projectiles;

		/// <summary>
		/// The text identifier for this enemy
		/// </summary>
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
