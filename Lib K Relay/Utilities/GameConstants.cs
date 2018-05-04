using Lib_K_Relay.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_K_Relay.Utilities
{
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
        Darkness = 1 << 30
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
        SlowedImmune = 31,
        DazedImmune = 32,
        ParalyzedImmune = 33,
        Petrified = 34,
        PetrifiedImmune = 35,
        PetEffectIcon = 36,
        Curse = 37,
        CurseImmune = 38,
        HPBoost = 39,
        MPBoost = 40,
        AttBoost = 41,
        DefBoost = 42,
        SpdBoost = 43,
        VitBoost = 44,
        WisBoost = 45,
        DexBoost = 46,
        Silenced = 47,
        GroundDamage = 98,
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
        Jitter = 14,
        Flash = 15, //period=pos1.x, numCycles=pos1.y
        ThrowProjectile = 16,
        Shocker = 17, //If a pet paralyzes a monster
        Shockee = 18, //If a monster got paralyzed from a electric pet
        RisingFury = 19, //If a pet is standing still (this white particles)
        NovaNoAOE = 20
    }

    public struct ARGB
    {
        public byte A;
        public byte B;
        public byte G;
        public byte R;

        public ARGB(uint argb)
        {
            A = (byte)((argb & 0xff000000) >> 24);
            R = (byte)((argb & 0x00ff0000) >> 16);
            G = (byte)((argb & 0x0000ff00) >> 8);
            B = (byte)((argb & 0x000000ff) >> 0);
        }

        public ARGB(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static ARGB Read(PacketReader r)
        {
            ARGB ret = new ARGB();
            ret.A = r.ReadByte();
            ret.R = r.ReadByte();
            ret.G = r.ReadByte();
            ret.B = r.ReadByte();
            return ret;
        }

        public void Write(PacketWriter w)
        {
            w.Write(A);
            w.Write(R);
            w.Write(G);
            w.Write(B);
        }
    }

    public enum Bags : short
    {
        Brown = 0x500,
        BrownBoosted = 0x6ad,
        Pink = 0x506,
        PinkBoosted = 0x6ae,
        Purple = 0x507,
        PurpleBoosted = 0x6ba,
        Egg = 0x508,
        EggBoosted = 0x6bb,
        Gold = 0x050E,
        GoldBoosted = 0x6bc,
        Cyan = 0x509,
        CyanBoosted = 0x6bd,
        Blue = 0x050B,
        BlueBoosted = 0x6be,
        Orange = 0x50F,
        OrangeBoosted = 0x6bf,
        Red = 0x6AC,
        RedBoosted = 0x6c0,
        White = 0x050C,
        WhiteBoosted = 0x0510,
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
}
