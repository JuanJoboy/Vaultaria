using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using SubworldLibrary;
using Terraria;
using Terraria.DataStructures;
using System.Collections;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Players;
using Vaultaria.Content.Items.Accessories.Attunements;
using Vaultaria.Content.Items.Accessories.Relics;
using Vaultaria.Content.Items.Accessories.Shields;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Items.Weapons.Magic;
using Vaultaria.Content.Items.Weapons.Ranged.Common.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Tediore;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Shotgun.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Common.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Effervescent.Launcher.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Eridian;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Epic;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Legendary;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Tediore;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Tediore;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Tediore;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.AssaultRifle.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.Shotgun.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Launcher.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.AssaultRifle.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Shotgun.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Sniper.Maliwan;
using Vaultaria.Content.Items.Weapons.Summoner.Sentry;
using Vaultaria.Content.NPCs.Town.Claptrap;

namespace Vaultaria.Common.GlobalItems
{
    public class NPCLoot : GlobalNPC
    {
        public override void OnChatButtonClicked(NPC npc, bool firstButton)
        {
            base.OnChatButtonClicked(npc, firstButton);

            Player player = Main.player[Main.myPlayer];
            QuestRewardsPlayer modPlayer = player.GetModPlayer<QuestRewardsPlayer>();

            QuestItem(npc, 30, ref modPlayer.ladyFistCollected, player, ModContent.ItemType<LadyFist>());
            QuestItem(npc, 50, ref modPlayer.swordSplosionCollected, player, ModContent.ItemType<SwordSplosion>());
        }

        private void QuestItem(NPC npc, int questNumber, ref bool condition, Player player, int item)
        {
            if(npc.type == NPCID.Angler)
            {
                if(player.anglerQuestsFinished > questNumber && condition == false)
                {
                    player.QuickSpawnItem(player.GetSource_GiftOrReward(), item, 1);
                    condition = true;
                }
            }
        }

