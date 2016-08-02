using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.DataStructures
{
    public class ProjectileStructure : IDataStructure<byte>
    {
        /// <summary>
        /// The numerical identifier for this projectile
        /// </summary>
        public byte ID { get; private set; }

        /// <summary>
        /// How much damage the projectile deals
        /// </summary>
        public int Damage;

        /// <summary>
        /// How fast the projectile moves
        /// </summary>
        public float Speed;

        /// <summary>
        /// The size of the projectile
        /// </summary>
        public int Size;

        /// <summary>
        /// The lifetime of the projectile, in milliseconds
        /// </summary>
        public int Lifetime;

        public int MaxDamage;
        public int MinDamage;

        public float Magnitude;
        public float Amplitude;
        public float Frequency;

        public bool Wavy;
        public bool Parametric;
        public bool Boomerang;
        public bool ArmorPiercing;
        public bool MultiHit;
        public bool PassesCover;

        /// <summary>
        /// What status effects, if any, the projectile applies (name: duration in seconds)
        /// </summary>
        public Dictionary<string, float> StatusEffects;

        /// <summary>
        /// The text identifier for this projectile
        /// </summary>
        public string Name { get; private set; }

        public ProjectileStructure(XElement projectile)
        {
            ID = (byte)projectile.AttrDefault("id", "0").ParseInt();
            Damage = projectile.ElemDefault("Damage", "0").ParseInt();
            Speed = projectile.ElemDefault("Speed", "0").ParseFloat() / 10000f;
            Size = projectile.ElemDefault("Size", "0").ParseInt();
            Lifetime = projectile.ElemDefault("LifetimeMS", "0").ParseInt();

            MaxDamage = projectile.ElemDefault("MaxDamage", "0").ParseInt();
            MinDamage = projectile.ElemDefault("MinDamage", "0").ParseInt();

            Magnitude = projectile.ElemDefault("Magnitude", "0").ParseFloat();
            Amplitude = projectile.ElemDefault("Amplitude", "0").ParseFloat();
            Frequency = projectile.ElemDefault("Frequency", "0").ParseFloat();

            Wavy = projectile.HasElement("Wavy");
            Parametric = projectile.HasElement("Parametric");
            Boomerang = projectile.HasElement("Boomerang");
            ArmorPiercing = projectile.HasElement("ArmorPiercing");
            MultiHit = projectile.HasElement("MultiHit");
            PassesCover = projectile.HasElement("PassesCover");

            var effects = new Dictionary<string, float>();
            projectile.Elements("ConditionEffect")
                .ForEach(effect => effects[effect.Value] = effect.AttrDefault("duration", "0").ParseFloat());

            StatusEffects = effects;
            Name = projectile.ElemDefault("ObjectId", "");
        }

        public override string ToString()
        {
            return string.Format("Projectile: {0} (0x{1:X})", Name, ID);
        }
    }
}
