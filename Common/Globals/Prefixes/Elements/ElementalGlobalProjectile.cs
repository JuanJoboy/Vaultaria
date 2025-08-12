using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class ElementalGlobalProjectile : GlobalProjectile
    {
        public int firedWeaponPrefixID;
        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            // This is the CRITICAL check to prevent the recursive loop.
            // If this projectile was created by one of our helper methods, its ai[0] will be 1.
            // We check this first and immediately exit the method if it's true.
            if (projectile.ai[1] == 1f)
            {
                return;
            }

            if (source is EntitySource_ItemUse_WithAmmo itemSource)
            {
                // The player instance that fired the projectile is available here
                Player player = itemSource.Player;

                if (ElementalProjectile.elementalPrefix.Contains(itemSource.Item.prefix))
                {
                    firedWeaponPrefixID = itemSource.Item.prefix;
                }

                if (player.HasBuff(ModContent.BuffType<DrunkEffect>()))
                {
                    DrunkShot(itemSource.Item, itemSource, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player);
                }

                if (player.HasBuff(ModContent.BuffType<OrcEffect>()))
                {
                    OrcShot(itemSource.Item, itemSource, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player);
                }

                if (itemSource.Item.prefix == ModContent.PrefixType<MagicDP>() || itemSource.Item.prefix == ModContent.PrefixType<RangerDP>())
                {
                    DoubleShot(itemSource.Item, itemSource, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player);
                }
            }
        }

        private void DrunkShot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, Player player)
        {
            int extraProjectilesToSpawn = 5; // We want 5 *additional* projectiles
            float totalSpreadAngle = MathHelper.ToRadians(20); // A small, subtle spread for the "drunk" effect (20 degrees)

            if (item.prefix == ModContent.PrefixType<MagicDP>() || item.prefix == ModContent.PrefixType<RangerDP>())
            {
                extraProjectilesToSpawn *= 2;
            }

            // Get the base angle of the original projectile's velocity.
            // This ensures the spread is oriented in the direction the player is aiming.
            float baseAngle = velocity.ToRotation();

            // Calculate the angle increment for distributing the extra projectiles evenly.
            // We divide by (extraProjectilesToSpawn - 1) because there are N-1 gaps between N projectiles.
            // If you want them all to be distinct and not overlap, this is key.
            float angleIncrement = totalSpreadAngle / (extraProjectilesToSpawn - 1);

            // Adjust the base angle to center the spread around the original velocity.
            // This makes the spread symmetrical around the primary shot.
            baseAngle -= totalSpreadAngle / 2f;

            for (int i = 0; i < extraProjectilesToSpawn; i++)
            {
                // Calculate the new angle for this specific extra projectile
                float newAngle = baseAngle + (i * angleIncrement);

                // Create the new velocity vector using the calculated angle and the original projectile's speed.
                // velocity.Length() gives the speed of the original projectile.
                Vector2 newVelocity = newAngle.ToRotationVector2() * velocity.Length();

                // Spawn the extra projectile.
                // We use the 'type', 'damage', and 'knockback' from the original shot,
                // so these cloned projectiles inherit the properties of the ammo/gun being used.
                Projectile.NewProjectile(
                    source,     // Use the same source as the original shot (important for loot/effects)
                    position,   // Spawn at the same position as the original shot
                    newVelocity, // Use the newly calculated spread velocity
                    type,       // Use the original projectile type (e.g., GrogBullet, or whatever ammo shoots)
                    damage,     // Use the original damage (already combined gun + ammo damage)
                    knockback,  // Use the original knockback (already combined gun + ammo knockback)
                    player.whoAmI, // Assign ownership to the player (crucial for multiplayer and damage attribution)
                    1f,
                    1f
                );
            }
        }

        private void DoubleShot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, Player player)
        {
            float spread = MathHelper.ToRadians(5);
            Vector2 newVelocity = velocity.RotatedByRandom(spread);

            Projectile.NewProjectile(
                source,
                position,
                newVelocity,
                type,
                damage,
                knockback,
                player.whoAmI,
                1f,
                1f
            );
        }

        private void OrcShot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, Player player)
        {
            float spread = MathHelper.ToRadians(5);
            Vector2 newVelocity = velocity.RotatedByRandom(spread);

            Projectile.NewProjectile(
                source,
                position,
                newVelocity,
                type,
                damage,
                knockback,
                player.whoAmI,
                1f,
                1f
            );
        }
    }
}