using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using Terraria.DataStructures;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Tediore;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Shotgun.Tediore
{
    public class HomingDeliverance : ElementalProjectile
    {
        public float explosiveMultiplier = 1f;
        private short explosiveProjectile = ElementalID.RoundExplosiveProjectile;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(70, 30);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.damage = 0;

            // Bullet Config
            Projectile.timeLeft = 360;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override bool PreAI()
        {
            if(Deliverance.thrown == true)
            {
                ThrowGun();
            }
            return base.PreAI();
        }

        public override void AI()
        {
            NPC target = FindTarget();

            if(target != null)
            {
                float distance = Vector2.Distance(Projectile.Center, target.Center);
                if (distance > 60)
                {
                    Utilities.MoveToPosition(Projectile, target.Center - new Vector2(60, 60), 5f, 0.2f);
                }

                Projectile.rotation = Projectile.velocity.ToRotation();
                
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.rotation += MathHelper.Pi;
                }
            }

            if (EnemyFoundToShoot(target))
            {
                // Calculates a normalized direction to the target and scales it to a bullet speed of 8
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= 8f;

                if(Projectile.owner == Main.myPlayer)
                {
                    CloneShots(Projectile.GetSource_FromThis(), Projectile.Center, direction, ProjectileID.ChlorophyteBullet, Projectile.damage, 2f, 8, 5f);
                }

                Projectile.ai[0] = 0f;
            }

            Projectile.netUpdate = true;
        }

        private void CloneShots(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int numberOfAdditionalBullets, float degreeSpread)
        {
            float spreadAngle = MathHelper.ToRadians(degreeSpread);

            for (int i = 0; i < numberOfAdditionalBullets; i++)
            {
                float bulletAngle = MathHelper.Lerp(-spreadAngle / 2, spreadAngle / 2, (float)i / (numberOfAdditionalBullets - 1));

                Vector2 bulletVelocity = bulletAngle.ToRotationVector2() * velocity.Length();

                Projectile.NewProjectile(source, position, bulletVelocity, type, damage, knockback, Projectile.owner);
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

        private bool EnemyFoundToShoot(NPC target)
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 25f)
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

                    Projectile.netUpdate = true;
                    return true;
                }
            }

            return false;
        }

        private void ThrowGun()
        {
            Vector2 mouse = Main.MouseWorld;
            Player player = Main.player[Main.myPlayer];

            // Face target
            if (mouse.X > player.Center.X)
            {
                Projectile.spriteDirection = 1; // Face right
            }
            else
            {
                Projectile.spriteDirection = -1; // Face left
            }

            Projectile.netUpdate = true;
        }

        public override void OnKill(int timeLeft)
        {
            // Create an explosion effect when the projectile dies
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            // Create dust particles for the explosion
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.damage = 250;
            
            Player player = Main.player[Projectile.owner];
            SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);

            return base.OnTileCollide(oldVelocity);
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive"
            };
        }
    }
}