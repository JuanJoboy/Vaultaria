using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Effervescent.Launcher.Torgue;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Bandit;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Maliwan;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Torgue;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Hyperion;

namespace Vaultaria.Common.Utilities
{
    public static class Utilities
    {
        public static Dictionary<int, int> bulletMap = new Dictionary<int, int>
        {
            { ProjectileID.Bullet, ItemID.MusketBall },
            { ProjectileID.BulletDeadeye, ItemID.HighVelocityBullet },
            { ProjectileID.SniperBullet, ItemID.HighVelocityBullet },
            { ProjectileID.MeteorShot, ItemID.MeteorShot },
            { ProjectileID.BulletHighVelocity, ItemID.HighVelocityBullet },
            { ProjectileID.BulletSnowman, ItemID.MusketBall },
            { ProjectileID.NanoBullet, ItemID.NanoBullet },
            { ProjectileID.IchorBullet, ItemID.IchorBullet },
            { ProjectileID.PartyBullet, ItemID.PartyBullet },
            { ProjectileID.VenomBullet, ItemID.VenomBullet },
            { ProjectileID.CursedBullet, ItemID.CursedBullet },
            { ProjectileID.GoldenBullet, ItemID.GoldenBullet },
            { ProjectileID.SilverBullet, ItemID.SilverBullet },
            { ProjectileID.CrystalBullet, ItemID.CrystalBullet },
            { ProjectileID.MoonlordBullet, ItemID.MoonlordBullet },
            { ProjectileID.ExplosiveBullet, ItemID.MusketBall },
            { ProjectileID.ChlorophyteBullet, ItemID.ChlorophyteBullet }
        };

        public static Dictionary<int, int> arrowMap = new Dictionary<int, int>
        {
            { ProjectileID.WoodenArrowFriendly, ItemID.WoodenArrow },
            { ProjectileID.WoodenArrowHostile, ItemID.WoodenArrow },
            { ProjectileID.BoneArrow, ItemID.BoneArrow },
            { ProjectileID.HolyArrow, ItemID.HolyArrow },
            { ProjectileID.IchorArrow, ItemID.IchorArrow },
            { ProjectileID.VenomArrow, ItemID.VenomArrow },
            { ProjectileID.CursedArrow, ItemID.CursedArrow },
            { ProjectileID.UnholyArrow, ItemID.UnholyArrow },
            { ProjectileID.FlamingArrow, ItemID.FlamingArrow },
            { ProjectileID.JestersArrow, ItemID.JestersArrow },
            { ProjectileID.ShimmerArrow, ItemID.ShimmerArrow },
            { ProjectileID.HellfireArrow, ItemID.HellfireArrow },
            { ProjectileID.FrostburnArrow, ItemID.FrostburnArrow },
            { ProjectileID.ChlorophyteArrow, ItemID.ChlorophyteArrow },
            { ProjectileID.MoonlordArrow, ItemID.MoonlordArrow }
        };

        public static Dictionary<int, int> flareMap = new Dictionary<int, int>
        {
            { ProjectileID.Flare, ItemID.Flare },
            { ProjectileID.BlueFlare, ItemID.BlueFlare },
            { ProjectileID.SpelunkerFlare, ItemID.SpelunkerFlare },
            { ProjectileID.CursedFlare, ItemID.CursedFlare },
            { ProjectileID.RainbowFlare, ItemID.RainbowFlare },
            { ProjectileID.ShimmerFlare, ItemID.ShimmerFlare }
        };

