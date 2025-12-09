using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Buffs.SkillEffects;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Jakobs;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Common.Globals
{
    public class ElementalGlobalProjectile : GlobalProjectile
    {
        public int firedWeaponPrefixID;
        public override bool InstancePerEntity => true;

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPC(projectile, target, ref modifiers);

            Player player = Main.player[projectile.owner];

            if(player.HeldItem.type == ModContent.ItemType<LadyFist>())
            {
                modifiers.CritDamage += 8;
            }

            if(player.HeldItem.type == ModContent.ItemType<Trespasser>())
            {
                modifiers.Defense *= 0;
                modifiers.DefenseEffectiveness *= 0;
                modifiers.ArmorPenetration += 100000;
            }

            JustGotReal(player, projectile, ref modifiers);

            Wreck(player, projectile, ref modifiers);

            Accelerate(player, projectile, ref modifiers);

            Reaper(player, projectile, target, ref modifiers);

            Impact(player, projectile, ref modifiers);

            Ranger(player, projectile, ref modifiers);

            Killer(player, projectile, ref modifiers);

            Headshot(player, projectile, ref modifiers);

            Velocity(player, projectile, ref modifiers);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(projectile, target, hit, damageDone);

            Player player=  Main.player[projectile.owner];

            Immolate(player, projectile, target, hit);
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            Player player = Main.player[projectile.owner];

            if(player.HeldItem.type == ModContent.ItemType<CatONineTails>())
            {
                return CatONineTails(projectile, oldVelocity);
            }

            return base.OnTileCollide(projectile, oldVelocity);
        }

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
                    OrcShot(itemSource, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player);
                }

                if (itemSource.Item.prefix == ModContent.PrefixType<MagicDP>() || itemSource.Item.prefix == ModContent.PrefixType<RangerDP>())
                {
                    MultiShot(itemSource, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player, 1);
                }
                if (itemSource.Item.prefix == ModContent.PrefixType<MagicMasher>() || itemSource.Item.prefix == ModContent.PrefixType<RangerMasher>())
                {
                    MultiShot(itemSource, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player, 4);
                }

                if (Utilities.Utilities.IsWearing(player, ModContent.ItemType<Bore>()))
                {
                    projectile.penetrate = -1;
                    projectile.maxPenetrate = -1;
                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 120;
                }

                Accelerate(player, projectile);

                Velocity(player, projectile);
            }

            if (source is EntitySource_Parent parent)
            {
                Projectile minion = parent.Entity as Projectile;

                if (minion != null && (minion.minion || minion.sentry))
                {
                    Player player = Main.player[minion.owner];

                    int minionPrefix = player.HeldItem.prefix;

                    projectile.GetGlobalProjectile<ElementalGlobalProjectile>().firedWeaponPrefixID = minionPrefix;

                    if (player.HasBuff(ModContent.BuffType<DrunkEffect>()))
                    {
                        DrunkShot(player.HeldItem, source, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player);
                    }

                    if (player.HasBuff(ModContent.BuffType<OrcEffect>()))
                    {
                        OrcShot(source, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player);
                    }

                    if (minionPrefix == ModContent.PrefixType<MagicDP>() || minionPrefix == ModContent.PrefixType<RangerDP>())
                    {
                        MultiShot(source, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player, 1);
                    }
                    if (minionPrefix == ModContent.PrefixType<MagicMasher>() || minionPrefix == ModContent.PrefixType<RangerMasher>())
                    {
                        MultiShot(source, projectile.position, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, player, 4);
                    }
                }
            }
        }

        private void DrunkShot(Item item, IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, Player player)
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

        private void MultiShot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, Player player, int amountOfShots)
        {
            float spread = MathHelper.ToRadians(5);

            for (int i = 0; i < amountOfShots; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(spread);

                Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI, 1f, 1f);
            }
        }

        private void OrcShot(IEntitySource source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, Player player)
        {
            float spread = MathHelper.ToRadians(5);
            Vector2 newVelocity = velocity.RotatedByRandom(spread);

            Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback, player.whoAmI, 1f, 1f);
        }

        private bool CatONineTails(Projectile projectile, Vector2 oldVelocity)
        {
            projectile.penetrate = 2;
            projectile.penetrate--;

            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
                return false;
            }

            if (Math.Abs(projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                projectile.velocity.X = -oldVelocity.X;
            }

            if (Math.Abs(projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }

            // --- CLONING LOGIC ---
            if (projectile.ai[0] == 1f) // Check if this is the designated "parent" bullet
            {
                if (projectile.ai[0] == 1f && projectile.ai[1] == 1f) // If it's a cloner parent AND eligible for first split
                {
                    const int numberOfClones = 9;
                    const float totalSpreadDegrees = 5;
                    float baseAngle = projectile.velocity.ToRotation();
                    float angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / (numberOfClones - 1));
                    
                    // Adjust the base angle so the spread is centered around the original velocity.
                    baseAngle -= MathHelper.ToRadians(totalSpreadDegrees) / 2f;

                    for (int i = 0; i < numberOfClones; i++)
                    {
                        // Calculate the new angle for this specific clone
                        float newAngle = baseAngle + (i * angleIncrement);

                        // Create the new velocity vector using the calculated angle and the parent's speed.
                        Vector2 newVelocity = newAngle.ToRotationVector2() * projectile.velocity.Length();

                        // Spawn the new projectile (clone)
                        Projectile.NewProjectile(
                            projectile.GetSource_FromThis(),
                            projectile.Center,
                            newVelocity,
                            projectile.type,
                            20,
                            projectile.knockBack,
                            projectile.owner,
                            0f,
                            0f
                        );
                    }

                    projectile.ai[0] = 0f;
                    projectile.ai[1] = 0f;
                }
            }

            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);

            return false;
        }

        private void JustGotReal(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<JustGotReal>()))
            {
                float bonusDamage = Utilities.Utilities.ComparativeBonus(player.statLifeMax2, player.statLife, 3f) + Utilities.Utilities.SkillBonus(60f, 0.05f);

                if(projectile.DamageType == DamageClass.Ranged)
                {
                    modifiers.SourceDamage *= bonusDamage;
                }
            }
        }

        private void Wreck(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(player.HasBuff(ModContent.BuffType<WreckPassiveSkill>()))
            {
                float bonusDamage = Utilities.Utilities.SkillBonus(80f, 0.05f);

                if(projectile.DamageType == DamageClass.Magic)
                {
                    modifiers.SourceDamage *= bonusDamage;
                }
            }
        }

        private void Accelerate(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Accelerate>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendarySiren>()))
            {
                float bonusDamage = Utilities.Utilities.SkillBonus(100f, 0.05f);

                if(projectile.DamageType == DamageClass.Magic)
                {
                    modifiers.SourceDamage *= bonusDamage;
                }
            }
        }

        private void Accelerate(Player player, Projectile projectile)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Accelerate>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendarySiren>()))
            {
                float bonusProjectileSpeed = Utilities.Utilities.SkillBonus(80f, 0.05f);

                if(projectile.DamageType == DamageClass.Magic)
                {
                    projectile.velocity *= bonusProjectileSpeed;
                }
            }
        }

        private void Reaper(Player player, Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Reaper>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendarySiren>()))
            {
                float bonusDamage = Utilities.Utilities.SkillBonus(35f, 0.05f);

                if(projectile.DamageType == DamageClass.Magic && target.life >= target.lifeMax * 0.5f)
                {
                    modifiers.SourceDamage *= bonusDamage;
                }
            }
        }

        private void Immolate(Player player, Projectile projectile, NPC target, NPC.HitInfo hit)
        {
            if(player.HasBuff(ModContent.BuffType<ImmolatePassiveSkill>()))
            {
                float bonusDamage = Utilities.Utilities.SkillBonus(20f, 0.1f);

                short incendiaryProjectile;

                if(Utilities.Utilities.DownedBossCounter() < 16)
                {
                    incendiaryProjectile = ElementalID.IncendiaryProjectile;
                }
                else
                {
                    incendiaryProjectile = ProjectileID.SolarWhipSwordExplosion;
                }

                if(projectile.DamageType == DamageClass.Magic)
                {
                    ElementalProjectile.SetElementOnNPC(target, hit, bonusDamage, player, incendiaryProjectile, ElementalID.IncendiaryBuff, 120);
                }
            }
        }

        private void Impact(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Impact>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendaryRanger>()))
            {
                if(projectile.DamageType == DamageClass.Ranged)
                {
                    float bonusDamage = Utilities.Utilities.SkillBonus(80f, 0.05f);

                    modifiers.SourceDamage *= bonusDamage;
                }
            }
        }

        private void Ranger(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Ranger>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendaryRanger>()))
            {
                if(projectile.DamageType == DamageClass.Ranged)
                {
                    float bonusDamage = Utilities.Utilities.SkillBonus(200f, 0.01f);

                    modifiers.SourceDamage *= bonusDamage;
                    modifiers.CritDamage *= bonusDamage;
                }
            }
        }

        private void Killer(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(player.HasBuff<KillerKillSkill>() && (projectile.DamageType == DamageClass.Magic || projectile.DamageType == DamageClass.Ranged || projectile.DamageType == DamageClass.Throwing))
            {
                float critBonus = Utilities.Utilities.SkillBonus(55f, 0.05f);
                modifiers.CritDamage *= critBonus;
            }
        }

        private void Headshot(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Headshot>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendaryKiller>()))
            {
                if(projectile.DamageType == DamageClass.Magic || projectile.DamageType == DamageClass.Ranged || projectile.DamageType == DamageClass.Throwing)
                {
                    float bonusCrit = Utilities.Utilities.SkillBonus(80f, 0.05f);

                    modifiers.CritDamage *= bonusCrit;
                }
            }
        }

        private void Velocity(Player player, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Velocity>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendaryKiller>()))
            {
                if(projectile.DamageType == DamageClass.Magic || projectile.DamageType == DamageClass.Ranged || projectile.DamageType == DamageClass.Throwing)
                {
                    float bonusDamage = Utilities.Utilities.SkillBonus(200f, 0.05f);
                    float bonusCrit = Utilities.Utilities.SkillBonus(150f, 0.05f);

                    modifiers.SourceDamage *= bonusDamage;
                    modifiers.CritDamage *= bonusCrit;
                }
            }
        }

        private void Velocity(Player player, Projectile projectile)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Velocity>()) || Utilities.Utilities.IsWearing(player, ModContent.ItemType<LegendaryKiller>()))
            {
                if(projectile.DamageType == DamageClass.Magic || projectile.DamageType == DamageClass.Ranged || projectile.DamageType == DamageClass.Throwing)
                {
                    float bonusProjectileSpeed = Utilities.Utilities.SkillBonus(15f, 0.1f);

                    projectile.velocity *= bonusProjectileSpeed;
                }
            }
        }
    }
}