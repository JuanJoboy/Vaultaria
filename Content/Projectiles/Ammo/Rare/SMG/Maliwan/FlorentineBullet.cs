using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic; // For Lists

namespace Vaultaria.Content.Projectiles.Ammo.Rare.SMG.Maliwan
{
    public class FlorentineBullet : ElementalProjectile
    {
        public float shockMultiplier;
        public float slagMultiplier;
        public int buffTime = 180;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 20);

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
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            base.AI();
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
            if (SetElementalChance(20f))
            {
                SetShockOnNPC(target, hit, shockMultiplier, buffTime);
                SetSlagOnNPC(target, hit, slagMultiplier, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(20f))
            {
                SetShockOnPlayer(target, info, shockMultiplier, buffTime);
                SetSlagOnPlayer(target, info, slagMultiplier, buffTime);
            }
        }

        public override List<string> getElement()
        {
            return new List<string>
            {
                "Shock",
                "Slag"
            };
        }
    }
}