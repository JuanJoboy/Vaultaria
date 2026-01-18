using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Setup.Configuration;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Vaultaria.Common.Configs;
using Vaultaria.Common.Systems;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Effervescent.Launcher.Torgue;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Bandit;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Maliwan;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Torgue;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Hyperion;
using System.IO;
using static System.Math;

namespace Vaultaria.Common.Utilities
{
    public static class Utilities
    {
        public static bool startedVault1BossRush = false;
        public static bool startedVault2BossRush = false;

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

        public enum Sounds
        {
            LegendaryDrop,
            DigiCloneSpawn,
            DigiCloneSwap,
            Norfleet,
            Boomacorn,
            Execute,
            RolandsMilkshakes,
            GenericLaser,
            BanditAR,
            BanditARRocket,
            BanditLauncher,
            BanditPistol,
            BanditShotgun,
            BanditSMG,
            Bane,
            DahlARBurst,
            DahlARSingle,
            DahlPistolBurst,
            DahlPistolSingle,
            DahlSMGBurst,
            LascauxBurst,
            DahlSMGSingle,
            DahlSniperBurst,
            DahlSniperSingle,
            Deception,
            ETechARBurst,
            ETechARSingle,
            ETechLauncher,
            ETechPistolBurst,
            ETechPistolSingle,
            ETechSMGBurst,
            ETechSMGSingle,
            ETechShotgun,
            ETechSniperBurst,
            ETechSniperSingle,
            PlasmaCoil,
            HyperionLaser,
            HyperionPistol,
            HyperionShotgun,
            HyperionSMG,
            HyperionSniper,
            JakobsPistol,
            JakobsAR,
            JakobsShotgun,
            JakobsSniper,
            MaliwanContinuousLaser,
            MaliwanLaserSingle,
            MaliwanLauncher,
            MaliwanPistol,
            MaliwanSMG,
            MaliwanSniper,
            PhaselockBase,
            PhaselockRuin,
            TedioreLaser,
            TedioreLaserThrow,
            LaserDisker,
            TedioreLauncher,
            TedioreLauncherThrow,
            TediorePistol,
            TediorePistolThrow,
            TedioreShotgun,
            TedioreShotgunThrow,
            TedioreSMG,
            TedioreSMGThrow,
            TorgueAR,
            TorgueLauncher,
            TorguePistol,
            TorgueShotgun,
            VladofAR,
            VladofARRocket,
            VladofLauncher,
            VladofPistol,
            VladofSniper,
        }

        // An enum of colours so that there's a centralized space for the below colours so that I don't have to keep making new Color(1,1,1), etc
        public enum VaultarianColours
        {
            Incendiary,
            Shock,
            Corrosive,
            Explosive,
            Slag,
            Cryo,
            Radiation,
            Healing,
            RedText,
            CursedText,
            Information,
            Master,
        }

        // A private dictionary that maps the enum to actual colours
        private static Dictionary<VaultarianColours, Color> vaultarianColours = new Dictionary<VaultarianColours, Color>()
        {
            { VaultarianColours.Incendiary, new Color(231, 92, 22) }, // Orange
            { VaultarianColours.Shock, new Color(46, 153, 228) }, // Blue
            { VaultarianColours.Corrosive, new Color(136, 235, 94) }, // Light Green
            { VaultarianColours.Explosive, new Color(228, 227, 105) }, // Light Yellow
            { VaultarianColours.Slag, new Color(207, 164, 245) }, // Purple
            { VaultarianColours.Cryo, new Color(131, 235, 228) }, // Light Blue
            { VaultarianColours.Radiation, new Color(227, 205, 109) }, // Light Yellow
            { VaultarianColours.Healing, new Color(245, 201, 239) }, // Pink
            { VaultarianColours.RedText, Color.Red }, // Red
            { VaultarianColours.CursedText, new Color(0, 249, 199) }, // Cyan
            { VaultarianColours.Information, new Color(224, 224, 224) }, // Light Grey
            { VaultarianColours.Master, new Color(168, 69, 95) }, // Master
        };

