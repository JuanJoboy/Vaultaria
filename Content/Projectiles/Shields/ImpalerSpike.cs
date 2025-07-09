using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Content.Projectiles.Shields
{
    public class ImpalerSpike : ModProjectile
    {
        public override void SetDefaults()
        {
            // Clone Chlorophytes homing
            Projectile.CloneDefaults(ProjectileID.ChlorophyteBullet);

            // Size
            Projectile.Size = new Vector2(20, 20);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            AIType = ProjectileID.ChlorophyteBullet; // Inherit Chlorophyte AI
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void PostAI()
        {
            base.PostAI();
            Projectile.rotation = Projectile.velocity.ToRotation();

            // This will cycle through all of the frames in the sprite sheet
            int frameSpeed = 4; // How fast you want it to animate (lower = faster)
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PureSpray).noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<CorrosiveBuff>(), 300);
        }
    }
}