        public override void ModifyNPCLoot(NPC mob, Terraria.ModLoader.NPCLoot npcLoot)
        {
            int npc = mob.type;

            //********************************** NPC's **********************************//

			if (npc == ModContent.NPCType<Claptrap>())
            {
                npcLoot.Add(ItemDropRule.ByCondition(new LaserDiskerCondition(), ModContent.ItemType<LaserDisker>(), 1, 1, 1));
            }

            Bane(npc, npcLoot);

            if (npc == NPCID.GoblinSorcerer || npc == NPCID.GoblinArcher)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Headshot>(), 50, 1, 1));
            }
            if (npc == NPCID.GraniteFlyer || npc == NPCID.GraniteGolem)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ElectricBanjo>(), 50, 1, 1));
            }

            if (npc == NPCID.GiantTortoise || npc == NPCID.IceTortoise || npc == NPCID.Derpling)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FabledTortoise>(), 100, 1, 1));
            }

            if (npc == NPCID.Nymph)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Deathless>(), 4, 1, 1));
            }

            if (npc == NPCID.Medusa)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Oracle>(), 10, 1, 1));
            }

            Leech(npc, npcLoot);

            if (npc == NPCID.RedDevil)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Flakker>(), 10, 1, 1));
            }

            if (npc == NPCID.Pixie || npc == NPCID.Unicorn)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SwordSplosion>(), 100, 1, 1));
            }

            if (npc == NPCID.WyvernHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoonlightSaga>(), 10, 1, 1));
            }

            if (npc == NPCID.IceGolem)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrainFreeze>(), 10, 1, 1));
            }

            if (npc == NPCID.Eyezor || npc == NPCID.Frankenstein || npc == NPCID.SwampThing || npc == NPCID.Vampire || npc == NPCID.CreatureFromTheDeep || npc == NPCID.Fritz || npc == NPCID.ThePossessed || npc == NPCID.Reaper || npc == NPCID.Mothron || npc == NPCID.Butcher || npc == NPCID.DeadlySphere || npc == NPCID.DrManFly || npc == NPCID.Nailhead || npc == NPCID.Psycho)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PlasmaCoil>(), 1000, 1, 1));
            }

            OttoIdol(npc, npcLoot);

            Cobra(npc, npcLoot);

            Pimpernel(npc, npcLoot);

            //********************************** Bosses *********************************//
            if(mob.boss)
            {
                Eridium(npcLoot, 1, 3);
            }

            if (npc == NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Hornet>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Backstab>(), 5, 1, 1));
                Eridium(npcLoot, 1, 3);
            }

            if (npc == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Law>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PhaselockSpell>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Accelerate>(), 10, 1, 1));
                Eridium(npcLoot, 3, 6);
            }

            if (npc == NPCID.TheGroom || npc == NPCID.TheBride || npc == NPCID.BloodZombie || npc == NPCID.Drippler)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BasicGrenade>(), 20, 30, 60));
                Eridium(npcLoot, 2, 5);
            }

            if (npc == NPCID.BrainofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CloudKill>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Velocity>(), 10, 1, 1));
                Eridium(npcLoot, 3, 6);
            }

            if (npc == NPCID.EaterofWorldsHead || npc == NPCID.EaterofWorldsBody || npc == NPCID.EaterofWorldsTail)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CloudKill>(), 300, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Velocity>(), 450, 1, 1));

                if (npc == NPCID.EaterofWorldsHead)
                {
                    Eridium(npcLoot, 1, 2);
                }
            }

            if (npc == NPCID.QueenBee)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlameOfTheFirehawk>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BabyMaker>(), 10, 1, 1));
                Eridium(npcLoot, 3, 6);
            }

            if (npc == NPCID.Deerclops)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UnkemptHarold>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DigiClone>(), 10, 1, 1));
                Eridium(npcLoot, 3, 6);
            }

            if (npc == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Hail>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wreck>(), 1, 1, 1));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Impaler>(), 3, 1, 1));
                Eridium(npcLoot, 10, 15);
            }

            if (npc == NPCID.GoblinShark || npc == NPCID.BloodEelHead || npc == NPCID.BloodNautilus || npc == NPCID.BloodSquid)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Badaboom>(), 12, 1, 1));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.QueenSlimeBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Florentine>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Striker>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PersonalSpace>(), 10, 1, 1));
                Eridium(npcLoot, 15, 18);
            }

            if (npc == NPCID.BigMimicCorruption)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Blackout>(), 10, 1, 1));
                Eridium(npcLoot, 5, 15);
            }

            if (npc == NPCID.BigMimicCrimson)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CatONineTails>(), 10, 1, 1));
                Eridium(npcLoot, 5, 15);
            }

            if (npc == NPCID.BigMimicHallow)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Seraphim>(), 10, 1, 1));
                Eridium(npcLoot, 5, 15);
            }

            if (npc == NPCID.TheDestroyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LeadStorm>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<JustGotReal>(), 10, 1, 1));
                Eridium(npcLoot, 18, 25);
            }

            if (npc == NPCID.Retinazer || npc == NPCID.Spazmatism)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fastball>(), 10, 100, 200));
                Eridium(npcLoot, 18, 25);
            }

            if (npc == NPCID.SkeletronPrime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheBee>(), 10, 1, 1));
                Eridium(npcLoot, 18, 25);
            }

            if (npc == NPCID.Mothron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sham>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Skullmasher>(), 10, 1, 1));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.PirateShip || npc == NPCID.PirateCaptain)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Nukem>(), 5, 1, 1));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.Everscream)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Zim>(), 25, 1, 1));
                Eridium(npcLoot, 3, 5);
            }

            if (npc == NPCID.IceQueen)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Hive>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Refreshment>(), 10, 1, 1));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.Pumpking)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WorldBurn>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GhastCall>(), 20, 50, 100));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.MourningWood)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sawbar>(), 25, 1, 1));
                Eridium(npcLoot, 3, 5);
            }

            if (npc == NPCID.MartianSaucerCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Shockblast>(), 10, 1, 1));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.DD2DarkMageT3)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagicMissileRare>(), 5, 50, 100));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagicMissileEpic>(), 10, 200, 300));
                Eridium(npcLoot, 3, 5);
            }

            if (npc == NPCID.DD2OgreT3)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Orc>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Ogre>(), 10, 1, 1));
                Eridium(npcLoot, 3, 5);
            }

            if (npc == NPCID.DD2Betsy)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BreathOfTerramorphous>(), 8, 50, 100));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Grit>(), 4, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GrogNozzle>(), 3, 1, 1));
                Eridium(npcLoot, 6, 10);
            }

            if (npc == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fibber>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Salvation>(), 10, 1, 1));
                Eridium(npcLoot, 20, 30);
            }

            if (npc == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LuckCannon>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HuntersEye>(), 10, 1, 1));
                Eridium(npcLoot, 10, 15);
            }

            if (npc == NPCID.DukeFishron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeathRattle>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Deliverance>(), 10, 1, 1));
                Eridium(npcLoot, 20, 30);
            }

            if (npc == NPCID.HallowBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Lyuda>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.EmpressOfLightIsGenuinelyEnraged(), ModContent.ItemType<Norfleet>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ViolentMomentum>(), 10, 1, 1));
                Eridium(npcLoot, 20, 30);
            }

            if (npc == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Antagonist>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<QuickCharge>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AkumasDemise>(), 1, 1, 1));
                Eridium(npcLoot, 30, 35);
            }

            if (npc == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<VaultFragment6>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CommanderPlanetoid>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HideOfTerramorphous>(), 20, 1, 1));
                Eridium(npcLoot, 25, 40);
            }

            VaultBosses(npc, npcLoot);
        }

        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            base.ModifyGlobalLoot(globalLoot);

            DropMiscItems(globalLoot);
            Attunements(globalLoot);
            TubbyDrops(globalLoot);
        }

        private void Eridium(Terraria.ModLoader.NPCLoot npcLoot, int min, int max)
        {
            EridiumRule eridiumRule = new EridiumRule();
            eridiumRule.min = min;
            eridiumRule.max = max;

            npcLoot.Add(eridiumRule);
        }
        
        private void DropMiscItems(GlobalLoot globalLoot)
        {
            // Ammo
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<PistolAmmo>(), 20, 1, 25));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<SubmachineGunAmmo>(), 20, 1, 25));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<AssaultRifleAmmo>(), 25, 1, 25));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShotgunAmmo>(), 25, 1, 25));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<SniperAmmo>(), 30, 1, 10));
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<LauncherAmmo>(), 30, 1, 10));

            // Eridium
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<Eridium>(), 100, 1, 2));
        }

        private void Attunements(GlobalLoot globalLoot)
        {
            globalLoot.Add(ItemDropRule.ByCondition(new SoulFireCondition(), ModContent.ItemType<SoulFire>(), 1000, 1, 1));
            globalLoot.Add(ItemDropRule.ByCondition(new ShockraCondition(), ModContent.ItemType<Shockra>(), 1000, 1, 1));
            globalLoot.Add(ItemDropRule.ByCondition(new BlightTigerCondition(), ModContent.ItemType<BlightTiger>(), 1000, 1, 1));
            globalLoot.Add(ItemDropRule.ByCondition(new MindBlownCondition(), ModContent.ItemType<MindBlown>(), 1000, 1, 1));
            globalLoot.Add(ItemDropRule.ByCondition(new CorruptedSpiritCondition(), ModContent.ItemType<CorruptedSpirit>(), 1000, 1, 1));
            globalLoot.Add(ItemDropRule.ByCondition(new ColdHeartedCondition(), ModContent.ItemType<ColdHearted>(), 1000, 1, 1));
            globalLoot.Add(ItemDropRule.ByCondition(new NuclearArmsCondition(), ModContent.ItemType<NuclearArms>(), 1000, 1, 1));
        }

        private void Cobra(int npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cobra>(), 10000, 1, 1));
            }

            if (npc == NPCID.DungeonGuardian)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cobra>(), 1, 1, 1));
            }

            if (npc == NPCID.CultistArcherBlue || npc == NPCID.CultistArcherWhite || npc == NPCID.CultistDevote || npc == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cobra>(), 100, 1, 1));
            }

            if (npc == NPCID.AngryBones || npc == NPCID.DarkCaster || npc == NPCID.CursedSkull || npc == NPCID.DungeonSlime || npc == NPCID.SpikeBall || npc == NPCID.BlazingWheel)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new CobraCondition(), ModContent.ItemType<Cobra>(), 1000, 1, 1));
            }

            if(npc == NPCID.BlueArmoredBones || npc == NPCID.BlueArmoredBonesMace || npc == NPCID.BlueArmoredBonesNoPants || npc == NPCID.BlueArmoredBonesSword || npc == NPCID.RustyArmoredBonesAxe || npc == NPCID.RustyArmoredBonesFlail || npc == NPCID.RustyArmoredBonesSword || npc == NPCID.RustyArmoredBonesSwordNoArmor || npc == NPCID.HellArmoredBones || npc == NPCID.HellArmoredBonesMace || npc == NPCID.HellArmoredBonesSpikeShield || npc == NPCID.HellArmoredBonesSword || npc == NPCID.Paladin || npc == NPCID.Necromancer || npc == NPCID.NecromancerArmored || npc == NPCID.RaggedCaster || npc == NPCID.RaggedCasterOpenCoat || npc == NPCID.DiabolistRed || npc == NPCID.DiabolistWhite || npc == NPCID.SkeletonCommando || npc == NPCID.SkeletonSniper || npc == NPCID.TacticalSkeleton || npc == NPCID.GiantCursedSkull || npc == NPCID.BoneLee || npc == NPCID.DungeonSpirit)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Cobra>(), 1000, 1, 1));
            }
        }

        private void Pimpernel(int npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc == NPCID.Parrot || npc == NPCID.PirateCaptain || npc == NPCID.PirateCorsair || npc == NPCID.PirateCrossbower || npc == NPCID.PirateDeadeye || npc == NPCID.PirateDeckhand || npc == NPCID.PirateGhost || npc == NPCID.PirateShip)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pimpernel>(), 500, 1, 1));
            }
        }

        private void OttoIdol(int npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc == NPCID.CursedHammer || npc == NPCID.Clinger || npc == NPCID.Corruptor || npc == NPCID.CorruptSlime || npc == NPCID.Slimer || npc == NPCID.PigronCorruption || npc == NPCID.DesertGhoulCorruption)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OttoIdol>(), 100, 1, 1));
            }

           if (npc == NPCID.BloodCrawler || npc == NPCID.BloodCrawlerWall || npc == NPCID.FaceMonster || npc == NPCID.LittleCrimera || npc == NPCID.Crimera || npc == NPCID.BigCrimera || npc == NPCID.BloodJelly || npc == NPCID.BloodFeeder || npc == NPCID.CrimsonAxe || npc == NPCID.Herpling || npc == NPCID.IchorSticker || npc == NPCID.FloatyGross || npc == NPCID.DesertGhoulCrimson || npc == NPCID.PigronCrimson)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OttoIdol>(), 100, 1, 1));
            }
        }

        private void Bane(int npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc == NPCID.Vulture || npc == NPCID.Antlion || npc == NPCID.FlyingAntlion || npc == NPCID.WalkingAntlion || npc == NPCID.GiantWalkingAntlion || npc == NPCID.GiantFlyingAntlion || npc == NPCID.SandSlime || npc == NPCID.DesertGhoul)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bane>(), 100, 1, 1));
            }
        }

        private void Leech(int npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc == NPCID.Hellbat || npc == NPCID.LavaSlime || npc == NPCID.FireImp || npc == NPCID.Demon || npc == NPCID.VoodooDemon)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Leech>(), 10, 5, 15));
            }
        }

        private void VaultBosses(int npc, Terraria.ModLoader.NPCLoot npcLoot)
        {
            if (npc == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<WarriorsTail>(), 1, 1, 1));
            }

            if(npc == NPCID.QueenSlimeBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.Pumpking)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.IceQueen)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.SkeletronPrime)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.DD2Betsy)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.DukeFishron)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.HallowBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if(npc == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }

            if (npc == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<DestroyersEye>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<EridianFabricator>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.ByCondition(new VaultCondition(), ModContent.ItemType<Moonstone>(), 20, 1, 1));
            }
        }

        private void TubbyDrops(GlobalLoot npcLoot)
        {
            // Chubby Drops
            // 1. Create the base condition rule
            LeadingConditionRule chubbyRule = new LeadingConditionRule(new ChubbyCondition());

            // 2. Chain your custom set rule to it using OnSuccess
            chubbyRule.OnSuccess(new OneFromManyOptions(1, 1, AncientArmorDrop()));
            chubbyRule.OnSuccess(new OneFromManyOptions(1, 1, MasterModePreHardmodeDrop()));
            chubbyRule.OnSuccess(ItemDropRule.OneFromOptions(1, ItemID.Terragrim, ItemID.Arkhalis, ItemID.FalconBlade, ItemID.MoneyTrough, ItemID.SlimeStaff, ItemID.DirtiestBlock, ItemID.CreativeWings, ItemID.DemonHeart, ItemID.HardySaddle, ItemID.SuperheatedBlood, ItemID.HellMinecart, ModContent.ItemType<FollowThrough>(), ModContent.ItemType<Killer>(), ModContent.ItemType<Quad>(), ModContent.ItemType<UnkemptHarold>(), ModContent.ItemType<Volcano>(), ModContent.ItemType<FirstBlood>()));

            // 3. Add the leading rule to the loot table
            npcLoot.Add(chubbyRule);

            // Tubby Drops
            // 1. Create the base condition rule
            LeadingConditionRule tubbyRule = new LeadingConditionRule(new TubbyCondition());

            // 2. Chain your custom set rule to it using OnSuccess
            tubbyRule.OnSuccess(new OneFromManyOptions(1, 1, DevSetDrop()));
            tubbyRule.OnSuccess(new OneFromManyOptions(1, 1, MasterModeHardmodeDrop()));
            tubbyRule.OnSuccess(ItemDropRule.OneFromOptions(1, ItemID.FrozenKey, ItemID.JungleKey, ItemID.CrimsonKey, ItemID.CorruptionKey, ItemID.HallowedKey, ItemID.DungeonDesertKey, ItemID.Uzi, ItemID.DiscountCard, ItemID.RodofDiscord, ItemID.CoinGun, ItemID.TheAxe, ItemID.TeleportationPylonVictory, ItemID.DiggingMoleMinecart, ItemID.GoldenFishingRod, ModContent.ItemType<Sawbar>()));

            // 3. Add the leading rule to the loot table
            npcLoot.Add(tubbyRule);
        }

        private int[][] DevSetDrop()
        {
            int[][] devSets = new int[][] 
            {
                new int[] { ItemID.AaronsHelmet, ItemID.AaronsBreastplate, ItemID.AaronsLeggings }, // Case 0
                new int[] { ItemID.Arkhalis, ItemID.ArkhalisHat, ItemID.ArkhalisPants, ItemID.ArkhalisWings }, // Case 1
                new int[] { ItemID.CrownosMask, ItemID.CrownosBreastplate, ItemID.CrownosLeggings, ItemID.CrownosWings }, // Case 2
                new int[] { ItemID.DTownsHelmet, ItemID.DTownsBreastplate, ItemID.DTownsLeggings, ItemID.DTownsWings }, // Case 3
                new int[] { ItemID.JimsHelmet, ItemID.JimsBreastplate, ItemID.JimsLeggings, ItemID.JimsWings }, // Case 4
                new int[] { ItemID.BejeweledValkyrieHead, ItemID.BejeweledValkyrieBody, ItemID.BejeweledValkyrieWing, ItemID.ValkyrieYoyo }, // Case 5
                new int[] { ItemID.LeinforsHat, ItemID.LeinforsShirt, ItemID.LeinforsPants, ItemID.LeinforsAccessory, ItemID.LeinforsWings }, // Case 6
                new int[] { ItemID.LokisHelm, ItemID.LokisShirt, ItemID.LokisPants, ItemID.LokisDye, ItemID.LokisWings }, // Case 7
                new int[] { ItemID.RedsHelmet, ItemID.RedsBreastplate, ItemID.RedsLeggings, ItemID.RedsYoyo, ItemID.RedsWings }, // Case 8
                new int[] { ItemID.SkiphsHelm, ItemID.SkiphsShirt, ItemID.SkiphsPants, ItemID.SkiphsWings, ItemID.DevDye }, // Case 9
                new int[] { ItemID.WillsHelmet, ItemID.WillsBreastplate, ItemID.WillsLeggings, ItemID.WillsWings }, // Case 10
                new int[] { ItemID.Yoraiz0rHead, ItemID.Yoraiz0rShirt, ItemID.Yoraiz0rPants, ItemID.Yoraiz0rDarkness, ItemID.Yoraiz0rWings }, // Case 11
                new int[] { ItemID.GroxTheGreatHelm, ItemID.GroxTheGreatArmor, ItemID.GroxTheGreatGreaves, ItemID.GroxTheGreatWings }, // Case 12
                new int[] { ItemID.FoodBarbarianHelm, ItemID.FoodBarbarianArmor, ItemID.FoodBarbarianGreaves, ItemID.FoodBarbarianWings }, // Case 13
                new int[] { ItemID.SafemanSunHair, ItemID.SafemanSunDress, ItemID.SafemanDressLeggings, ItemID.SafemanWings }, // Case 14
                new int[] { ItemID.GhostarSkullPin, ItemID.GhostarShirt, ItemID.GhostarPants, ItemID.GhostarsWings } // Case 15
            };

            return devSets;
        }

        private int[][] AncientArmorDrop()
        {
            int[][] ancientArmorSets = new int[][]
            {
                new int[] { ItemID.AncientCobaltHelmet, ItemID.AncientCobaltBreastplate, ItemID.AncientCobaltLeggings },
                new int[] { ItemID.AncientGoldHelmet, ItemID.GoldChainmail, ItemID.GoldGreaves },
                new int[] { ItemID.AncientIronHelmet, ItemID.IronChainmail, ItemID.IronGreaves },
                new int[] { ItemID.AncientNecroHelmet, ItemID.NecroBreastplate, ItemID.NecroGreaves },
                new int[] { ItemID.AncientShadowHelmet, ItemID.AncientShadowScalemail, ItemID.AncientShadowGreaves }
            };

            return ancientArmorSets;
        }

        private int[][] MasterModePreHardmodeDrop()
        {
            int[][] ancientArmorSets = new int[][]
            {
                new int[] { ItemID.KingSlimePetItem },
                new int[] { ItemID.EyeOfCthulhuPetItem },
                new int[] { ItemID.EaterOfWorldsPetItem },
                new int[] { ItemID.BrainOfCthulhuPetItem },
                new int[] { ItemID.QueenBeePetItem },
                new int[] { ItemID.SkeletronPetItem },
                new int[] { ItemID.DeerclopsPetItem },
                new int[] { ItemID.WallOfFleshGoatMountItem }
            };

            return ancientArmorSets;
        }

        private int[][] MasterModeHardmodeDrop()
        {
            int[][] ancientArmorSets = new int[][]
            {
                new int[] { ItemID.QueenSlimePetItem },
                new int[] { ItemID.DestroyerPetItem },
                new int[] { ItemID.TwinsPetItem },
                new int[] { ItemID.SkeletronPrimePetItem },
                new int[] { ItemID.PlanteraPetItem },
                new int[] { ItemID.GolemPetItem },
                new int[] { ItemID.FairyQueenPetItem },
                new int[] { ItemID.DukeFishronPetItem },
                new int[] { ItemID.LunaticCultistPetItem },
                new int[] { ItemID.MoonLordPetItem },

                new int[] { ItemID.SpookyWoodMountItem },
                new int[] { ItemID.PumpkingPetItem },
                new int[] { ItemID.EverscreamPetItem },
                new int[] { ItemID.SantankMountItem },
                new int[] { ItemID.IceQueenPetItem },
                new int[] { ItemID.DarkMageBookMountItem },
                new int[] { ItemID.DD2OgrePetItem },
                new int[] { ItemID.DD2BetsyPetItem },
                new int[] { ItemID.PirateShipMountItem },
                new int[] { ItemID.MartianPetItem }
            };

            return ancientArmorSets;
        }
    }
}

