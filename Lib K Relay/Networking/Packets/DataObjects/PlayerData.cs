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
        public int AccountId;
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
        public bool NameRegistered;
        public int Fame;
        public int FameGoal;
        public int GlowingEffect;
        public string Guild;
        public int GuildRank;
        public int Breath;
        public int HealthpotCount;
        public int ManapotCount;
        public int BoolHasbackPack;
        public int PetSkinObjectType;
        public Location Pos = new Location();
        // Custom
        public Classes Class;

        public PlayerData(int ownerObjectId)
        {
            OwnerObjectId = ownerObjectId;
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
            if (id == 0) MaxHealth = intValue;
            else if (id == 1) Health = intValue;
            else if (id == 3) MaxMana = intValue;
            else if (id == 4) Mana = intValue;
            else if (id == 5) XpGoal = intValue;
            else if (id == 6) Xp = intValue;
            else if (id == 7) Level = intValue;
            else if (id == 8) Slot[0] = intValue;
            else if (id == 9) Slot[1] = intValue;
            else if (id == 10) Slot[2] = intValue;
            else if (id == 11) Slot[3] = intValue;
            else if (id == 12) Slot[4] = intValue;
            else if (id == 13) Slot[5] = intValue;
            else if (id == 14) Slot[6] = intValue;
            else if (id == 15) Slot[7] = intValue;
            else if (id == 16) Slot[8] = intValue;
            else if (id == 17) Slot[9] = intValue;
            else if (id == 18) Slot[11] = intValue;
            else if (id == 19) Slot[10] = intValue;
            else if (id == 20) Attack = intValue;
            else if (id == 21) Defense = intValue;
            else if (id == 22) Speed = intValue;
            else if (id == 26) Vitality = intValue;
            else if (id == 27) Wisdom = intValue;
            else if (id == 28) Dexterity = intValue;
            else if (id == 29) Effects = intValue;
            else if (id == 30) Stars = intValue;
            else if (id == 31) Name = stringValue;
            else if (id == 35) RealmGold = intValue;
            else if (id == 36) Price = intValue;
            //else if (id == 37) CanEnterPortal = bool.Parse(stringValue);
            else if (id == 38) AccountId = intValue;
            else if (id == 39) CurrentFame = intValue; //fame you got when you died
            else if (id == 46) HealthBonus = intValue;
            else if (id == 47) ManaBonus = intValue;
            else if (id == 48) AttackBonus = intValue;
            else if (id == 49) DefenseBonus = intValue;
            else if (id == 50) SpeedBonus = intValue;
            else if (id == 51) VitalityBonus = intValue;
            else if (id == 52) WisdomBonus = intValue;
            else if (id == 53) DexterityBonus = intValue;
            else if (id == 55) NameChangeRankRequired = intValue;
            //else if (id == 56) NameRegistered = bool.Parse(stringValue);
            else if (id == 57) Fame = intValue; //fame on this character
            else if (id == 58) FameGoal = intValue;
            else if (id == 59) GlowingEffect = intValue;
            else if (id == 62) Guild = stringValue;
            else if (id == 63) GuildRank = intValue;
            else if (id == 64) Breath = intValue;
            else if (id == 69) HealthpotCount = intValue;
            else if (id == 70) ManapotCount = intValue;
            else if (id == 79) BackPack[0] = intValue;
            else if (id == 80) BackPack[1] = intValue;
            else if (id == 81) BackPack[2] = intValue;
            else if (id == 82) BackPack[3] = intValue;
            else if (id == 83) BackPack[4] = intValue;
            else if (id == 84) BackPack[5] = intValue;
            else if (id == 85) BackPack[6] = intValue;
            else if (id == 86) BackPack[7] = intValue;
            else if (id == 79) BoolHasbackPack = intValue;
            else if (id == 80) PetSkinObjectType = intValue;
        }

        public bool HasConditionEffect(ConditionEffects effect)
        {
            return (Effects & (int)effect) != 0;
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