        public static Dictionary<int, int> rocketMap = new Dictionary<int, int>
        {
            { ProjectileID.RocketSkeleton, ItemID.RocketI },
            { ProjectileID.RocketI, ItemID.RocketI },
            { ProjectileID.RocketII, ItemID.RocketII },
            { ProjectileID.RocketIII, ItemID.RocketIII },
            { ProjectileID.RocketIV, ItemID.RocketIV },
            { ProjectileID.ClusterRocketI, ItemID.ClusterRocketI },
            { ProjectileID.ClusterRocketII, ItemID.ClusterRocketII },
            { ProjectileID.DryRocket, ItemID.DryRocket },
            { ProjectileID.WetRocket, ItemID.WetRocket },
            { ProjectileID.LavaRocket, ItemID.LavaRocket },
            { ProjectileID.HoneyRocket, ItemID.HoneyRocket },
            { ProjectileID.MiniNukeRocketI, ItemID.MiniNukeI },
            { ProjectileID.MiniNukeRocketII, ItemID.MiniNukeII },
            { ModContent.ProjectileType<LoganBullet>(), ModContent.ItemType<LauncherAmmo>() },
            { ModContent.ProjectileType<NorfleetRocket>(), ModContent.ItemType<LauncherAmmo>() },
            { ModContent.ProjectileType<BadaboomRocket>(), ModContent.ItemType<LauncherAmmo>() },
            { ModContent.ProjectileType<NukemRocket>(), ModContent.ItemType<LauncherAmmo>() },
            { ModContent.ProjectileType<WorldBurnRocket>(), ModContent.ItemType<LauncherAmmo>() },
        };

        public static Dictionary<int, int> dartMap = new Dictionary<int, int>
        {
            { ProjectileID.Seed, ItemID.Seed },
            { ProjectileID.PoisonDart, ItemID.PoisonDart },
            { ProjectileID.CrystalDart, ItemID.CrystalDart },
            { ProjectileID.CursedDart, ItemID.CursedDart },
            { ProjectileID.IchorDart, ItemID.IchorDart }
        };

        /// <summary>
        /// Heals the player based on the healingPercentage.
        /// <br/> The full formula for healing is:
        /// <br/> ((damageDone * healingPercentage) / 0.075)
        /// <br/> This is because vampireHeal already multiplies the first parameter by 0.075, so this method divides it by that to make it normal. That way, if you enter in 0.65f as your healingPercentage, then your item will heal you for 65% of your damage.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damageDone"></param>
        /// <param name="healingPercentage"></param>
        /// <param name="projectile"></param>
        public static void HealOnNPCHit(NPC target, int damageDone, float healingPercentage, Projectile projectile)
        {
            int heal = (int)(damageDone * healingPercentage);
            heal = (int)(heal / 0.075f); // Divide by 0.075f to bring it back to normal
            projectile.vampireHeal(heal, projectile.Center, target);
        }

        /// <summary>
        /// Heals the player based on the healingPercentage.
        /// <br/> The full formula for healing is:
        /// <br/> ((damageDone * healingPercentage) / 0.075)
        /// <br/> This is because vampireHeal already multiplies the first parameter by 0.075, so this method divides it by that to make it normal. That way, if you enter in 0.65f as your healingPercentage, then your item will heal you for 65% of your damage.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damageDone"></param>
        /// <param name="healingPercentage"></param>
        /// <param name="projectile"></param>
        public static void HealOnPlayerHit(Player target, int damageDone, float healingPercentage, Projectile projectile)
        {
            int heal = (int)(damageDone * healingPercentage);
            heal = (int)(heal / 0.075f); // Divide by 0.075f to bring it back to normal
            projectile.vampireHeal(heal, projectile.Center, target);
        }

        /// <summary>
        /// Takes the parameters of an item's Shoot() method along with how much the new clones should spread and how many clones should appear.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="source"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="type"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="numberOfAdditionalBullets"></param>
        /// <param name="degreeSpread"></param>
        public static void CloneShots(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int numberOfAdditionalBullets, float degreeSpread)
        {
            // Define a slight spread angle for the bullets (e.g., degreeSpread = 5, 5 degrees total spread)
            float spreadAngle = MathHelper.ToRadians(degreeSpread); // Convert degrees to radians

            // Calculate the base rotation of the velocity vector
            float baseRotation = velocity.ToRotation();

            for (int i = 0; i < numberOfAdditionalBullets; i++)
            {
                // Calculate the individual bullet's angle
                // This distributes the bullets symmetrically around the original velocity direction
                float bulletAngle = baseRotation + MathHelper.Lerp(-spreadAngle / 2, spreadAngle / 2, (float)i / (numberOfAdditionalBullets - 1));

                // Calculate the new velocity vector for this bullet
                Vector2 bulletVelocity = bulletAngle.ToRotationVector2() * velocity.Length();

                Projectile.NewProjectile(source, position, bulletVelocity, type, damage, knockback, player.whoAmI);
            }
        }

