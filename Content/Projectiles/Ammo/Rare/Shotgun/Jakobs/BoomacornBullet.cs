using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Jakobs
{
    public class BoomacornBullet : ElementalProjectile
    {
        private float explosiveMultiplier = 1f;
        private float shockMultiplier = 0.5f;
        private float incendiaryMultiplier = 0.5f;
        private float corrosiveMultiplier = 0.5f;
        private float slagMultiplier = 0.5f;
        private float cryoMultiplier = 0.5f;
        private float radiationMultiplier = 0.5f;
        private float explosiveChance = 100f;
        private float shockChance = 20f;
        private float incendiaryChance = 20f;
        private float corrosiveChance = 20f;
        private float slagChance = 20f;
        private float cryoChance = 20f;
        private float radiationChance = 20f;
        private short explosiveProjectile = ElementalID.ExplosiveProjectile;
        private short shockProjectile = ElementalID.ShockProjectile;
        private short incendiaryProjectile = ElementalID.IncendiaryProjectile;
        private short corrosiveProjectile = ElementalID.CorrosiveProjectile;
        private short slagProjectile = ElementalID.SlagProjectile;
        private short cryoProjectile = ElementalID.CryoProjectile;
        private short radiationProjectile = ElementalID.RadiationProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int shockBuff = ElementalID.ShockBuff;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int corrosiveBuff = ElementalID.CorrosiveBuff;
        private int slagBuff = ElementalID.SlagBuff;
        private int cryoBuff = ElementalID.CryoBuff;
        private int radiationBuff = ElementalID.RadiationBuff;
        private int buffTime = 60;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(17, 8);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
        }
        
        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(explosiveChance))
            {
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }

            if (SetElementalChance(shockChance))
            {
                SetElementOnNPC(target, hit, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
            }

            if (SetElementalChance(incendiaryChance))
            {
                SetElementOnNPC(target, hit, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }

            if (SetElementalChance(corrosiveChance))
            {
                SetElementOnNPC(target, hit, corrosiveMultiplier, player, corrosiveProjectile, corrosiveBuff, buffTime);
            }

            if (SetElementalChance(slagChance))
            {
                SetElementOnNPC(target, hit, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }

            if (SetElementalChance(cryoChance))
            {
                SetElementOnNPC(target, hit, cryoMultiplier, player, cryoProjectile, cryoBuff, buffTime);
            }

            if (SetElementalChance(radiationChance))
            {
                SetElementOnNPC(target, hit, radiationMultiplier, player, radiationProjectile, radiationBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(explosiveChance))
            {
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }

            if (SetElementalChance(shockChance))
            {
                SetElementOnPlayer(target, info, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
            }

            if (SetElementalChance(incendiaryChance))
            {
                SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }

            if (SetElementalChance(corrosiveChance))
            {
                SetElementOnPlayer(target, info, corrosiveMultiplier, player, corrosiveProjectile, corrosiveBuff, buffTime);
            }

            if (SetElementalChance(slagChance))
            {
                SetElementOnPlayer(target, info, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }

            if (SetElementalChance(cryoChance))
            {
                SetElementOnPlayer(target, info, cryoMultiplier, player, cryoProjectile, cryoBuff, buffTime);
            }

            if (SetElementalChance(radiationChance))
            {
                SetElementOnPlayer(target, info, radiationMultiplier, player, radiationProjectile, radiationBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(explosiveChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);
            }

            return false;
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive",
                "Shock",
                "Incendiary",
                "Corrosive",
                "Slag",
                "Cryo",
                "Radiation",
            };
        }
    }
}