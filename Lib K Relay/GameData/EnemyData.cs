using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData {

	public struct EnemyData {

		public static Dictionary<int, EnemyData> Enemies;

		public static EnemyData ByName(string name) {
			return Enemies.First(enemy => enemy.Value.Name == name).Value;
		}

		public static EnemyData ByID(uint id) {
			return Enemies[(ushort)id];
		}

		public struct Projectile {
			public byte ID;
			public ushort Damage;
			public ushort Speed;
			public ushort Size;
			public uint Lifetime;

			public bool MultiHit;
			public bool Boomerang;
			public bool ArmorPiercing;

			public Dictionary<ConditionEffects, float> Effects;
			public string Name;
		}

		public static void Load() {
			Enemies = new Dictionary<int, EnemyData>();
			XDocument doc = XDocument.Parse(RawGameData.Enemies);

			// there's probably a better way to do this with LINQ
			foreach (XElement enemy in doc.Elements()
				.First(elem => elem.Name == "Objects")
				.Elements("Object")
				.Where(elem => elem.Elements("Enemy").Any())) {

				EnemyData e = new EnemyData();

				e.ID = Convert.ToUInt16(enemy.Attribute("type").Value, 16);
				e.Size = enemy.Elements("Size").Any() ? ushort.Parse(enemy.Element("Size").Value) : (ushort)0;
				e.ShadowSize = enemy.Elements("ShadowSize").Any() ? ushort.Parse(enemy.Element("ShadowSize").Value) : (ushort)0;
				e.MaxHP = enemy.Elements("MaxHitPoints").Any() ? uint.Parse(enemy.Element("MaxHitPoints").Value) : 0;
				e.Defense = enemy.Elements("Defense").Any() ? ushort.Parse(enemy.Element("Defense").Value) : (ushort)0;
				e.Flying = enemy.Elements("Flying").Any();
				e.God = enemy.Elements("God").Any();
				List<Projectile> projs = new List<Projectile>();
				foreach (XElement projectile in enemy.Elements("Projectile")) {
					Projectile p = new Projectile();
					p.ID = projectile.Attributes("id").Any() ? byte.Parse(projectile.Attribute("id").Value) : (byte)0;
					p.Damage = projectile.Elements("Damage").Any() ? ushort.Parse(projectile.Element("Damage").Value) : (ushort)0;
					p.Speed = projectile.Elements("Speed").Any() ? ushort.Parse(projectile.Element("Speed").Value) : (ushort)0;
					p.Size = projectile.Elements("Size").Any() ? ushort.Parse(projectile.Element("Size").Value) : (ushort)0;
					p.Lifetime = projectile.Elements("Lifetime").Any() ? ushort.Parse(projectile.Element("Lifetime").Value) : (uint)0;
					p.MultiHit = projectile.Elements("MultiHit").Any();
					p.Boomerang = projectile.Elements("Boomerang").Any();
					p.ArmorPiercing = projectile.Elements("ArmorPiercing").Any();
					p.Effects = new Dictionary<ConditionEffects, float>();
					p.Name = projectile.Element("ObjectId").Value;
					foreach (XElement cond in projectile.Elements("ConditionEffect")) {
						p.Effects[(ConditionEffects)Enum.Parse(typeof(ConditionEffects), cond.Value.Replace(" ", ""))] = 
							// for some reason, there's a condition effect without a duration...
							cond.Attribute("duration") != null ? float.Parse(cond.Attribute("duration").Value) : 0;
					}
				}
				e.Projectiles = projs.ToArray();
				e.Name = enemy.Attribute("id").Value;

				Enemies[e.ID] = e;
			}

			PluginUtils.Log("Enemies", "Parsed {0} enemies.", Enemies.Count);
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

		public override string ToString() {
			return string.Format("{0} (0x{1:X})", Name, ID);
		}
	}
}