        // An array of tiles that should be taken into consideration when trying to generate a vault when a world is made
        public static int[] badTiles =
        [
            // TileID.DemonAltar,
            TileID.BlueDungeonBrick,
            TileID.PinkDungeonBrick, 
            TileID.GreenDungeonBrick,
            TileID.CrackedBlueDungeonBrick,
            TileID.CrackedPinkDungeonBrick, 
            TileID.CrackedGreenDungeonBrick,
            TileID.Ebonstone,
            TileID.ShadowOrbs, 
            TileID.Crimstone,
            TileID.HoneyBlock,
            TileID.Hive, 
            TileID.LihzahrdBrick,
            TileID.LihzahrdAltar,
            TileID.HeavenforgeBrick,
            // TileID.Containers,
            // TileID.Containers2,
            // TileID.FakeContainers,
            // TileID.FakeContainers2,
            TileID.Sand,
            TileID.SandFallBlock,
            // TileID.Silt,
            // TileID.Slush,
            TileID.PlatinumBrick,
            TileID.AstraBrick,

            TileID.HeavyWorkBench,
            TileID.Bottles,
            TileID.OpenDoor,
            TileID.ClosedDoor,

            TileID.Glass,
        ];

        public static int[] badLiquids =
        [
            LiquidID.Shimmer,
            // LiquidID.Lava,
        ];

        public static ArrayList gunGunItemArray = new ArrayList();

        // The 'this' keyword in the signature allows for it to be used as an extension
        // Turns it from the 1st style below into the 2nd style
        // Utilities.GetVaultarianColor(Utilities.VaultarianColours.Slag);
        // Utilities.VaultarianColours.Shock.GetVaultarianColor();
        public static Color GetVaultarianColor(this VaultarianColours colour)
        {
            if(vaultarianColours.TryGetValue(colour, out Color value))
            {
                return value; // Return the correct colour if the enum colour exists
            }

            return Color.White; // Otherwise return white
        }

        public static void Text(List<TooltipLine> tooltips, Mod mod, string name = "Tooltip1", string tooltip = "Uses any normal bullet type as ammo")
        {
            tooltips.Add(new TooltipLine(mod, name, tooltip));
        }

        public static void Text(List<TooltipLine> tooltips, Mod mod, string name, string tooltip, VaultarianColours colour)
        {
            tooltips.Add(new TooltipLine(mod, name, tooltip)
            {
                OverrideColor = colour.GetVaultarianColor()
            });
        }

        public static void RedText(List<TooltipLine> tooltips, Mod mod, string tooltip)
        {
            tooltips.Add(new TooltipLine(mod, "Red Text", tooltip)
            {
                OverrideColor = VaultarianColours.RedText.GetVaultarianColor()
            });
        }

        public static void CursedText(List<TooltipLine> tooltips, Mod mod, string tooltip)
        {
            tooltips.Add(new TooltipLine(mod, "Curse", tooltip)
            {
                OverrideColor = VaultarianColours.CursedText.GetVaultarianColor()
            });
        }

        public static void MultiShotText(List<TooltipLine> tooltips, Item item, int number)
        {
            TooltipLine damageLine = tooltips.Find(tip => tip.Name == "Damage");

            if (damageLine != null)
            {
                Player player = Main.LocalPlayer;
                int finalDamage = (int)player.GetTotalDamage(item.DamageType).ApplyTo(item.damage);
                damageLine.Text = finalDamage + $" x {number}{item.DamageType.DisplayName}";
            }
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
        public static void HealOnNPCHit(NPC target, int damageDone, float healingPercentage, Projectile projectile)
        {
            if(projectile.owner == Main.myPlayer)
            {
                int heal = (int)(damageDone * healingPercentage);
                heal = (int)(heal / 0.075f); // Divide by 0.075f to bring it back to normal
                projectile.vampireHeal(heal, projectile.Center, target);   
            }
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
            if(projectile.owner == Main.myPlayer)
            {
                int heal = (int)(damageDone * healingPercentage);
                heal = (int)(heal / 0.075f); // Divide by 0.075f to bring it back to normal
                projectile.vampireHeal(heal, projectile.Center, target);   
            }
        }

        /// <summary>
        /// Uses the regular heal method that Terraria provides, but is wrapped in a multiplayer-safe guard
        /// </summary>
        public static void Heal(Player player, float amount)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.Heal((int) amount);
            }
        }

