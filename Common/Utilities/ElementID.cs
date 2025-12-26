using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Content.Projectiles.Elements;
using Vaultaria.Content.Projectiles.Shields;

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
        public static short ShockExplosion => (short)ModContent.ProjectileType<ShockExplosion>();
        public static readonly int ShockBuff = ModContent.BuffType<ShockBuff>();

        // Slag Element
        public static readonly int SlagPrefix = ModContent.PrefixType<Slag>();
        public static readonly short SlagProjectile = ProjectileID.ShadowFlame;
        public static short SlagExplosion => (short)ModContent.ProjectileType<SlagExplosion>();
        public static readonly int SlagBuff = ModContent.BuffType<SlagBuff>();

        // Corrosive Element
        public static readonly int CorrosivePrefix = ModContent.PrefixType<Corrosive>();
        public static readonly short CorrosiveProjectile = ProjectileID.SporeGas3;
        public static short CorrosiveExplosion => (short)ModContent.ProjectileType<CorrosiveExplosion>();
        public static readonly int CorrosiveBuff = ModContent.BuffType<CorrosiveBuff>();

        // Incendiary Element
        public static readonly int IncendiaryPrefix = ModContent.PrefixType<Incendiary>();
        public static readonly short IncendiaryProjectile = ProjectileID.MolotovFire;
        public static short IncendiaryNovaProjectile => (short)ModContent.ProjectileType<IncendiaryNova>();
        public static short IncendiaryExplosion => (short)ModContent.ProjectileType<IncendiaryExplosion>();
        public static readonly int IncendiaryBuff = ModContent.BuffType<IncendiaryBuff>();

        // Explosive Element
        public static readonly int ExplosivePrefix = ModContent.PrefixType<Explosive>();
        public static short RoundExplosiveProjectile => (short)ModContent.ProjectileType<ExplosiveProjectile>(); // This is a Property. It runs the code EVERY time you call it. Rather than calling something thats already loaded (vanilla projectiles)
        public static readonly short SmallExplosiveProjectile = ProjectileID.DD2ExplosiveTrapT1Explosion;
        public static readonly short ExplosiveProjectile = ProjectileID.DD2ExplosiveTrapT2Explosion;
        public static readonly short LargeExplosiveProjectile = ProjectileID.DD2ExplosiveTrapT3Explosion;
        public static readonly int ExplosiveBuff = ModContent.BuffType<ExplosiveBuff>();

        // Cryo Element
        public static readonly int CryoPrefix = ModContent.PrefixType<Cryo>();
        public static readonly short CryoProjectile = ProjectileID.FrostBlastFriendly;
        public static short CryoExplosion => (short)ModContent.ProjectileType<CryoExplosion>();
        public static readonly int CryoBuff = ModContent.BuffType<CryoBuff>();

        // Radiation Element
        public static readonly int RadiationPrefix = ModContent.PrefixType<Radiation>();
        public static readonly short RadiationProjectile = ProjectileID.GoldenShowerFriendly;
        public static short RadiationExplosion => (short)ModContent.ProjectileType<RadiationExplosion>();
        public static readonly int RadiationBuff = ModContent.BuffType<RadiationBuff>();
    }
}