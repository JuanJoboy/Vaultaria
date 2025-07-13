using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Content.Projectiles.Grenades.Legendary
{
    public class Breath : ModProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);
            Projectile.scale = 1.6f;

            // Damage
            Projectile.damage = 1;
            Projectile.CritChance = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();

            int frameSpeed = 3;
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

            Projectile.velocity.Y += 0.04f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Shoot();
            Projectile.velocity.X *= 0.0f;
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Shoot();
            target.AddBuff(ModContent.BuffType<IncendiaryBuff>(), 300);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Shoot();
            target.AddBuff(ModContent.BuffType<IncendiaryBuff>(), 300);
        }

        public override void OnKill(int timeLeft)
        {
            Shoot();

            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3f);
            }
        }

        private void Shoot()
        {
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                Projectile.velocity,
                ProjectileID.ApprenticeStaffT3Shot,
                Projectile.damage,
                Projectile.knockBack
            );
        }
    }
}