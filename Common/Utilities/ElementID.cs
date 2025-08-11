using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Common.Utilities
{
    public static class ElementalID
    {
        // ***************************************************
        // *------------- ID's For Projectiles -------------*
        // ***************************************************

        // Shock Element
        public static readonly int ShockPrefix = ModContent.PrefixType<Shock>();
        public static readonly short ShockProjectile = ProjectileID.Electrosphere;
        public static readonly int ShockBuff = ModContent.BuffType<ShockBuff>();

        // Slag Element
        public static readonly int SlagPrefix = ModContent.PrefixType<Slag>();
        public static readonly short SlagProjectile = ProjectileID.ShadowFlame;
        public static readonly int SlagBuff = ModContent.BuffType<SlagBuff>();

        // Corrosive Element
        public static readonly int CorrosivePrefix = ModContent.PrefixType<Corrosive>();
        public static readonly short CorrosiveProjectile = ProjectileID.SporeGas3;
        public static readonly int CorrosiveBuff = ModContent.BuffType<CorrosiveBuff>();

        // Incendiary Element
        public static readonly int IncendiaryPrefix = ModContent.PrefixType<Incendiary>();
        public static readonly short IncendiaryProjectile = ProjectileID.SolarWhipSwordExplosion;
        public static readonly int IncendiaryBuff = ModContent.BuffType<IncendiaryBuff>();

        // Explosive Element
        public static readonly int ExplosivePrefix = ModContent.PrefixType<Explosive>();
        public static readonly short ExplosiveProjectile = ProjectileID.DD2ExplosiveTrapT2Explosion;
        public static readonly int ExplosiveBuff = ModContent.BuffType<ExplosiveBuff>();

        // Cryo Element
        public static readonly int CryoPrefix = ModContent.PrefixType<Cryo>();
        public static readonly short CryoProjectile = ProjectileID.FrostBlastFriendly;
        public static readonly int CryoBuff = ModContent.BuffType<CryoBuff>();
    }
}