public class NewItemLoot : GlobalItem
{
    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        AllBossBags(item, itemLoot);

        AllCrates(item, itemLoot);
    }

    private void AllBossBags(Item item, ItemLoot itemLoot)
    {
        BossBagLoot(item, itemLoot, ItemID.KingSlimeBossBag, ModContent.ItemType<Hornet>());
        BossBagLoot(item, itemLoot, ItemID.KingSlimeBossBag, ModContent.ItemType<Backstab>(), 5);

        BossBagLoot(item, itemLoot, ItemID.EyeOfCthulhuBossBag, ModContent.ItemType<Law>());
        BossBagLoot(item, itemLoot, ItemID.EyeOfCthulhuBossBag, ModContent.ItemType<PhaselockSpell>());
        BossBagLoot(item, itemLoot, ItemID.EyeOfCthulhuBossBag, ModContent.ItemType<Accelerate>());

        BossBagLoot(item, itemLoot, ItemID.EaterOfWorldsBossBag, ModContent.ItemType<CloudKill>());
        BossBagLoot(item, itemLoot, ItemID.EaterOfWorldsBossBag, ModContent.ItemType<Velocity>());

        BossBagLoot(item, itemLoot, ItemID.BrainOfCthulhuBossBag, ModContent.ItemType<CloudKill>());
        BossBagLoot(item, itemLoot, ItemID.BrainOfCthulhuBossBag, ModContent.ItemType<Velocity>());

        BossBagLoot(item, itemLoot, ItemID.QueenBeeBossBag, ModContent.ItemType<FlameOfTheFirehawk>(), 5);
        BossBagLoot(item, itemLoot, ItemID.QueenBeeBossBag, ModContent.ItemType<BabyMaker>());

        BossBagLoot(item, itemLoot, ItemID.DeerclopsBossBag, ModContent.ItemType<UnkemptHarold>());
        BossBagLoot(item, itemLoot, ItemID.DeerclopsBossBag, ModContent.ItemType<DigiClone>());

        BossBagLoot(item, itemLoot, ItemID.SkeletronBossBag, ModContent.ItemType<Hail>(), 1);
        BossBagLoot(item, itemLoot, ItemID.SkeletronBossBag, ModContent.ItemType<Wreck>(), 1);

        BossBagLoot(item, itemLoot, ItemID.WallOfFleshBossBag, ModContent.ItemType<Impaler>(), 5);

        BossBagLoot(item, itemLoot, ItemID.QueenSlimeBossBag, ModContent.ItemType<Florentine>());
        BossBagLoot(item, itemLoot, ItemID.QueenSlimeBossBag, ModContent.ItemType<Striker>());
        BossBagLoot(item, itemLoot, ItemID.QueenSlimeBossBag, ModContent.ItemType<PersonalSpace>());

        BossBagLoot(item, itemLoot, ItemID.DestroyerBossBag, ModContent.ItemType<LeadStorm>());
        BossBagLoot(item, itemLoot, ItemID.DestroyerBossBag, ModContent.ItemType<JustGotReal>());

        BossBagLoot(item, itemLoot, ItemID.TwinsBossBag, ModContent.ItemType<Fastball>(), 5, 100, 200);

        BossBagLoot(item, itemLoot, ItemID.SkeletronPrimeBossBag, ModContent.ItemType<TheBee>());

        BossBagLoot(item, itemLoot, ItemID.BossBagBetsy, ModContent.ItemType<BreathOfTerramorphous>(), 8, 50, 100);
        BossBagLoot(item, itemLoot, ItemID.BossBagBetsy, ModContent.ItemType<Grit>(), 4);
        BossBagLoot(item, itemLoot, ItemID.BossBagBetsy, ModContent.ItemType<GrogNozzle>(), 3);

        BossBagLoot(item, itemLoot, ItemID.PlanteraBossBag, ModContent.ItemType<Fibber>(), 5);
        BossBagLoot(item, itemLoot, ItemID.PlanteraBossBag, ModContent.ItemType<Salvation>(), 10);

        BossBagLoot(item, itemLoot, ItemID.GolemBossBag, ModContent.ItemType<LuckCannon>());
        BossBagLoot(item, itemLoot, ItemID.GolemBossBag, ModContent.ItemType<HuntersEye>());

        BossBagLoot(item, itemLoot, ItemID.FishronBossBag, ModContent.ItemType<DeathRattle>());
        BossBagLoot(item, itemLoot, ItemID.FishronBossBag, ModContent.ItemType<Deliverance>());

        BossBagLoot(item, itemLoot, ItemID.FairyQueenBossBag, ModContent.ItemType<Lyuda>());
        BossBagLoot(item, itemLoot, ItemID.FairyQueenBossBag, ModContent.ItemType<ViolentMomentum>());

        BossBagLoot(item, itemLoot, ItemID.CultistBossBag, ModContent.ItemType<Antagonist>(), 5);
        BossBagLoot(item, itemLoot, ItemID.CultistBossBag, ModContent.ItemType<QuickCharge>(), 5);
        BossBagLoot(item, itemLoot, ItemID.CultistBossBag, ModContent.ItemType<AkumasDemise>(), 1);

        BossBagLoot(item, itemLoot, ItemID.MoonLordBossBag, ModContent.ItemType<VaultFragment6>(), 1);
        BossBagLoot(item, itemLoot, ItemID.MoonLordBossBag, ModContent.ItemType<CommanderPlanetoid>());
        BossBagLoot(item, itemLoot, ItemID.MoonLordBossBag, ModContent.ItemType<HideOfTerramorphous>(), 20);
    }

    private void BossBagLoot(Item item, ItemLoot itemLoot, int bossBag, int newItem, int chance = 10, int min = 1, int max = 1)
    {
        if (item.type == bossBag)
        {
            itemLoot.Add(ItemDropRule.Common(newItem, chance, min, max));
        }
    }

    private void AllCrates(Item item, ItemLoot itemLoot)
    {
        if (item.type == ItemID.WoodenCrate)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<LumpyRoot>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Aegis>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Handgun>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Skatergun>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SmoothFox>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Inconceivable>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Incite>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<OutOfBubblegum>(), 10, 1, 1));
        }

        if (item.type == ItemID.IronCrate)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlushRifle>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Snider>(), 10, 1, 1));
        }

        if (item.type == ItemID.GoldenCrate)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ThreeWayHulk>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CloudOfLead>(), 10, 1, 1));
        }

        if (item.type == ItemID.LavaCrate || item.type == ItemID.LavaCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<OrphanMaker>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ScorpioTurret>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Onslaught>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Reaper>(), 10, 1, 1));
        }

        if (item.type == ItemID.JungleFishingCrate || item.type == ItemID.JungleFishingCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Revenant>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InspiringTransaction>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AgilityRelic>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PackTactics>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Impact>(), 10, 1, 1));
        }

        if (item.type == ItemID.FrozenCrate || item.type == ItemID.FrozenCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<TooScoops>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<NightSniper>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Carbine>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ViolentSpeed>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fleet>(), 10, 1, 1));
        }

        if (item.type == ItemID.FloatingIslandFishingCrate || item.type == ItemID.FloatingIslandFishingCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<OlPainful>(), 10, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Boomacorn>(), 10, 1, 1));
        }

        if (item.type == ItemID.DungeonFishingCrate || item.type == ItemID.DungeonFishingCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<NightHawkin>(), 10, 1, 1));
        }

        if (item.type == ItemID.OasisCrate || item.type == ItemID.OasisCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bane>(), 10, 1, 1));
        }

        if (item.type == ItemID.OasisCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pimpernel>(), 30, 1, 1));
        }

        if (item.type == ItemID.OceanCrate || item.type == ItemID.OceanCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Lascaux>(), 10, 1, 1));
        }

        if (item.type == ItemID.WoodenCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Redistribution>(), 10, 1, 1));
        }

        if (item.type == ItemID.IronCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Innervate>(), 10, 1, 1));
        }

        if (item.type == ItemID.GoldenCrateHard)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesperateMeasures>(), 10, 1, 1));
        }
    }
}