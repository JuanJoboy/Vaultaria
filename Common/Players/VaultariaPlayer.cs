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
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Dahl;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Summoner.Sentry;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Buffs.SkillEffects;
using System.Formats.Tar;
using Vaultaria.Common.Configs;

namespace Vaultaria.Common.Players
{
    public class VaultariaPlayer : ModPlayer
    {
        public NPC.HitInfo globalNpcHitInfo;
        private static int numTimesPenetrated = 0;

        // 1. Persistence Flag: Saved with the character file.
        public bool hasInitialized = false;

        // 2. The Hook: Runs when the character first loads or enters the world.
        public override void OnEnterWorld()
        {
            Main.NewText($"If using Calamity's Prefix Roller, disable it to access all the prefixes in this mod", Color.Red);

            // The logic runs only if this character has NOT been initialized yet.
            if (!hasInitialized)
            {
                // --- CUSTOM INITIALIZATION LOGIC GOES HERE ---

                // Example 1: Give the player a starting item (e.g., a ModItem)
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<VaultHuntersRelic>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<GearboxProjectileConvergence>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<GearboxRenegade>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<GearboxMuckamuck>(), 1);
                Player.QuickSpawnItem(Player.GetSource_None(), ModContent.ItemType<CopperBullet>(), 600);

                // Example 3: Display a welcome message
                Utilities.Utilities.DisplayStatusMessage(Player.Center, Color.Gold, $"Welcome to Vaultaria, {Player.name}!");

                // Set the flag to true so this code doesn't run again on the next login.
                hasInitialized = true;
            }
        }
        
        // 3. Data Saving: Ensure the flag is saved and loaded with the player
        public override void SaveData(TagCompound tag)
        {
            tag.Add("hasInitialized", hasInitialized);
        }

        public override void LoadData(TagCompound tag)
        {
            hasInitialized = tag.GetBool("hasInitialized");
        }

        public override void UpdateDead()
        {
            base.UpdateDead();

            TerminationProtocols();
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithProj(proj, target, hit, damageDone);

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

            HitWithElectricBanjoOn(target, hit);

            if (IsWearing(ModContent.ItemType<MoonlightSaga>()))
            {
                if (Player.ZoneSkyHeight)
                {
                    Utilities.Utilities.HealOnNPCHit(target, damageDone, 0.1f, proj);
                }
            }

            if(IsHolding(ModContent.ItemType<NightHawkin>()))
            {
                if(Utilities.Utilities.Randomizer(30))
                {
                    if(Main.dayTime)
                    {
                        ElementalProjectile.SetElementOnNPC(target, 0.75f, Player, ElementalID.CryoProjectile, ElementalID.CryoBuff, 120);
                    }
                    else
                    {
                        ElementalProjectile.SetElementOnNPC(target, 0.75f, Player, ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff, 120);
                    }
                }
            }

            if(IsHolding(ModContent.ItemType<AkumasDemise>()))
            {
                if (ElementalProjectile.SetElementalChance(75))
                {
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.75f, Player, ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff, 180);
                }
            }

            if(IsHolding(ModContent.ItemType<Oracle>()))
            {                
                proj.penetrate = 2;

                if(hit.Crit && numTimesPenetrated < 5)
                {
                    Utilities.Utilities.MoveToTarget(proj, target, 10, 10);
                    numTimesPenetrated++;
                }
                else
                {
                    proj.Kill();
                    numTimesPenetrated = 0;
                }
            }

