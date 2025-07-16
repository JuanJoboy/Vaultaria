using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Projectiles.Shields;
using Terraria.DataStructures;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Summoner.Sentry;

namespace Vaultaria.Content.Projectiles.Minions
{
    public class Turret : ElementalProjectile
    {
        private float yPosition;
        private bool touchedTheGround = false;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.sentry = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            // Sprite
            Projectile.spriteDirection = 1;
        }

        // Nuke
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.NewProjectile(
                Projectile.GetSource_FromThis(),
                Projectile.Center,
                Vector2.Zero,
                ProjectileID.DD2ExplosiveTrapT3Explosion,
                100,
                2f,
                Projectile.owner
            );
        }

        public override void AI()
        {
            // Gets the owner and ensures that only one turret can spawn
            Player owner = Main.player[Projectile.owner];
            owner.maxTurrets = 2;
            owner.UpdateMaxTurrets();

            Projectile.velocity = Vector2.Zero; // Stay stationary

            if (touchedTheGround == false)
            {
                float y = Projectile.velocity.Y += 15; // Fall to the ground
            }

            NPC target = FindTarget();

            if (EnemyFoundToShoot(target))
            {
                // Calculates a normalized direction to the target and scales it to a bullet speed of 8
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= 8f;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    direction,
                    ProjectileID.ChlorophyteBullet,
                    40,
                    2f,
                    Projectile.owner
                );

                // Reset fire timer
                Projectile.ai[0] = 0f;
            }

            if (EnemyFoundToExplode(target))
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= 8f;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    direction,
                    ProjectileID.ClusterRocketI,
                    100,
                    0f,
                    Projectile.owner
                );

                Projectile.ai[1] = 0f;
            }

            if (EnemyFoundToSlag(target))
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= 8f;

                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    direction,
                    ModContent.ProjectileType<HomingSlagBall>(),
                    1,
                    2f,
                    Projectile.owner
                );

                Projectile.ai[2] = 0f;
            }
        }

        private NPC FindTarget()
        {
            float range = 500f; // 500 pixels
            NPC closest = null;

            // Loops through every NPC in the world
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this)) // Filters to only hostile and valid targets
                {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center); // Measures the distance from turret to NPC
                    if (dist < range && Collision.CanHitLine(Projectile.Center, 1, 1, npc.Center, 1, 1)) // Checks if the NPC is closer than any previously checked NPC and if there's a clear line of sight
                    {
                        closest = npc;
                        range = dist;
                    }
                }
            }

            return closest; // Returns the best valid NPC target, or null if none found
        }

        public override bool? CanDamage()
        {
            return false;
        }

        private bool EnemyFoundToShoot(NPC target)
        {
            // Fire timer stored in ai[0]
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 6f) // Fire every 6 ticks (~0.1 sec)
            {
                if (target != null)
                {
                    // Face target
                    if (target.Center.X > Projectile.Center.X)
                    {
                        Projectile.spriteDirection = 1; // Face right
                    }
                    else
                    {
                        Projectile.spriteDirection = -1; // Face left
                    }

                    return true;
                }
            }

            return false;
        }

        private bool EnemyFoundToExplode(NPC target)
        {
            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 120f)
            {
                if (target != null)
                {
                    if (target.Center.X > Projectile.Center.X)
                    {
                        Projectile.spriteDirection = 1;
                    }
                    else
                    {
                        Projectile.spriteDirection = -1;
                    }

                    return true;
                }
            }

            return false;
        }

        private bool EnemyFoundToSlag(NPC target)
        {
            Projectile.ai[2]++;
            if (Projectile.ai[2] >= 60f)
            {
                if (target != null)
                {
                    if (target.Center.X > Projectile.Center.X)
                    {
                        Projectile.spriteDirection = 1;
                    }
                    else
                    {
                        Projectile.spriteDirection = -1;
                    }

                    return true;
                }
            }

            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity.Y = 0f; // Stop falling
            Projectile.position.Y -= 16f; // Raise it by 16 pixels
            touchedTheGround = true;
            return false; // False will allow it to not despawn on tile collide since its a projectile
        }
    }
}
