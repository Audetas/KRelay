using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lib_K_Relay.GameData.DataStructures
{
    public class ObjectStructure : IDataStructure<ushort>
    {
        internal static Dictionary<ushort, ObjectStructure> Load(XDocument doc)
        {
            Dictionary<ushort, ObjectStructure> map = new Dictionary<ushort, ObjectStructure>();

            doc.Element("Objects")
                .Elements("Object")
                .ForEach(obj => {
                    ObjectStructure o = new ObjectStructure(obj);
                    map[o.ID] = o;
                });

            return map;
        }

        /// <summary>
        /// The numerical identifier for this object
        /// </summary>
        public ushort ID { get; private set; }

        /// <summary>
        /// What kind of object this is
        /// </summary>
        public string ObjectClass;

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
        /// Whether this object protects the player from taking ground damage
        /// </summary>
        public bool ProtectFromGroundDamage;

        /// <summary>
        /// Whether this objects protects the player from sinking (into Tomb sand, etc.)
        /// </summary>
        public bool ProtectFromSink;

        /// <summary>
        /// Whether this object is an enemy (e.g. can be damaged)
        /// </summary>
        public bool Enemy;

        /// <summary>
        /// Whether this object is a player
        /// </summary>
        public bool Player;

        /// <summary>
        /// Whether this object is a pet
        /// </summary>
        public bool Pet;

        /// <summary>
        /// ???
        /// </summary>
        public bool DrawOnGround;

        /// <summary>
        /// The size of the enemy
        /// </summary>
        public ushort Size;

        /// <summary>
        /// The size of the shadow of the enemy
        /// </summary>
        public ushort ShadowSize;

        /// <summary>
        /// How much defense the enemy has
        /// </summary>
        public ushort Defense;

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
        public ProjectileStructure[] Projectiles;

        /// <summary>
        /// The text identifier for this object
        /// </summary>
        public string Name { get; private set; }

        public ObjectStructure(XElement obj)
        {
            ID = (ushort)obj.AttrDefault("type", "0x0").ParseHex();

            // if this errors you need to add a new entry to the krObject.Class enum
            ObjectClass = obj.ElemDefault("Class", "GameObject");

            MaxHP = (ushort)obj.ElemDefault("MaxHitPoints", "0").ParseHex();
            XPMult = obj.ElemDefault("XpMult", "0").ParseFloat();

            Static = obj.HasElement("Static");
            OccupySquare = obj.HasElement("OccupySquare");
            EnemyOccupySquare = obj.HasElement("EnemyOccupySquare");
            FullOccupy = obj.HasElement("FullOccupy");
            BlocksSight = obj.HasElement("BlocksSight");
            ProtectFromGroundDamage = obj.HasElement("ProtectFromGroundDamage");
            ProtectFromSink = obj.HasElement("ProtectFromSink");
            Enemy = obj.HasElement("Enemy");
            Player = obj.HasElement("Player");
			Pet = obj.HasElement("Pet");
            DrawOnGround = obj.HasElement("DrawOnGround");

            Size = (ushort)obj.ElemDefault("Size", "0").ParseInt();
            ShadowSize = (ushort)obj.ElemDefault("ShadowSize", "0").ParseInt();
            Defense = (ushort)obj.ElemDefault("Defense", "0").ParseInt();
            Flying = obj.HasElement("Flying");
            God = obj.HasElement("God");

            List<ProjectileStructure> projs = new List<ProjectileStructure>();
            obj.Elements("Projectile").ForEach(projectile => projs.Add(new ProjectileStructure(projectile)));
            Projectiles = projs.ToArray();

            Name = obj.AttrDefault("id", "");
        }

        public override string ToString()
        {
            return string.Format("Object: {0} (0x{1:X})", Name, ID);
        }
    }
}