        /// <summary>
        /// A wrapper method for the randomizer.
        /// <br/> To use chance, put in a float from 1 - 100. So if you put in 23.5, there would be a 23.5% chance of something happening.
        /// </summary>
        /// <param name="chance"></param>
        /// <returns>True if the randomizer picks a number within your range, and false otherwise.</returns>
        public static bool Randomizer(float chance)
        {
            if (Main.rand.Next(1, 101) <= chance)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This will cycle through all of the frames in the sprite sheet.
        /// <br/> Frame speed is how fast you want it to animate (lower = faster).
        /// </summary>
        /// <param name="frameSpeed"></param>
        public static void FrameRotator(int frameSpeed, Projectile projectile)
        {
            projectile.rotation = projectile.velocity.ToRotation();

            projectile.frameCounter++;
            if (projectile.frameCounter >= frameSpeed)
            {
                projectile.frameCounter = 0;
                projectile.frame++;

                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
        }

        /// <summary>
        /// Allows for items to give the player the ability to rocket jump. This method is mainly used for projectiles
        /// <br/> projectile = The current projectile, so just pass in Projectile.
        /// <br/> item = The item, not the projectile, that you want to have rocket jumping capabilities.
        /// <br/> xVelocity = How fast the player will go horizontally.
        /// <br/> yVelocity = How fast the player will go vertically.
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="item"></param>
        /// <param name="xVelocity"></param>
        /// <param name="yVelocity"></param>
        public static void RocketJump(Projectile projectile, int item, float xVelocity, float yVelocity)
        {
            Player player = Main.player[projectile.owner];

            if (player.Distance(projectile.Center) <= 0)
            {
                xVelocity *= 0f;
                yVelocity *= 1.75f;
            }
            else if (player.Distance(projectile.Center) <= 30)
            {
                xVelocity *= 0.5f;
                yVelocity *= 1.5f;
            }
            else if (player.Distance(projectile.Center) <= 70)
            {
                xVelocity *= 0.75f;
                yVelocity *= 1.25f;
            }
            else if (player.Distance(projectile.Center) <= 100)
            {
                xVelocity *= 1f;
                yVelocity *= 1f;
            }

            if (player.HasItemInAnyInventory(item) && player.Distance(projectile.Center) <= 100) // Within explosion radius
            {
                if (player.Center.X > projectile.Center.X)
                {
                    player.velocity.X += xVelocity;
                }
                else
                {
                    player.velocity.X -= xVelocity;
                }

                if (player.Center.Y > projectile.Center.Y)
                {
                    player.velocity.Y = +yVelocity;
                }
                else
                {
                    player.velocity.Y = -yVelocity;
                }
            }
        }

        public static int AmmoIs(Projectile projectile)
        {
            foreach (var bullet in bulletMap)
            {
                if (bullet.Key == projectile.type)
                {
                    return bullet.Value;
                }
            }

            foreach (var arrow in arrowMap)
            {
                if (arrow.Key == projectile.type)
                {
                    return arrow.Value;
                }
            }

            foreach (var flare in flareMap)
            {
                if (flare.Key == projectile.type)
                {
                    return flare.Value;
                }
            }

            foreach (var rocket in rocketMap)
            {
                if (rocket.Key == projectile.type)
                {
                    return rocket.Value;
                }
            }

            foreach (var dart in dartMap)
            {
                if (dart.Key == projectile.type)
                {
                    return dart.Value;
                }
            }

            return 0;
        }

        public static bool AbsorbedAmmo(Projectile proj, Player.HurtInfo hurtInfo, float chance)
        {
            int amountToGet = 5;

            if (Randomizer(chance))
            {
                int projectileItem = AmmoIs(proj);

                Main.LocalPlayer.QuickSpawnItem(proj.GetSource_DropAsItem(), projectileItem, amountToGet);

                return true;
            }

            return false;
        }
    }
}