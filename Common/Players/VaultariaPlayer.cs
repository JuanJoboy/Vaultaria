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
using Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof;
using Terraria.WorldBuilding;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Bandit;
using System.Collections;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Hyperion;
using Vaultaria.Content.Items.Accessories.Attunements;
using Vaultaria.Content.Buffs.PotionEffects;
using Terraria.ModLoader.IO;
using Vaultaria.Content.Items.Weapons.Ranged.Common.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Common.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Sniper.Jakobs;
using Terraria.GameContent;

namespace Vaultaria.Common.Players
{
    public class VaultariaPlayer : ModPlayer
    {
        public NPC.HitInfo globalNpcHitInfo;

        // 1. Persistence Flag: Saved with the character file.
        public bool hasInitialized = false;

        // 2. The Hook: Runs when the character first loads or enters the world.
        public override void OnEnterWorld()
        {
            Main.NewText($"If using Calamity's Prefix Roller, disable it to access all the prefixes in this mod", Color.Red);

            // The logic runs only if this character has NOT been initialized yet.
            // if (!hasInitialized)
            {
                // --- CUSTOM INITIALIZATION LOGIC GOES HERE ---

                // Example 1: Give the player a starting item (e.g., a ModItem)
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<VaultHuntersRelic>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<GearboxProjectileConvergence>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<GearboxRenegade>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<GearboxMuckamuck>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ItemID.MusketBall, 1000);

                // Example 3: Display a welcome message
                Utilities.Utilities.DisplayStatusMessage(Player.Center, Color.Gold, $"Welcome to Vaultaria, {Player.name}!");

                // Set the flag to true so this code doesn't run again on the next login.
                hasInitialized = true;
            }
        }
        
        // 3. Data Saving: Ensure the flag is saved and loaded with the player.
        public override void SaveData(TagCompound tag)
        {
            tag.Add("hasInitialized", hasInitialized);
        }

        public override void LoadData(TagCompound tag)
        {
            hasInitialized = tag.GetBool("hasInitialized");
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Check if the projectile that just hit is one of my elemental projectiles
            // If it is, do nothing. This prevents the recursive spawning loop
            if (ElementalProjectile.elementalProjectile.Contains(proj.type))
            {
                return;
            }

            float elementalChance = 20f;
            float multiplier = 0.2f;
            int elementalBuffTime = 60;

            ElementalProjectile.HandleElementalProjOnNPC(proj, Player, target, hit, elementalChance, multiplier, ElementalID.ShockPrefix, ElementalID.ShockProjectile, ElementalID.ShockBuff, elementalBuffTime);
            ElementalProjectile.HandleElementalProjOnNPC(proj, Player, target, hit, elementalChance, multiplier, ElementalID.IncendiaryPrefix, ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff, elementalBuffTime);
            ElementalProjectile.HandleElementalProjOnNPC(proj, Player, target, hit, elementalChance, multiplier, ElementalID.CorrosivePrefix, ElementalID.CorrosiveProjectile, ElementalID.CorrosiveBuff, elementalBuffTime);
            ElementalProjectile.HandleElementalProjOnNPC(proj, Player, target, hit, elementalChance, multiplier, ElementalID.SlagPrefix, ElementalID.SlagProjectile, ElementalID.SlagBuff, elementalBuffTime);
            ElementalProjectile.HandleElementalProjOnNPC(proj, Player, target, hit, elementalChance, multiplier, ElementalID.CryoPrefix, ElementalID.CryoProjectile, ElementalID.CryoBuff, elementalBuffTime);
            ElementalProjectile.HandleElementalProjOnNPC(proj, Player, target, hit, elementalChance, multiplier, ElementalID.ExplosivePrefix, ElementalID.ExplosiveProjectile, ElementalID.ExplosiveBuff, elementalBuffTime);
            ElementalProjectile.HandleElementalProjOnNPC(proj, Player, target, hit, 50, multiplier, ElementalID.RadiationPrefix, ElementalID.RadiationProjectile, ElementalID.RadiationBuff, 240);
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
                multiplier *= 1.2f;
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
                damage *= 1.2f;
            }
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            int antagonist = ModContent.ItemType<Antagonist>();
            int impaler = ModContent.ItemType<Impaler>();
            int asteroidBelt = ModContent.ItemType<AsteroidBelt>();
            int sham = ModContent.ItemType<Sham>();
            int aequitas = ModContent.ItemType<Aequitas>();
            int aspis = ModContent.ItemType<AthenasAspis>();

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

