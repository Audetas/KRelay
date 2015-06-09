using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Networking.Packets.DataObjects
{
    public class PlayerData // TODO: Add the rest of the stats
    {
        public int OwnerObjectId;
        public string MapName;
        public bool TeleportAllowed;
        public int MapWidth;
        public int MapHeight;

        public int MaxHealth;
        public int Health;
        public int MaxMana;
        public int Mana;
        public int XpGoal;
        public int Xp;
        public int Level = 1;
        public int[] Slot = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        public int[] BackPack = { -1, -1, -1, -1, -1, -1, -1, -1 };
        public int Attack;
        public int Defense;
        public int Speed;
        public int Vitality;
        public int Wisdom;
        public int Dexterity;
        public int Effects;
        public int Stars;
        public string Name;
        public int RealmGold;
        public int Price;
        public bool CanEnterPortal;
        public string AccountId;
        public int CurrentFame;
        public int HealthBonus;
        public int ManaBonus;
        public int AttackBonus;
        public int DefenseBonus;
        public int SpeedBonus;
        public int VitalityBonus;
        public int WisdomBonus;
        public int DexterityBonus;
        public int NameChangeRankRequired;
        public bool NameChosen;
        public int Fame;
        public int FameGoal;
        public bool GlowingEffect;
        public string Guild;
        public int GuildRank;
        public int Breath;
        public int HealthpotCount;
        public int ManapotCount;
        public bool HasBackpack;
        public int Skin;
        public Location Pos = new Location();
        // Custom
        public Classes Class;

        public PlayerData(int ownerObjectId)
        {
            OwnerObjectId = ownerObjectId;
            Name = "";
        }

        public void Parse(UpdatePacket update)
        {
            foreach (Entity newObject in update.NewObjs)
                if (newObject.Status.ObjectId == OwnerObjectId)
                {
                    Class = (Classes)newObject.ObjectType;
                    foreach (StatData data in newObject.Status.Data)
                        Parse(data.Id, data.IntValue, data.StringValue);
                }
        }

        public void Parse(NewTickPacket newTick)
        {
            foreach (Status status in newTick.Statuses)
                if (status.ObjectId == OwnerObjectId)
                    foreach (StatData data in status.Data)
                    {
                        Pos = status.Position;
                        Parse(data.Id, data.IntValue, data.StringValue);
                    }
        }

        public void Parse(int id, int intValue, string stringValue)
        {
            if (id == StatsType.MaximumHP) MaxHealth = intValue;
            else if (id == StatsType.HP) Health = intValue;
            else if (id == StatsType.MaximumMP) MaxMana = intValue;
            else if (id == StatsType.MP) Mana = intValue;
            else if (id == StatsType.ExperienceGoal) XpGoal = intValue;
            else if (id == StatsType.Experience) Xp = intValue;
            else if (id == StatsType.Level) Level = intValue;
            else if (id == StatsType.Inventory0) Slot[0] = intValue;
            else if (id == StatsType.Inventory1) Slot[1] = intValue;
            else if (id == StatsType.Inventory2) Slot[2] = intValue;
            else if (id == StatsType.Inventory3) Slot[3] = intValue;
            else if (id == StatsType.Inventory4) Slot[4] = intValue;
            else if (id == StatsType.Inventory5) Slot[5] = intValue;
            else if (id == StatsType.Inventory6) Slot[6] = intValue;
            else if (id == StatsType.Inventory7) Slot[7] = intValue;
            else if (id == StatsType.Inventory8) Slot[8] = intValue;
            else if (id == StatsType.Inventory9) Slot[9] = intValue;
            else if (id == StatsType.Inventory10) Slot[10] = intValue;
            else if (id == StatsType.Inventory11) Slot[11] = intValue;
            else if (id == StatsType.Attack) Attack = intValue;
            else if (id == StatsType.Defense) Defense = intValue;
            else if (id == StatsType.Speed) Speed = intValue;
            else if (id == StatsType.Vitality) Vitality = intValue;
            else if (id == StatsType.Wisdom) Wisdom = intValue;
            else if (id == StatsType.Dexterity) Dexterity = intValue;
            else if (id == StatsType.Effects) Effects = intValue;
            else if (id == StatsType.Stars) Stars = intValue;
            else if (id == StatsType.Name) Name = stringValue;
            else if (id == StatsType.Credits) RealmGold = intValue;
            else if (id == StatsType.SellablePrice) Price = intValue;
            //else if (id == 37) CanEnterPortal = bool.Parse(stringValue);
            else if (id == StatsType.AccountId) AccountId = stringValue;
            else if (id == StatsType.CurrentFame) CurrentFame = intValue; //fame you got when you died
            else if (id == StatsType.HPBoost) HealthBonus = intValue;
            else if (id == StatsType.MPBoost) ManaBonus = intValue;
            else if (id == StatsType.AttackBonus) AttackBonus = intValue;
            else if (id == StatsType.DefenseBonus) DefenseBonus = intValue;
            else if (id == StatsType.SpeedBonus) SpeedBonus = intValue;
            else if (id == StatsType.VitalityBonus) VitalityBonus = intValue;
            else if (id == StatsType.WisdomBonus) WisdomBonus = intValue;
            else if (id == StatsType.DexterityBonus) DexterityBonus = intValue;
            else if (id == StatsType.NameChangerStar) NameChangeRankRequired = intValue;
            else if (id == StatsType.NameChosen) NameChosen = intValue > 0;
            else if (id == StatsType.Fame) Fame = intValue; //fame on this character
            else if (id == StatsType.FameGoal) FameGoal = intValue;
            else if (id == StatsType.Glowing) GlowingEffect = intValue > -1;
            else if (id == StatsType.Guild) Guild = stringValue;
            else if (id == StatsType.GuildRank) GuildRank = intValue;
            else if (id == StatsType.OxygenBar) Breath = intValue;
            else if (id == StatsType.HealStackCount) HealthpotCount = intValue;
            else if (id == StatsType.MagicStackCount) ManapotCount = intValue;
            else if (id == StatsType.Backpack0) BackPack[0] = intValue;
            else if (id == StatsType.Backpack1) BackPack[1] = intValue;
            else if (id == StatsType.Backpack2) BackPack[2] = intValue;
            else if (id == StatsType.Backpack3) BackPack[3] = intValue;
            else if (id == StatsType.Backpack4) BackPack[4] = intValue;
            else if (id == StatsType.Backpack5) BackPack[5] = intValue;
            else if (id == StatsType.Backpack6) BackPack[6] = intValue;
            else if (id == StatsType.Backpack7) BackPack[7] = intValue;
            else if (id == StatsType.Has_Backpack) HasBackpack = intValue > 0;
            else if (id == StatsType.Skin) Skin = intValue;
        }

        public bool HasConditionEffect(ConditionEffects effect)
        {
            return (Effects & (int)effect) != 0;
        }

        public float TilesPerTick()
        {
            // Ticks per second = 5
            return (4.0f + 5.6f * (Speed / 75.0f)) / 5.0f;
        }

        public override string ToString()
        {
            // Use reflection to get the the non-null fields and arrange them into a table.
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Public |
                                              BindingFlags.NonPublic |
                                              BindingFlags.Instance);

            StringBuilder s = new StringBuilder();
            s.Append(OwnerObjectId + "'s PlayerData Instance");
            foreach (FieldInfo f in fields)
                if (f.GetValue(this) != null)
                    s.Append("\n\t" + f.Name + " => " + f.GetValue(this));
            return s.ToString();
        }
    }
}
