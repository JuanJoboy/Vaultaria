using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Projectiles.Shields;
using Terraria.DataStructures;
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Projectiles.Summoner.Sentry
{
    public class RolandTurret : ElementalProjectile
    {
        private bool touchedTheGround = false;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.Size = new Vector2(51, 60);
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.sentry = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.damage = 75;

            // Sprite
            Projectile.spriteDirection = 1;
        }

        public override void AI()
        {
            base.AI();

            // Gets the player and ensures that only one turret can spawn
            Player player = Main.player[Projectile.owner];

            player.UpdateMaxTurrets();
            int pos = Main.rand.Next(-100, 100);

            Projectile.velocity = Vector2.Zero; // Stay stationary

            if (touchedTheGround == false)
            {
                Projectile.velocity.Y += 15; // Fall to the ground
                Projectile.netUpdate = true;
            }

            NPC target = FindTarget();
            int summonDamage = (int)(Projectile.damage * player.GetDamage(DamageClass.Summon).Multiplicative);

            if(player.whoAmI == Main.myPlayer)
            {
                if (EnemyFoundToShoot(target, 0, 10f))
                {
                    // Calculates a normalized direction to the target and scales it to a bullet speed of 8
                    Vector2 direction = target.Center - Projectile.Center;
                    direction.Normalize();
                    direction *= 8f;

                    Projectile proj = Projectile.NewProjectileDirect(
                        Projectile.GetSource_FromThis(),
                        Projectile.Center,
                        direction,
                        ProjectileID.SilverBullet,
                        summonDamage,
                        2f,
                        Projectile.owner
                    );

                    proj.DamageType = DamageClass.Summon;

                    // Reset fire timer
                    Projectile.ai[0] = 0f;
                }

                if (EnemyFoundToShoot(target, 1, 300f))
                {
                    for(int i = 0; i < 50; i++)
                    {
                        if(Main.netMode == NetmodeID.SinglePlayer)
                        {
                            Item.NewItem(
                                Projectile.GetSource_FromThis(),
                                Projectile.Center + new Vector2(pos, 0),
                                GetAmmo(player)
                            );
                        }
                        else
                        {
                            player.QuickSpawnItem(
                                Projectile.GetSource_FromThis(),
                                GetAmmo(player),
                                1
                            );
                        }
                    }

                    Projectile.ai[1] = 0f;
                }

                if (EnemyFoundToShoot(target, 2, 600f))
                {
                    if(Main.netMode == NetmodeID.SinglePlayer)
                    {
                        Item.NewItem(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center + new Vector2(pos, 0),
                            ItemID.Heart
                        );
                    }
                    else
                    {
                        player.QuickSpawnItem(
                            Projectile.GetSource_FromThis(),
                            ItemID.Heart,
                            1
                        );
                    }

                    Projectile.ai[2] = 0f;
                }
            }
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity.Y = 0f; // Stop falling
            Projectile.position.Y += 16f; // Lower it by an additional 16 pixels since it stays in the air for some reason
            touchedTheGround = true;
            Projectile.netUpdate = true;
            return false; // False will allow it to not despawn on tile collide since its a projectile
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

        private bool EnemyFoundToShoot(NPC target, int index, float ticks)
        {
            // Fire timer stored in ai[0]
            Projectile.ai[index]++;
            if (Projectile.ai[index] >= ticks) // Fire every 6 ticks (~0.1 sec)
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

        private int GetAmmo(Player player)
        {
            switch (Main.rand.Next(0, 8))
            {
                case 1:
                    return ModContent.ItemType<PistolAmmo>();
                case 2:
                    return ModContent.ItemType<SubmachineGunAmmo>();
                case 3:
                    return ModContent.ItemType<AssaultRifleAmmo>();
                case 4:
                    return ModContent.ItemType<ShotgunAmmo>();
                case 5:
                    return ModContent.ItemType<SniperAmmo>();
                case 6:
                    return ModContent.ItemType<LauncherAmmo>();
                case 7:
                    Item fakeGun = new Item();
                    fakeGun.SetDefaults(ItemID.SDMG);
                    fakeGun.useAmmo = AmmoID.Bullet;  // force bullet-only
                    fakeGun.shoot = ProjectileID.Bullet; // enforce projectile type
                    fakeGun.notAmmo = true; // prevents lava buckets or other items being used
                    int ammo = player.ChooseAmmo(fakeGun).type;
                    return ammo;
                default:
                    return ItemID.SilverBullet;
            }
        }

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(177, 178, 172);
        }
    }
}