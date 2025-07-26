using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Vaultaria.Content.Buffs.AccessoryEffects;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Items.Accessories.Shields;
using Vaultaria.Content.Items.Accessories.Relics;
using Terraria.Audio;
using Terraria.ID;
using Vaultaria.Content.Projectiles.Shields;
using Vaultaria.Common.Globals.Prefixes.GunModifier;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof;
using Terraria.WorldBuilding;
using Vaultaria.Common.Utilities;

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

            if (Player.HasBuff(ModContent.BuffType<OrcEffect>()))
            {
                OrcShot(item, source, position, velocity, type, damage, knockback);
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

        public override float UseSpeedMultiplier(Item item)
        {
            float multiplier = 1;

            if (Player.HasBuff(ModContent.BuffType<OrcEffect>()))
            {
                multiplier *= 1.5f;
            }

            if (Player.HasBuff(ModContent.BuffType<DrunkEffect>()))
            {
                multiplier *= 0.5f;
            }

            if (Player.HasBuff(ModContent.BuffType<DeathEffect>()))
            {
                multiplier *= 1.3f;
            }

            if (IsWearing(ModContent.ItemType<SuperSoldier>()) && Player.statLife == Player.statLifeMax2)
            {
                multiplier *= 1.5f;
            }

            return multiplier;
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            if (Player.HasBuff(ModContent.BuffType<OrcEffect>()))
            {
                damage *= 1.2f;
            }

            if (Player.HasBuff(ModContent.BuffType<DeathEffect>()))
            {
                damage *= 1.3f;
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            int antagonist = ModContent.ItemType<Antagonist>();
            int impaler = ModContent.ItemType<Impaler>();
            int asteroidBelt = ModContent.ItemType<AsteroidBelt>();

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

                HomingCauseProjectile(proj, hurtInfo, ModContent.ProjectileType<HomingSlagBall>(), 0.1f, 2);
            }

            if (IsWearing(impaler))
            {
                HomingCauseProjectile(proj, hurtInfo, ModContent.ProjectileType<ImpalerSpike>(), 0.4f, 2);
            }

            if (IsWearing(asteroidBelt))
            {
                HomingCauseProjectile(proj, hurtInfo, ModContent.ProjectileType<Meteor>(), 0.3f, 2);
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            int antagonist = ModContent.ItemType<Antagonist>();
            int impaler = ModContent.ItemType<Impaler>();
            int asteroidBelt = ModContent.ItemType<AsteroidBelt>();

            if (IsWearing(antagonist))
            {
                HomingCauseHit(npc, hurtInfo, ModContent.ProjectileType<HomingSlagBall>(), 0.2f, 1);
            }

            if (IsWearing(impaler))
            {
                npc.AddBuff(BuffID.Thorns, 60);
                npc.life -= (int)(hurtInfo.SourceDamage * 0.35f);
                npc.AddBuff(ModContent.BuffType<CorrosiveBuff>(), 300);
            }

            if (IsWearing(asteroidBelt))
            {
                HomingCauseHit(npc, hurtInfo, ModContent.ProjectileType<Meteor>(), 0.3f, 2);
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

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            RapierCurse(npc, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int ottoIdol = ModContent.ItemType<OttoIdol>();
            int planetoid = ModContent.ItemType<CommanderPlanetoid>();

            if (IsWearing(ottoIdol))
            {
                if (target.life <= 0)
                {
                    Player.Heal((int)(Player.statLifeMax2 * 0.1f)); // Heals for 10% of health
                }
            }

            if (IsWearing(planetoid))
            {
                if (hit.DamageType == DamageClass.Melee) // Allow only on melee hits
                {
                    ElementRandomizer(target, hit);
                }
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

        private void OrcShot(Item item, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Player player = Main.player[Player.whoAmI];
            Item weapon = player.HeldItem;

            float spread = MathHelper.ToRadians(5);
            Vector2 newVelocity = velocity.RotatedByRandom(spread);

            // 1 Extra projectile
            Projectile.NewProjectile(
                source,
                position,
                newVelocity,
                type,
                damage,
                knockback,
                Player.whoAmI,
                1,
                1
            );
        }

        private void HomingCauseProjectile(Projectile proj, Player.HurtInfo hurtInfo, int homer, float damage, int knockback)
        {
            Vector2 direction = Vector2.Normalize(proj.Center - Player.Center);
            Vector2 spawnPos = Player.Center + direction * 5f;

            Projectile.NewProjectile(
                proj.GetSource_OnHit(Player),
                spawnPos,
                direction * 12f,
                homer,
                (int)(hurtInfo.SourceDamage * damage),
                0f,
                Player.whoAmI
            );
        }

        private void HomingCauseHit(NPC npc, Player.HurtInfo hurtInfo, int homer, float damage, int knockback)
        {
            Vector2 direction = Vector2.Normalize(npc.Center - Player.Center);
            Vector2 spawnPos = Player.Center + direction * 5f;

            Projectile.NewProjectile(
                npc.GetSource_OnHit(Player),
                spawnPos,
                direction * 12f,
                homer,
                (int)(hurtInfo.SourceDamage * damage),
                0f,
                Player.whoAmI
            );
        }

        private void RapierCurse(NPC npc, ref Player.HurtModifiers modifiers)
        {
            if (Player.HeldItem.type == ModContent.ItemType<Rapier>())
            {
                modifiers.SourceDamage *= 3f; // If holding the rapier, take 3x more damage
            }
        }

        private void ElementRandomizer(NPC target, NPC.HitInfo hit)
        {
            switch (Main.rand.Next(1, 7))
            {
                case 1:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, Player, ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff, 60);
                    break;
                case 2:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, Player, ElementalID.ShockProjectile, ElementalID.ShockBuff, 60);
                    break;
                case 3:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, Player, ElementalID.CorrosiveProjectile, ElementalID.CorrosiveBuff, 60);
                    break;
                case 4:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, Player, ElementalID.SlagProjectile, ElementalID.SlagBuff, 60);
                    break;
                case 5:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, Player, ElementalID.CryoProjectile, ElementalID.CryoBuff, 60);
                    break;
                case 6:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, Player, ElementalID.ExplosiveProjectile, ElementalID.ExplosiveBuff, 60);
                    break;
            }
        }
    }
}