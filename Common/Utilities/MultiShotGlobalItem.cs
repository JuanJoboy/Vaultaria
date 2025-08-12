using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Vladof;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Maliwan;
using Vaultaria.Content.Projectiles.Ammo.Seraph.SMG.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Summoner.Sentry;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Epic;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Legendary;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare;
using Vaultaria.Content.Projectiles.Shields;
using Vaultaria.Content.Projectiles.Ammo.Pearlescent.AssaultRifle.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.AssaultRifle.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.Shotgun.Hyperion;

namespace Vaultaria.Common.Utilities
{
    public class MultiShotGlobalItem : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Utilities.ItemIs(item.type, ModContent.ItemType<Ogre>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 2, 5);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Shredifier>()))
            {
                Projectile.NewProjectile(source, position - new Vector2(0, -7), velocity, type, damage, knockback, player.whoAmI);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Badaboom>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 5, 15);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Norfleet>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 2, 35);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Maggie>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 7, 5);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<UnkemptHarold>()))
            {
                Projectile projectile = Projectile.NewProjectileDirect(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<UHBullet>(),
                    damage,
                    knockback,
                    player.whoAmI,
                    1f, // Projectile.ai[0] = 1f; (This bullet is the cloner)
                    0f  // Projectile.ai[1] = 0f; (Optional, if you don't need ai[1] yet)
                );

                if (projectile.ModProjectile is UHBullet bullet)
                {
                    bullet.explosiveMultiplier = 1f;
                }
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<LightShow>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 5);
                Projectile.NewProjectile(source, position - new Vector2(0, -5), velocity, type, damage, knockback, player.whoAmI);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Lyuda>()))
            {
                Projectile projectile = Projectile.NewProjectileDirect(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<LyudaBullet>(),
                    damage,
                    knockback,
                    player.whoAmI,
                    1f,
                    0f
                );
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Sawbar>()))
            {
                Projectile projectile = Projectile.NewProjectileDirect(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<SawbarBullet>(),
                    damage,
                    knockback,
                    player.whoAmI,
                    1f,
                    0f
                );
            }
            
            if (Utilities.ItemIs(item.type, ModContent.ItemType<Butcher>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 4, 5);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Fibber>()))
            {
                Projectile.NewProjectileDirect(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<FibberBullet>(),
                    damage,
                    knockback,
                    player.whoAmI,
                    1f,
                    1f
                );
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<GrogNozzle>()))
            {
                Projectile projectile = Projectile.NewProjectileDirect(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<GrogBullet>(),
                    damage,
                    knockback,
                    player.whoAmI
                );

                if (projectile.ModProjectile is GrogBullet bullet)
                {
                    bullet.slagMultiplier = 0.2f;
                }
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<HeartBreaker>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 11, 5);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Orc>()))
            {
                Projectile.NewProjectileDirect(
                    source,
                    position,
                    velocity,
                    type,
                    damage,
                    knockback,
                    player.whoAmI
                );
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Florentine>()))
            {
                Projectile projectile = Projectile.NewProjectileDirect(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<FlorentineBullet>(),
                    damage,
                    knockback,
                    player.whoAmI
                );

                if (projectile.ModProjectile is FlorentineBullet bullet)
                {
                    bullet.shockMultiplier = 0.2f;
                    bullet.slagMultiplier = 0.2f;
                }
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<LeadStorm>()))
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 2, 5);
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<SabreTurret>()))
            {
                player.AddBuff(item.buffType, 2);

                var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, Main.myPlayer);
                projectile.originalDamage = item.damage;
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<MagicMissileEpic>()))
            {
                int projectileIndex = Projectile.NewProjectile(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<HomingSlagBall>(),
                    damage,
                    knockback,
                    player.whoAmI,
                    0f,
                    0f,
                    1f
                );

                Projectile projectile = Main.projectile[projectileIndex];

                if (projectile.ModProjectile is HomingSlagBall grenade)
                {
                    grenade.slagMultiplier = 0.4f;
                }
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<Fastball>()))
            {
                return true;
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<BreathOfTerramorphous>()))
            {
                return true;
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<MagicMissileRare>()))
            {
                int projectileIndex = Projectile.NewProjectile(
                    source,
                    position,
                    velocity,
                    ModContent.ProjectileType<HomingSlagBall>(),
                    damage,
                    knockback,
                    player.whoAmI,
                    0f,
                    0f,
                    1f
                );

                Projectile projectile = Main.projectile[projectileIndex];

                if (projectile.ModProjectile is HomingSlagBall grenade)
                {
                    grenade.slagMultiplier = 0.2f;
                }
            }

            if (Utilities.ItemIs(item.type, ModContent.ItemType<BasicGrenade>()))
            {
                return true;
            }

            return false;
        }
    }
}