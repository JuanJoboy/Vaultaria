using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Hyperion
{
    public class HeartBreakerBullet : ElementalProjectile
    {
        public float incendiaryMultiplier = 0.5f;
        private float elementalChance = 5;
        private short incendiaryProjectile = ElementalID.IncendiaryProjectile;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int buffTime = 0;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 2);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            base.AI();
            Utilities.FrameRotator(3, Projectile); 
        }

        public override void OnKill(int timeLeft)
        {
            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Torch).noGravity = false;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Utilities.HealOnNPCHit(target, damageDone, 0.5f, Projectile);

            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Utilities.HealOnPlayerHit(target, info.SourceDamage, 0.5f, Projectile);

            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Incendiary"
            };
        }
    }
}