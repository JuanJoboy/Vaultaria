using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;

namespace Vaultaria.Content.Projectiles.Melee
{
    public class BuzzAxeBombardier : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);
            Projectile.scale = 1.6f;

            // Damage
            Projectile.damage = 36;
            Projectile.CritChance = 10;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();

            int frameSpeed = 12;
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

            Projectile.velocity.Y += 0.06f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // On hitting a tile, the projectile should typically explode (like a grenade)
            Projectile.Kill(); // Immediately kill the projectile to trigger OnKill
            return false; // Prevent default tile collision behavior (like bouncing)
        }

        public override void OnKill(int timeLeft)
        {
            // Create an explosion effect when the projectile dies
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position); // Explosion sound

            // Create dust particles for the explosion
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3f);
            }

            // Create a damaging explosion (similar to a grenade's explosion)
            // This spawns an invisible projectile that immediately deals explosion damage in an area.
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                Vector2.Zero, // No velocity for the explosion projectile itself
                ProjectileID.Volcano, // Use a vanilla explosion projectile type
                Projectile.damage, // Use the damage of this projectile for the explosion
                Projectile.knockBack,
                Projectile.owner
            );
        }
    }
}