        /// <summary>
        /// Takes the parameters of an item's Shoot() method along with how much the new clones should spread and how many clones should appear. Min and Max should be from 1 to 11
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
        public static void CloneShots(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int numberOfAdditionalBullets, float degreeSpread, int min = 0, int max = 0)
        {
            if(player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < numberOfAdditionalBullets; i++)
                {
                    degreeSpread = Main.rand.Next(min, max) switch
                    {
                        1 => -45, // If 1 is returned then degreeSpread =- 5
                        2 => -34,
                        3 => -23,
                        4 => -12,
                        5 => -1,
                        6 => +1,
                        7 => +12,
                        8 => +23,
                        9 => +34,
                        10 => +45,
                        _ => degreeSpread, // Default
                    };

                    // Define a slight spread angle for the bullets (e.g., degreeSpread = 5, 5 degrees total spread)
                    float spreadAngle = MathHelper.ToRadians(degreeSpread); // Convert degrees to radians

                    // Calculate the base rotation of the velocity vector
                    float baseRotation = velocity.ToRotation();

                    // Calculate the individual bullet's angle
                    // This distributes the bullets symmetrically around the original velocity direction
                    float bulletAngle = baseRotation + MathHelper.Lerp(-spreadAngle / 2, spreadAngle / 2, (float)i / (numberOfAdditionalBullets - 1));

                    // Calculate the new velocity vector for this bullet
                    Vector2 bulletVelocity = bulletAngle.ToRotationVector2() * velocity.Length();

                    Projectile.NewProjectile(source, position, bulletVelocity, type, damage, knockback, player.whoAmI);
                }
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

        public static void MinionFrameRotator(int frameSpeed, Projectile projectile)
        {
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

            projectile.spriteDirection = projectile.direction;
        }

        /// <summary>
        /// Iterates through a loop from 0 to the amountOfDust, and creates that much dust
        /// <br/> amountOfDust = The amount of dust you want.
        /// <br/> projectile = The current projectile, so just pass in Projectile.
        /// <br/> dustID = The desired dust type (DustID.JungleSpore).
        /// <br/> gravityOff = Put false for gravity and true for gravity on the dust.
        /// </summary>
        /// <param name="amountOfDust"></param>
        /// <param name="projectile"></param>
        /// <param name="dustID"></param>
        /// <param name="gravityOff"></param>
        public static void DustMaker(int amountOfDust, Projectile projectile, short dustID, bool gravityOff = false)
        {
            for (int i = 0; i < amountOfDust; i++)
            {
                Dust.NewDustPerfect(projectile.Center, dustID).noGravity = gravityOff;
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

            // The first 2 values are the range, and the 3rd is the percentage (distance divided by 100 to get a %)
            xVelocity *= MathHelper.Lerp(0.0f, 1.5f, Abs(player.Center.X - projectile.Center.X) / 100); // So for X, the player shouldnt move at all if they shoot directly under them 
            yVelocity *= MathHelper.Lerp(2.5f, 0.5f, Abs(player.Center.Y - projectile.Center.Y) / 100); // For Y, the closer you are to the explosion, the higher the multiplier

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

        public static void AbsorbedAmmo(Player player, Projectile proj, ref Player.HurtModifiers modifiers, float chance)
        {
            int amountToGet = 5;

            if (Randomizer(chance))
            {
                int projectileItem = AmmoIs(proj);

                modifiers.FinalDamage *= 0.05f;

                player.QuickSpawnItem(proj.GetSource_DropAsItem(), projectileItem, amountToGet);
            }
        }

        public static void MoveToTarget(Entity movingEntity, Entity target, float moveSpeed, float accelerationRate)
        {
            // Set Distance to Player
            float distanceToTarget = Vector2.Distance(movingEntity.Center, target.Center);

            // Set Move Speeds
            float movementSpeed = moveSpeed / distanceToTarget;

            float targetVelocityX = (target.Center.X - movingEntity.Center.X) * movementSpeed;
            float targetVelocityY = (target.Center.Y - movingEntity.Center.Y) * movementSpeed;

            // Apply Acceleration
            if (movingEntity.velocity.X < targetVelocityX)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.X += accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.X < 0f && targetVelocityX > 0f)
                {
                    movingEntity.velocity.X += accelerationRate;
                }
            }

            if (movingEntity.velocity.X > targetVelocityX)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.X -= accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.X > 0f && targetVelocityX < 0f)
                {
                    movingEntity.velocity.X -= accelerationRate;
                }
            }