            if (IsWearing(aspis))
            {
                HomingCauseProjectile(proj, hurtInfo, ModContent.ProjectileType<Meteor>(), 0.3f, 2);
            }
        }

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            int antagonist = ModContent.ItemType<Antagonist>();
            int impaler = ModContent.ItemType<Impaler>();
            int asteroidBelt = ModContent.ItemType<AsteroidBelt>();
            int aspis = ModContent.ItemType<AthenasAspis>();

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

            if (IsWearing(aspis))
            {
                HomingCauseHit(npc, hurtInfo, ModContent.ProjectileType<AspisShield>(), 0.3f, 2);
                hurtInfo.SourceDamage = (int) (hurtInfo.SourceDamage * 0.75f);
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

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            int aequitas = ModContent.ItemType<Aequitas>();
            int sham = ModContent.ItemType<Sham>();

            if (IsWearing(sham))
            {
                Utilities.Utilities.AbsorbedAmmo(proj, ref modifiers, 94f);
            }
            if (IsWearing(aequitas))
            {
                Utilities.Utilities.AbsorbedAmmo(proj, ref modifiers, 50f);
            }
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            RapierCurse(npc, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            globalNpcHitInfo = hit;

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
        
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            base.OnHitAnything(x, y, victim);

            float multiplier = 0.2f;
            int buffTime = 30;

            if (victim is NPC npcVictim)
            {
                if (IsWearing(ModContent.ItemType<BlightTiger>()))
                {
                    ElementalProjectile.SetElementOnNPC(npcVictim, globalNpcHitInfo, multiplier, Player, ElementalID.CorrosiveProjectile, ElementalID.CorrosiveBuff, buffTime);
                }
                if (IsWearing(ModContent.ItemType<ColdHearted>()))
                {
                    ElementalProjectile.SetElementOnNPC(npcVictim, globalNpcHitInfo, multiplier, Player, ElementalID.CryoProjectile, ElementalID.CryoBuff, buffTime);
                }
                if (IsWearing(ModContent.ItemType<CorruptedSpirit>()))
                {
                    ElementalProjectile.SetElementOnNPC(npcVictim, globalNpcHitInfo, multiplier, Player, ElementalID.SlagProjectile, ElementalID.SlagBuff, buffTime);
                }
                if (IsWearing(ModContent.ItemType<MindBlown>()))
                {
                    ElementalProjectile.SetElementOnNPC(npcVictim, globalNpcHitInfo, multiplier, Player, ElementalID.ExplosiveProjectile, ElementalID.ExplosiveBuff, buffTime);
                }
                if (IsWearing(ModContent.ItemType<Shockra>()))
                {
                    ElementalProjectile.SetElementOnNPC(npcVictim, globalNpcHitInfo, multiplier, Player, ElementalID.ShockProjectile, ElementalID.ShockBuff, buffTime);
                }
                if (IsWearing(ModContent.ItemType<SoulFire>()))
                {
                    ElementalProjectile.SetElementOnNPC(npcVictim, globalNpcHitInfo, multiplier, Player, ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff, buffTime);
                }
                if (IsWearing(ModContent.ItemType<NuclearArms>()))
                {
                    ElementalProjectile.SetElementOnNPC(npcVictim, globalNpcHitInfo, multiplier, Player, ElementalID.RadiationProjectile, ElementalID.RadiationBuff, buffTime);
                }
            }
        }
        
        private bool IsWearing(int accessory)
        {
            // Ignore empty accessory slots and check if the player is wearing the accessory
            for (int i = 0; i < 8 + Player.extraAccessorySlots; i++)
            {
                if (Player.armor[i].ModItem != null && Player.armor[i].ModItem.Type == accessory)
                {
                    return true;
                }
            }

            return false;
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
                case 7:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, Player, ElementalID.RadiationProjectile, ElementalID.RadiationBuff, 60);
                    break;
            }
        }
    }
}