            if(IsHolding(ModContent.ItemType<CatONineTails>()))
            {                
                if (ElementalProjectile.SetElementalChance(20))
                {
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.75f, Player, ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff, 180);
                }
            }

            if(proj.active && proj.owner == Player.whoAmI && proj.minion && Player.HasBuff<GammaBurstBuff>())
            {
                hit.SourceDamage *= 2;
                ElementalProjectile.SetElementOnNPC(target, hit, 1f, Player, ElementalID.RadiationProjectile, ElementalID.RadiationBuff, 180);
            }

            Salvation(hit);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPCWithItem(item, target, hit, damageDone);

            HitWithElectricBanjoOn(target, hit);
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

            if(Player.HasBuff(ModContent.BuffType<IncitePassive>()))
            {
                multiplier *= Utilities.Utilities.SkillBonus(100f, 0.05f);
            }

            if(Player.HasBuff(ModContent.BuffType<KillerKillSkill>()))
            {
                multiplier *= Utilities.Utilities.SkillBonus(40f, 0.05f);
            }

            return multiplier;
        }

        public override void PostUpdateRunSpeeds()
        {
            base.PostUpdateRunSpeeds();

            if(IsHolding(ModContent.ItemType<Bane>()))
            {
                float speedPenalty = 0.2f;

                // Apply the 80% reduction (0.2f remaining speed)
                Player.moveSpeed *= speedPenalty; 
                Player.runAcceleration *= speedPenalty;
                Player.accRunSpeed *= speedPenalty;
                Player.maxRunSpeed *= speedPenalty;
                
                // Also apply the penalty to wing-specific speed stats
                Player.wingRunAccelerationMult *= speedPenalty;
                Player.wingAccRunSpeed *= speedPenalty;
            }

            if(Player.HasBuff(ModContent.BuffType<IncitePassive>()))
            {
                float bonusSpeed = Utilities.Utilities.SkillBonus(85f, 0.05f);

                Player.moveSpeed *= bonusSpeed; 
                Player.runAcceleration *= bonusSpeed;
                Player.accRunSpeed *= bonusSpeed;
                Player.maxRunSpeed *= bonusSpeed;
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(item, ref damage);

            if (Player.HasBuff(ModContent.BuffType<OrcEffect>()))
            {
                damage *= 1.2f;
            }

            if (Player.HasBuff(ModContent.BuffType<DeathEffect>()))
            {
                damage *= 1.2f;
            }
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            base.ModifyHitNPCWithItem(item, target, ref modifiers);

            BackStab(target, ref modifiers);

            KillingBlow(target, ref modifiers);
        }

        public override void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
        {
            int antagonist = ModContent.ItemType<Antagonist>();
            int impaler = ModContent.ItemType<Impaler>();
            int asteroidBelt = ModContent.ItemType<AsteroidBelt>();
            int sham = ModContent.ItemType<Sham>();
            int aequitas = ModContent.ItemType<Aequitas>();

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

            Grit(ref modifiers);

            Incite();
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            RapierCurse(npc, ref modifiers);

            Grit(ref modifiers);

            Incite();
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

            Resurgence(target);
        }
        
        public override void OnHitAnything(float x, float y, Entity victim)
        {
            base.OnHitAnything(x, y, victim);

            float multiplier = 0.2f;
            int buffTime = 60;

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

        private bool IsHolding(int item)
        {
            if(Player.HeldItem.type == item)
            {
                return true;
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

        private void HitWithElectricBanjoOn(NPC target, NPC.HitInfo hit)
        {
            if(IsWearing(ModContent.ItemType<ElectricBanjo>()))
            {
                for(int i = 0; i < Main.npc.Length; i++)
                {
                    if(Main.npc[i].townNPC == false && Vector2.Distance(Main.npc[i].Center, target.Center) < 250)
                    {
                        if(Utilities.Utilities.Randomizer(20))
                        {
                            ElementalProjectile.SetElementOnNPC(Main.npc[i], hit, 0.3f, Player, ElementalID.ShockProjectile, ElementalID.ShockBuff, 120);
                        }
                    }
                }
            }
        }

        private void BackStab(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if((IsWearing(ModContent.ItemType<Backstab>()) || IsWearing(ModContent.ItemType<LegendaryNinja>())) && Player.HeldItem.DamageType == DamageClass.Melee)
            {
                float bonusDamage = Utilities.Utilities.SkillBonus(65f, 0.05f);

                if(npc.direction == 1 && Player.Center.X < npc.Center.X)
                {
                    modifiers.SourceDamage *= bonusDamage;
                }
                else if(npc.direction == -1 && Player.Center.X > npc.Center.X)
                {
                    modifiers.SourceDamage *= bonusDamage;
                }
            }
        }

        private void Grit(ref Player.HurtModifiers modifiers)
        {
            if(IsWearing(ModContent.ItemType<Grit>()))
            {
                float numberOfBossesDefeated = Utilities.Utilities.DownedBossCounter();

                float baseGrit = 5f;

                float gritChance = numberOfBossesDefeated * 2 + baseGrit;

                if(Utilities.Utilities.Randomizer(gritChance))
                {
                    modifiers.FinalDamage *= 0;
                    modifiers.Knockback *= 0;
                }
            }
        }

        private void Incite()
        {
            if(IsWearing(ModContent.ItemType<Incite>()))
            {
                Player.AddBuff(ModContent.BuffType<IncitePassive>(), 360);
            }
        }

        private void KillingBlow(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if((IsWearing(ModContent.ItemType<KillingBlow>()) || IsWearing(ModContent.ItemType<LegendaryNinja>())) && Player.HeldItem.DamageType == DamageClass.Melee)
            {
                float bonusDamage = Utilities.Utilities.SkillBonus(65f, 0.05f);

                if(npc.life <= npc.lifeMax * 0.2f)
                {
                    modifiers.SourceDamage *= bonusDamage;
                }
            }
        }

        private void Resurgence(NPC npc)
        {
            if((IsWearing(ModContent.ItemType<Resurgence>()) || IsWearing(ModContent.ItemType<LegendaryNinja>())) && Player.HeldItem.DamageType == DamageClass.Melee)
            {
                float bonusHealth = Utilities.Utilities.SkillBonus(300f) - 1; // Without the -1, it'll be 1.x which means player.life * 1.x will be like 500 instead of 50

                if (npc.life <= 0)
                {
                    Player.Heal((int) (Player.statLifeMax2 * bonusHealth)); // Heals for % of health
                }
            }
        }

        private void Salvation(NPC.HitInfo hit)
        {
            if(Player.HasBuff(ModContent.BuffType<SalvationKillSkill>()) && Player.HeldItem.DamageType == DamageClass.Ranged)
            {
                float Lifesteal = Utilities.Utilities.SkillBonus(600f) - 1;

                Player.Heal((int) (hit.SourceDamage * Lifesteal));
            }
        }

        private void TerminationProtocols()
        {
            if(IsWearing(ModContent.ItemType<TerminationProtocols>()))
            {
                float damage = Player.statDefense * 4f;

                VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

                if(config.VaultHunterMode == 3)
                {
                    damage *= 2f;
                }
                else if(config.VaultHunterMode == 2)
                {
                    damage *= 1.5f;
                }

                if(Main.masterMode)
                {
                    damage *= 3;
                }
                else if(Main.expertMode)
                {
                    damage *= 2;
                }

                Projectile.NewProjectileDirect(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ElementalID.LargeExplosiveProjectile, (int) damage, 4);
            }
        }
    }
}