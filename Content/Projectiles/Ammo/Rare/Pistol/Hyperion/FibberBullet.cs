using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion
{
    public class FibberBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            // Turn off immunity frames so that every projectile can hit
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;

            Projectile.ai[0] = 0f;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnKill(int timeLeft)
        {
            int numDust = 1;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Stone).noGravity = true;
            }
        }
        
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;

            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
                return false;
            }

            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }

            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            // --- CLONING LOGIC ---
            if (Projectile.ai[0] == 1f) // Check if this is the designated "parent" bullet
            {
                if (Projectile.ai[0] == 1f && Projectile.ai[1] == 1f) // If it's a cloner parent AND eligible for first split
                {
                    const int numberOfClones = 10;
                    const float totalSpreadDegrees = 5;
                    float baseAngle = Projectile.velocity.ToRotation();
                    float angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / (numberOfClones - 1));
                    
                    // Adjust the base angle so the spread is centered around the original velocity.
                    baseAngle -= MathHelper.ToRadians(totalSpreadDegrees) / 2f;

                    for (int i = 0; i < numberOfClones; i++)
                    {
                        // Calculate the new angle for this specific clone
                        float newAngle = baseAngle + (i * angleIncrement);

                        // Create the new velocity vector using the calculated angle and the parent's speed.
                        Vector2 newVelocity = newAngle.ToRotationVector2() * Projectile.velocity.Length();

                        // Spawn the new projectile (clone)
                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            newVelocity,
                            Projectile.type,
                            20,
                            Projectile.knockBack,
                            Projectile.owner,
                            0f,
                            0f
                        );
                    }

                    Projectile.ai[0] = 0f;
                    Projectile.ai[1] = 0f;
                }
            }

            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);

            SoundEngine.PlaySound(SoundID.Item50, Projectile.position);

            return false;
        }
    }
}