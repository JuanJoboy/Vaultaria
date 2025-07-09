using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Items.Accessories.Shields;
using Terraria.Audio;
using Terraria.ID;
using Vaultaria.Content.Projectiles.Shields;

namespace Vaultaria.Common.Players
{
    public class VaultariaPlayer : ModPlayer
    {
        // This hook is called whenever the player attempts to shoot a projectile.
        // It's ideal for adding extra projectiles based on player state (like buffs).
        public override bool Shoot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Check if the player currently has the DrunkEffect buff active
            if (Player.HasBuff(ModContent.BuffType<DrunkEffect>()))
            {
                DrunkShot(item, source, position, velocity, type, damage, knockback);
            }

            if (item.prefix == ModContent.PrefixType<DoublePenetrating>())
            {
                DoubleShot(item, source, position, velocity, type, damage, knockback);
            }

            // Return true to allow the original projectile to be shot as well.
            // If you return false here, the original projectile from the gun's Shoot method
            // (or vanilla's default shoot) would NOT be spawned, and you'd only get the 5 clones.
            // Since your GrogNozzle's Shoot method explicitly spawns the GrogBullet and returns false,
            // this ModPlayer's Shoot will cause the 5 additional projectiles to be spawned,
            // and then the GrogNozzle's Shoot will spawn the 1 original GrogBullet.
            // This results in 1 (original) + 5 (clones) = 6 projectiles total when the buff is active.
            return true;
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            int antagonist = ModContent.ItemType<Antagonist>();
            int impaler = ModContent.ItemType<Impaler>();

            if (IsWearing(antagonist))
            {
                if (Main.rand.Next(0, 2) == 1) // 50% Deflection chance
                {
                    proj.velocity *= -1f; // Reverse direction
                    proj.owner = Player.whoAmI;
                    proj.friendly = true;
                    proj.hostile = false;
                    proj.damage = (int)(hurtInfo.Damage * 8.8f); // 880% Reflection damage
                    SoundEngine.PlaySound(SoundID.NPCHit4, Player.position);
                }

                Vector2 direction = Vector2.Normalize(proj.Center - Player.Center);
                Vector2 spawnPos = Player.Center + direction * 5f;

                Projectile.NewProjectile(
                    proj.GetSource_OnHit(Player),
                    spawnPos,
                    direction * 12f,
                    ModContent.ProjectileType<HomingSlagBall>(),
                    1,
                    0f,
                    Player.whoAmI
                );
            }
            
            if (IsWearing(impaler))
            {
                Vector2 direction = Vector2.Normalize(proj.Center - Player.Center);
                Vector2 spawnPos = Player.Center + direction * 5f;

                Projectile.NewProjectile(
                    proj.GetSource_OnHit(Player),
                    spawnPos,
                    direction * 12f,
                    ModContent.ProjectileType<ImpalerSpike>(),
                    (int)(hurtInfo.SourceDamage * 0.4f),
                    0f,
                    Player.whoAmI
                );
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            int antagonist = ModContent.ItemType<Antagonist>();
            int impaler = ModContent.ItemType<Impaler>();

            if (IsWearing(antagonist))
            {
                Vector2 direction = Vector2.Normalize(npc.Center - Player.Center);
                Vector2 spawnPos = Player.Center + direction * 5f;

                Projectile.NewProjectile(
                    npc.GetSource_OnHit(Player),
                    spawnPos,
                    direction * 12f,
                    ModContent.ProjectileType<HomingSlagBall>(),
                    1,
                    0f,
                    Player.whoAmI
                );
            }

            if (IsWearing(impaler))
            {
                npc.AddBuff(BuffID.Thorns, 60);
                npc.life -= (int) (hurtInfo.SourceDamage * 0.35f);
                npc.AddBuff(ModContent.BuffType<CorrosiveBuff>(), 300);
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            int antagonist = ModContent.ItemType<Antagonist>();

            if (IsWearing(antagonist))
            {
                modifiers.FinalDamage *= 0.5f;
            }
        }

        private bool IsWearing(int shield)
        {
            // Ignore empty accessory slots and check if the player is wearing the shield
            for (int i = 0; i < 8 + Player.extraAccessorySlots; i++)
            {
                if (Player.armor[i].ModItem != null && Player.armor[i].ModItem.Type == shield)
                {
                    return true;
                }
            }

            return false;
        }

        private void DrunkShot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int extraProjectilesToSpawn = 5; // We want 5 *additional* projectiles
            float totalSpreadAngle = MathHelper.ToRadians(20); // A small, subtle spread for the "drunk" effect (20 degrees)

            if (item.prefix == ModContent.PrefixType<DoublePenetrating>())
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
                    Player.whoAmI, // Assign ownership to the player (crucial for multiplayer and damage attribution)
                    1f,
                    1f
                );
            }
        }

        private void DoubleShot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float spread = MathHelper.ToRadians(5); // A small 5-degree random spread for the extra shot
            Vector2 newVelocity = velocity.RotatedByRandom(spread);

            Projectile.NewProjectile(
                source,
                position,
                newVelocity, // Use the slightly perturbed velocity
                type,
                damage,
                knockback,
                Player.whoAmI,
                1,
                1
            );
        }
    }
}