using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibKRelay
{
    public static class Constants
    {
        public static string Key0 = "311f80691451c71d09a13a2a6e";
        public static string Key1 = "72c5583cafb6818995cdd74b80";
    }

    public enum Tiers : byte
    {
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

    [Flags]
    public enum ConditionEffects
    {
        Dead = 1 << 0,
        Quiet = 1 << 1,
        Weak = 1 << 2,
        Slowed = 1 << 3,
        Sick = 1 << 4,
        Dazed = 1 << 5,
        Stunned = 1 << 6,
        Blind = 1 << 7,
        Hallucinating = 1 << 8,
        Drunk = 1 << 9,
        Confused = 1 << 10,
        StunImmume = 1 << 11,
        Invisible = 1 << 12,
        Paralyzed = 1 << 13,
        Speedy = 1 << 14,
        Bleeding = 1 << 15,
        NotUsed = 1 << 16,
        Healing = 1 << 17,
        Damaging = 1 << 18,
        Berserk = 1 << 19,
        Paused = 1 << 20,
        Stasis = 1 << 21,
        StasisImmune = 1 << 22,
        Invincible = 1 << 23,
        Invulnerable = 1 << 24,
        Armored = 1 << 25,
        ArmorBroken = 1 << 26,
        Hexed = 1 << 27,
        AnotherSpeedy = 1 << 28,
        Unstable = 1 << 29,
        Darkness = 1 << 30,
        Curse = 1 << 31
    }

    public enum ConditionEffectIndex
    {
        Dead = 0,
        Quiet = 1,
        Weak = 2,
        Slowed = 3,
        Sick = 4,
        Dazed = 5,
        Stunned = 6,
        Blind = 7,
        Hallucinating = 8,
        Drunk = 9,
        Confused = 10,
        StunImmume = 11,
        Invisible = 12,
        Paralyzed = 13,
        Speedy = 14,
        Bleeding = 15,
        NotUsed = 16,
        Healing = 17,
        Damaging = 18,
        Berserk = 19,
        Paused = 20,
        Stasis = 21,
        StasisImmune = 22,
        Invincible = 23,
        Invulnerable = 24,
        Armored = 25,
        ArmorBroken = 26,
        Hexed = 27,
        AnotherSpeedy = 28,
        Unstable = 29,
        Darkness = 30,
        Curse = 31
    }

    public enum EffectType
    {
        Unknown = 0,
        Heal = 1,
        Teleport = 2,
        Stream = 3,
        Throw = 4,
        Nova = 5, //radius=pos1.x
        Poison = 6,
        Line = 7,
        Burst = 8, //radius=dist(pos1,pos2)
        Flow = 9,
        Ring = 10, //radius=pos1.x
        Lightning = 11, //particleSize=pos2.x
        Collapse = 12, //radius=dist(pos1,pos2)
        ConeBlast = 13, //origin=pos1, radius = pos2.x
        Earthquake = 14,
        Flash = 15, //period=pos1.x, numCycles=pos1.y
        BeachBall = 16,
        ElectricBolts = 17, //If a pet paralyzes a monster
        ElectricFlashing = 18, //If a monster got paralyzed from a electric pet
        RisingFury = 19 //If a pet is standing still (this white particles)
    }

    public enum Bags : short
    {
        Normal = 0x500,
        Purple = 0x503,
        Pink = 0x506,
        Cyan = 0x509,
        Red = 0x510,
        Blue = 0x050B,
        Purple2 = 0x507,
        Egg = 0x508,
        White = 0x050C,
        White2 = 0x050E,
        White3 = 0x50F
    }
    public enum Ability : uint
    {
        AttackClose = 402,
        AttackMid = 404,
        AttackFar = 405,
        Electric = 406,
        Heal = 407,
        MagicHeal = 408,
        Savage = 409,
        Decoy = 410,
        RisingFury = 411,
    }

    public enum Classes : short
    {
        Rogue = 0x0300,
        Archer = 0x0307,
        Wizard = 0x030e,
        Priest = 0x0310,
        Warrior = 0x031d,
        Knight = 0x031e,
        Paladin = 0x031f,
        Assassin = 0x0320,
        Necromancer = 0x0321,
        Huntress = 0x0322,
        Mystic = 0x0323,
        Trickster = 0x0324,
        Sorcerer = 0x0325,
        Ninja = 0x0326
    }

    public enum StatType : byte
    {
        MaximumHP = 0,
        HP = 1,
        Size = 2,
        MaximumMP = 3,
        MP = 4,
        NextLevelExperience = 5,
        Experience = 6,
        Level = 7,
        Inventory0 = 8,
        Inventory1 = 9,
        Inventory2 = 10,
        Inventory3 = 11,
        Inventory4 = 12,
        Inventory5 = 13,
        Inventory6 = 14,
        Inventory7 = 15,
        Inventory8 = 16,
        Inventory9 = 17,
        Inventory10 = 18,
        Inventory11 = 19,
        Attack = 20,
        Defense = 21,
        Speed = 22,
        Vitality = 26,
        Wisdom = 27,
        Dexterity = 28,
        Effects = 29,
        Stars = 30,
        Name = 31, //Is UTF
        Texture1 = 32,
        Texture2 = 33,
        MerchandiseType = 34,
        Credits = 35,
        MerchandisePrice = 36,
        PortalUsable = 37, // "ACTIVE_STAT"
        AccountId = 38, //Is UTF
        AccountFame = 39,
        MerchandiseCurrency = 40,
        ObjectConnection = 41,
        MerchandiseRemainingCount = 42,
        MerchandiseRemainingMinutes = 43,
        MerchandiseDiscount = 44,
        MerchandiseRankRequirement = 45,
        HealthBonus = 46,
        ManaBonus = 47,
        AttackBonus = 48,
        DefenseBonus = 49,
        SpeedBonus = 50,
        VitalityBonus = 51,
        WisdomBonus = 52,
        DexterityBonus = 53,
        OwnerAccountId = 54, //Is UTF
        RankRequired = 55,
        NameChosen = 56,
        CharacterFame = 57,
        CharacterFameGoal = 58,
        Glowing = 59,
        SinkLevel = 60,
        AltTextureIndex = 61,
        GuildName = 62, //Is UTF
        GuildRank = 63,
        OxygenBar = 64,
        XpBoosterActive = 65,
        XpBoostTime = 66,
        LootDropBoostTime = 67,
        LootTierBoostTime = 68,
        HealthPotionCount = 69,
        MagicPotionCount = 70,
        Backpack0 = 71,
        Backpack1 = 72,
        Backpack2 = 73,
        Backpack3 = 74,
        Backpack4 = 75,
        Backpack5 = 76,
        Backpack6 = 77,
        Backpack7 = 78,
        HasBackpack = 79,
        Skin = 80,
        PetInstanceId = 81,
        PetName = 82, //Is UTF
        PetType = 83,
        PetRarity = 84,
        PetMaximumLevel = 85,
        PetFamily = 86, //This does do nothing in the client
        PetPoints0 = 87,
        PetPoints1 = 88,
        PetPoints2 = 89,
        PetLevel0 = 90,
        PetLevel1 = 91,
        PetLevel2 = 92,
        PetAbilityType0 = 93,
        PetAbilityType1 = 94,
        PetAbilityType2 = 95,
        Effects2 = 96, // Used for things like Curse, Petrify etc...
        FortuneTokens = 97,
    }
}
