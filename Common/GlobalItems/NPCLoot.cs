using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Accessories.Relics;
using Vaultaria.Content.Items.Accessories.Shields;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ranged.Effervescent.Launcher.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Epic;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Legendary;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.AssaultRifle.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Launcher.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Maliwan;

namespace Vaultaria.Common.GlobalItems
{
    public class NPCLoot : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC mob, Terraria.ModLoader.NPCLoot npcLoot)
        {
            int npc = mob.type;
            int eridium = ModContent.ItemType<Eridium>();

            //********************************** NPC's **********************************//
            if (npc == NPCID.GiantTortoise || npc == NPCID.IceTortoise || npc == NPCID.Derpling)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OttoIdol>(), 50, 1, 1));
            }

            //********************************** Bosses *********************************//
            if (npc == NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Hornet>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 1, 3));
            }

            if (npc == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Law>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 6));
            }

            if (npc == NPCID.TheGroom || npc == NPCID.TheBride || npc == NPCID.BloodZombie || npc == NPCID.Drippler)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BasicGrenade>(), 20, 30, 60));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 2, 5));
            }

            if (npc == NPCID.EaterofWorldsHead || npc == NPCID.BrainofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CloudKill>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 6));
            }

            if (npc == NPCID.QueenBee)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlameOfTheFirehawk>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 6));
            }
        
            if (npc == NPCID.Deerclops)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<UnkemptHarold>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 6));
            }

            if (npc == NPCID.SkeletronHead)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Hail>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Impaler>(), 3, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 10, 15));
            }

            if (npc == NPCID.GoblinShark || npc == NPCID.BloodEelHead || npc == NPCID.BloodNautilus || npc == NPCID.BloodSquid)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Badaboom>(), 12, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.QueenSlimeBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Florentine>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 15, 18));
            }

            if (npc == NPCID.TheDestroyer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LeadStorm>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 18, 25));
            }

            if (npc == NPCID.Retinazer || npc == NPCID.Spazmatism)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fastball>(), 5, 200, 600));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 18, 25));
            }

            if (npc == NPCID.SkeletronPrime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TheBee>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 18, 25));
            }

            if (npc == NPCID.Mothron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sham>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.PirateShip || npc == NPCID.PirateCaptain)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Nukem>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.Everscream)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Zim>(), 25, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 5));
            }

            if (npc == NPCID.IceQueen)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Hive>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.Pumpking)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WorldBurn>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.MourningWood)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Sawbar>(), 25, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 5));
            }

            if (npc == NPCID.MartianSaucer)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CommanderPlanetoid>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.DD2DarkMageT3)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagicMissileRare>(), 5, 300, 600));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagicMissileEpic>(), 10, 400, 700));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 5));
            }

            if (npc == NPCID.DD2OgreT3)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Orc>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Ogre>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 3, 5));
            }

            if (npc == NPCID.DD2Betsy)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BreathOfTerramorphous>(), 4, 200, 250));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GrogNozzle>(), 3, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 6, 10));
            }

            if (npc == NPCID.Plantera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Fibber>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 20, 30));
            }

            if (npc == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<LuckCannon>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 10, 15));
            }
    
            if (npc == NPCID.DukeFishron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DeathRattle>(), 10, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 20, 30));
            }

            if (npc == NPCID.HallowBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Conditions.EmpressOfLightIsGenuinelyEnraged(), ModContent.ItemType<Norfleet>(), 1, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 20, 30));
            }

            if (npc == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Antagonist>(), 5, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 30, 35));
            }

            if (npc == NPCID.MoonLordCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HideOfTerramorphous>(), 20, 1, 1));
                npcLoot.Add(ItemDropRule.Common(eridium, 1, 25, 40));
            }
        }

        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            base.ModifyGlobalLoot(globalLoot);
        }
    }
}