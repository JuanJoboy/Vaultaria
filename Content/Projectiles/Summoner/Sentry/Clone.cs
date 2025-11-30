using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Projectiles.Shields;
using Terraria.DataStructures;
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using System.Net.Mail;
using Vaultaria.Content.Items.Weapons.Summoner.Sentry;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Maliwan;
using Vaultaria.Content.Items.Weapons.Melee;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Legendary;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Tediore;

namespace Vaultaria.Content.Projectiles.Summoner.Sentry
{
    public class Clone : ElementalProjectile
    {
        private bool touchedTheGround = false;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(34, 60);
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.sentry = true;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.damage = 10;
            Projectile.CritChance = 4;

            // Sprite
            Projectile.spriteDirection = 1;
        }

        public override void AI()
        {
            // Gets the owner and ensures that only one turret can spawn
            Player owner = Main.player[Projectile.owner];
            owner.UpdateMaxTurrets();
            Item item = owner.HeldItem;
            ModItem modItem = item.ModItem;
            int projectile = ProjectileID.Bullet;
            int damage = 0;
            float knockback = 0;

            Projectile.velocity = Vector2.Zero; // Stay stationary

            if (touchedTheGround == false)
            {
                Projectile.velocity.Y += 15; // Fall to the ground
            }

            if((item.DamageType == DamageClass.Ranged || item.DamageType == DamageClass.Magic) && item.type != ModContent.ItemType<DigiClone>() && item.type != ModContent.ItemType<BuzzAxe>() && item.type != ModContent.ItemType<BreathOfTerramorphous>())
            {
                if(item.ammo != AmmoID.Arrow && item.ammo != AmmoID.Bullet && item.ammo != AmmoID.CandyCorn && item.ammo != AmmoID.Coin && item.ammo != AmmoID.Dart && item.ammo != AmmoID.FallenStar && item.ammo != AmmoID.Flare && item.ammo != AmmoID.Gel && item.ammo != AmmoID.JackOLantern && item.ammo != AmmoID.NailFriendly && item.ammo != AmmoID.None && item.ammo != AmmoID.Rocket && item.ammo != AmmoID.Sand && item.ammo != AmmoID.Snowball && item.ammo != AmmoID.Solution && item.ammo != AmmoID.Stake && item.ammo != AmmoID.StyngerBolt || item.mana > 1)
                {
                    projectile = item.shoot;
                }
                else
                {
                    // Gets the ammo in the players inventory
                    owner.PickAmmo(item, out projectile, out float speed, out damage, out knockback, out int usedAmmo, true);
                }
            }

            if(item.shoot <= ProjectileID.None)
            {
                projectile = ProjectileID.Bullet;
            }

            NPC target = FindTarget();

            if(Projectile.ai[1] > 0)
            {
                Projectile.ai[1]--;
            }

            if (Projectile.ai[1] <= 0 && EnemyFoundToShoot(target, 0, item.useAnimation))
            {
                // Calculates a normalized direction to the target and scales it to a bullet speed of 8
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= item.shootSpeed;

                if(modItem != null)
                {
                    modItem.Shoot(owner, (EntitySource_ItemUse_WithAmmo)owner.GetSource_ItemUse_WithPotentialAmmo(item, item.ammo), Projectile.Center, direction, projectile, item.damage + damage, item.knockBack + damage);
                }

                Projectile.NewProjectileDirect(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    direction,
                    projectile,
                    item.damage + damage,
                    item.knockBack + knockback,
                    Projectile.owner
                );

                // Reset fire timer
                Projectile.ai[0] = 0f;
                Projectile.ai[1] = item.reuseDelay;
            }
        }

        public override void PostDraw(Color lightColor)
        {
            base.PostDraw(lightColor);

            Player owner = Main.player[Projectile.owner];
            Item item = owner.HeldItem;
            Vector2 offset = new Vector2(10, 0);

			SpriteEffects effects = Projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Texture2D? texture = TextureAssets.Item[ItemID.None].Value;

            if(Projectile.spriteDirection == -1)
            {
                offset = new Vector2(-10, 0);
            }

            if(item.DamageType == DamageClass.Magic || item.DamageType == DamageClass.Ranged)
            {
                // Check if the item is a ModItem
                if (item.ModItem != null)
                {
                    // Use ModContent.Request<Texture2D> for ModItems via their internal texture path.
                    // This is the correct, safe way to load a Mod Content texture.
                    texture = ModContent.Request<Texture2D>(item.ModItem.Texture).Value;
                }
                else
                {
                    // For vanilla items, use TextureAssets.Item indexed by the global item ID.
                    // The .Value property accesses the loaded Texture2D asset.
                    texture = TextureAssets.Item[item.type].Value;
                }
            }
            
            int frameHeight = texture.Height / Main.projFrames[Projectile.type];
            int spriteSheetOffset = frameHeight * Projectile.frame;
            Vector2 sheetInsertPosition = (Projectile.Center + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition).Floor();
            
            Color drawColor = new Color(88, 160, 151);
            Main.EntitySpriteDraw(texture, sheetInsertPosition + offset, new Rectangle?(new Rectangle(0, spriteSheetOffset, texture.Width, frameHeight)), drawColor, Projectile.rotation, new Vector2(texture.Width / 2f, frameHeight / 2f), Projectile.scale / 1.5f, effects, 0f);    
		}

        private NPC FindTarget()
        {
            float range = 1500; // 500 pixels
            NPC closest = null;

            // Loops through every NPC in the world
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this)) // Filters to only hostile and valid targets
                {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center); // Measures the distance from turret to NPC
                    if (dist < range) // Checks if the NPC is closer than any previously checked NPC and if there's a clear line of sight
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

                    return true;
                }
            }

            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity.Y = 0f; // Stop falling
            Projectile.position.Y += 10f; // Lower it by an additional 16 pixels since it stays in the air for some reason
            touchedTheGround = true;
            return false; // False will allow it to not despawn on tile collide since its a projectile
        }
    }
}