            if (movingEntity.velocity.Y < targetVelocityY)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.Y += accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.Y < 0f && targetVelocityY > 0f)
                {
                    movingEntity.velocity.Y += accelerationRate;
                }
            }

            if (movingEntity.velocity.Y > targetVelocityY)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.Y -= accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.Y > 0f && targetVelocityY < 0f)
                {
                    movingEntity.velocity.Y -= accelerationRate;
                }
            }   
        }

        public static void MoveToPosition(Entity movingEntity, Vector2 position, float moveSpeed, float accelerationRate)
        {
            // Set Distance to Player
            float distanceToTarget = Vector2.Distance(movingEntity.Center, position);

            // Set Move Speeds
            float movementSpeed = moveSpeed / distanceToTarget;

            float targetVelocityX = (position.X - movingEntity.Center.X) * movementSpeed;
            float targetVelocityY = (position.Y - movingEntity.Center.Y) * movementSpeed;

            // Apply Acceleration
            if (movingEntity.velocity.X < targetVelocityX)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.X += accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.X < 0f && targetVelocityX > 0f)
                {
                    movingEntity.velocity.X += accelerationRate;
                }
            }

            if (movingEntity.velocity.X > targetVelocityX)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.X -= accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.X > 0f && targetVelocityX < 0f)
                {
                    movingEntity.velocity.X -= accelerationRate;
                }
            }

            if (movingEntity.velocity.Y < targetVelocityY)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.Y += accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.Y < 0f && targetVelocityY > 0f)
                {
                    movingEntity.velocity.Y += accelerationRate;
                }
            }

            if (movingEntity.velocity.Y > targetVelocityY)
            {
                // Increase Velocity by Acceleration
                movingEntity.velocity.Y -= accelerationRate;

                // Further increase velocity
                if (movingEntity.velocity.Y > 0f && targetVelocityY < 0f)
                {
                    movingEntity.velocity.Y -= accelerationRate;
                }
            }
        }
        
        public static void DisplayStatusMessage(Vector2 position, Color colour, string msg)
        {
            // Use MessageID.ChatText to send a chat message to all players.
            // remoteClient: -1 (all clients)
            // ignoreClient: -1 (no client ignored)
            // Used for multiplayer to send the message to everyone
            if(Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(MessageID.CombatTextString, -1, -1, NetworkText.FromLiteral(msg), (int) colour.PackedValue, position.X, position.Y);
            }

            // Display the text at the position
            CombatText.NewText(
                new Rectangle((int)position.X, (int)position.Y, 1, 1), 
                colour, // The color of the text (e.g., gold)
                msg, // The message you want to display
                dramatic: true, // Optional: Makes the text larger and appear more impactful
                dot: false
            );
        }

        public static void SetItemSound(Item item, Sounds sound, int instances = 60)
        {
            item.UseSound = new SoundStyle($"Vaultaria/Common/Sounds/{sound}") 
            {
                // Allow up to 60 concurrent instances of the sound to play. 
                // This makes fast firing sound layered and prevents harsh cutoffs.
                MaxInstances = instances
            };
        }

        // Used for most tile and wall checks to see if the current area is a vault
        public static bool VaultArea(Point16 vaultDimensions, int positionX, int positionY, int i, int j)
        {
            int topLeftCorner = positionX;
            int topRightCorner = positionX + vaultDimensions.X;
            int bottomLeftCorner = positionY;
            int bottomRightCorner = positionY + vaultDimensions.Y;

            if(i >= topLeftCorner && i < topRightCorner && j >= bottomLeftCorner && j < bottomRightCorner)
            {
                return true;
            }

            return false;
        }

        // Used just for KillWall() in VaultWalls.cs
        public static void VaultArea(Point16 vaultDimensions, int positionX, int positionY, int i, int j, ref bool fail)
        {
            int topLeftCorner = positionX;
            int topRightCorner = positionX + vaultDimensions.X;
            int bottomLeftCorner = positionY;
            int bottomRightCorner = positionY + vaultDimensions.Y;

            if(i >= topLeftCorner && i < topRightCorner && j >= bottomLeftCorner && j < bottomRightCorner)
            {
                fail = true;
            }
        }

        public static bool IsWearing(Player player, int accessory)
        {
            // Ignore empty accessory slots and check if the player is wearing the accessory
            for (int i = 0; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem.Type == accessory)
                {
                    return true;
                }
            }

            return false;
        }

        // A helper method that tracks what bosses have been defeated
        public static float DownedBossCounter()
        {
            float counter = 0f;
            
            // --- Pre-Hardmode Bosses ---
            if (NPC.downedSlimeKing) // 1
            {
                counter++;
            }
            if (NPC.downedBoss1) // Eye of Cthulhu 2
            {
                counter++;
            }
            if (NPC.downedBoss2) // EoW / BoC 3
            {
                counter++;
            }
            if (NPC.downedQueenBee) // 4
            {
                counter++;
            }
            if (NPC.downedDeerclops) // 5
            {
                counter++;
            }
            if (NPC.downedBoss3) // Skeletron 6
            {
                counter++;
            }
            if (Main.hardMode) // 7
            {
                counter++; // Wall of Flesh is tracked by Main.hardMode
            }

            // --- Hardmode Bosses ---
            if (NPC.downedQueenSlime) // 8
            {
                counter++;
            }
            if (NPC.downedMechBoss1) // The Destroyer 9
            {
                counter++;
            }
            if (NPC.downedMechBoss2) // The Twins 10 
            {
                counter++;
            }
            if (NPC.downedMechBoss3) // Skeletron Prime 11
            {
                counter++;
            }
            if (NPC.downedPlantBoss) // 12
            {
                counter++;
            }
            if (NPC.downedGolemBoss) // 13
            {
                counter++;
            }
            if (NPC.downedFishron) // 14
            {
                counter++;
            }
            if (NPC.downedEmpressOfLight) // 15
            {
                counter++;
            }

            // --- Lunar and Final Bosses ---
            if (NPC.downedAncientCultist) // 16
            {
                counter++;
            }
            if (NPC.downedTowerSolar) // 17
            {
                counter++;
            }
            if (NPC.downedTowerVortex) // 18
            {
                counter++;
            }
            if (NPC.downedTowerNebula) // 19
            {
                counter++;
            }
            if (NPC.downedTowerStardust) // 20
            {
                counter++;
            }
            if (NPC.downedMoonlord) // 21
            {
                counter++;
            }
            
            // --- Invasions and Events ---
            if (NPC.downedFrost) // 22
            {
                counter++;
            }
            if (NPC.downedGoblins) // 23
            {
                counter++;
            }
            if (NPC.downedMartians) // 24
            {
                counter++;
            }
            if (NPC.downedPirates) // 25
            {
                counter++;
            }
            
            if (NPC.downedChristmasTree) // 26
            {
                counter++;
            }
            if (NPC.downedChristmasSantank) // 27
            {
                counter++;
            }
            if (NPC.downedChristmasIceQueen) // 28
            {
                counter++;
            }
            if (NPC.downedHalloweenTree) // 29
            {
                counter++;
            }
            if (NPC.downedHalloweenKing) // 30
            {
                counter++;
            }

            // --- Additional Flag ---
            if (NPC.downedClown) // 31
            {
                counter++;
            }

            return counter;
        }

        /// <summary>
        /// Calculates a bonus based on the difference between 2 values
        /// <br/> The full formula is:
        /// <br/> (((highestValue - currentValue) / highestValue) / divisor)
        /// <br/> First it gets highest number (Player.statLifeMax2) then it takes away the current value (), and then divides it by the highest number again so that it becomes within the 100% area instead of being 1000%. Then it divides it by the divisor for scaling purposes.
        /// <br/> divisor = The number that balances the item.
        /// </summary>
        public static float ComparativeBonus(float highestValue, float currentValue, float divisor)
        {
            float value = highestValue - currentValue;

            if(value == 0)
            {
                value = 1;
            }
            if(value < 0)
            {
                value *= -1;
            }

            float bonus = (value / highestValue) / divisor;

            if(bonus < 0)
            {
                bonus *= -1;
            }

            return bonus;
        }

        /// <summary>
        /// Calculates a bonus based on the difference between 2 values
        /// <br/> The full formula is:
        /// <br/> (int) (100f * (1 / divisor))
        /// <br/> Since it just shows what it can be UP TO, then for the sake of showing what the highest damage is, the highestValue - currentValue would just be the highestValue, and since it's divided against itself, it's just 1. So to skip all that, 1 is placed in the formula. Then it divides against the divisor and multiplies against 100 to get a percentage.
        /// <br/> divisor = The number that balances the item.
        /// </summary>
        public static int DisplayComparativeBonusText(float divisor)
        {
            return (int) (100f * (1 / divisor));
        }

        /// <summary>
        /// Calculates the bonus effect that a skill should provide
        /// <br/> The full formula is:
        /// <br/> 1f + (DownedBossCounter() / divisor) + baseValue
        /// <br/> First it gets the number of bosses that have been killed in the world, then divides it by the number you provide, and then adds the base value on. Then it adds 1 to it so that it becomes greater than 1, so that it can be used to multiply against values when doing calculations (Player.moveSpeed *= bonusSpeed; == Player.moveSpeed *= 1.4f;).
        /// <br/> divisor = The number that balances the item. For example, if I kill every boss and have the counter at 30, and at most this skill should only provide 100% bonus movement speed, then the divisor should be 30, so that 30 / 30 = 1, and 1 is equal to 100% for Terraria calculations.
        /// <br/> baseValue = A base starting value for when no bosses have been killed, but the item should still give an effect.
        /// </summary>
        public static float SkillBonus(float divisor, float baseValue = 0)
        {
            return 1f + (DownedBossCounter() / divisor) + baseValue;
        }

        /// <summary>
        /// Calculates the bonus effect that a skill should provide
        /// <br/> The full formula is:
        /// <br/> (int) (100 * + ((DownedBossCounter() / divisor) + baseValue))
        /// <br/> First it gets the number of bosses that have been killed in the world, then divides it by the number you provide, and then adds the base value on. Then it multiplies it by 100 to get a percentage and converts it to an int to round it.
        /// <br/> divisor = The number that balances the item. For example, if I kill every boss and have the counter at 30, and at most this skill should only provide 100% bonus movement speed, then the divisor should be 30, so that 30 / 30 = 1, and 1 is equal to 100% for Terraria calculations.
        /// <br/> baseValue = A base starting value for when no bosses have been killed, but the item should still give an effect.
        /// </summary>
        public static int DisplaySkillBonusText(float divisor, float baseValue = 0)
        {
            return (int) (100f * ((DownedBossCounter() / divisor) + baseValue));
        }

        public static void PlaceItemsInChest(int[] itemsToPlaceInChest, int itemsToPlaceInChestChoice, int itemsPlaced, int maxItems, int internalChestID)
        {
			// Loop over all the chests
			for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
				Chest chest = Main.chest[chestIndex];

				if (chest == null)
                {
					continue;
				}

				Tile chestTile = Main.tile[chest.x, chest.y];
				// We need to check if the current chest is the Frozen Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Frozen Chest.
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Frozen Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Frozen_Chest
				if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == internalChestID * 36)
                {
					// We have found a Frozen Chest
					// If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
					// if (WorldGen.genRand.NextBool(3))
                    // {
					// 	continue;
                    // }

					// Next we need to find the first empty slot for our item
					for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
						if (chest.item[inventoryIndex].type == ItemID.None)
                        {
							// Place the item
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChest[itemsToPlaceInChestChoice]);
							// Decide on the next item that will be placed.
							itemsToPlaceInChestChoice = (itemsToPlaceInChestChoice + 1) % itemsToPlaceInChest.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInChest));
							itemsPlaced++;
							break;
						}
					}
				}

				// Once we've placed as many items as we wanted, break out of the loop
				if (itemsPlaced >= maxItems)
                {
					break;
				}
			}
        }
    }
}