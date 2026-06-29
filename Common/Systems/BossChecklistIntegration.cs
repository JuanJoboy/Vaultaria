using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Systems;
using Terraria.Localization;
using Vaultaria.Content.Items.Placeables.Vaults;
using Vaultaria.Content.Items.Consumables.Bags;
using Vaultaria.Content.Items.Weapons.Magic;
using Vaultaria.Content.Items.Weapons.Ranged.Eridian;
using Vaultaria.Content.Items.Materials; // Required for LocalizedText

namespace Vaultaria.Common.Systems
{
    public class BossChecklistIntegration : ModSystem
    {
        public override void PostSetupContent()
        {
            if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
            {
                // Registering the Warrior Vault
                RegisterVault(bossChecklist, "Warrior", () => VaultMonsterSystem.vaultSkeletron, 7.1f, "Underworld", NPCID.SkeletronHead, ModContent.ItemType<VaultKey1>(), ModContent.ItemType<Vault1Bag>(), [ModContent.ItemType<WarriorsTail>()]);
                
                // Registering the Destroyer Vault
                RegisterVault(bossChecklist, "Destroyer", () => VaultMonsterSystem.vaultMoonLord, 18.1f, "Tundra", NPCID.MoonLordHead, ModContent.ItemType<VaultKey2>(), ModContent.ItemType<Vault2Bag>(), [ModContent.ItemType<DestroyersEye>(), ModContent.ItemType<EridianFabricator>(), ModContent.ItemType<Moonstone>()]);
            }
        }

        private void RegisterVault(Mod bossChecklist, string borderlandsVaultMonster, System.Func<bool> downedVaultBoss, float progression, string biome, int bossHead, int key, int bag, List<int> items)
        {
            bossChecklist.Call(
                "LogEvent", 
                Mod,
                "VaultOfThe" + borderlandsVaultMonster,
                progression,
                downedVaultBoss,
                bossHead,
                new Dictionary<string, object>()
                {
                    // Use Language.GetText to handle localization correctly
                    ["displayName"] = Language.GetText($"Mods.Vaultaria.BossChecklistIntegration.Vault{borderlandsVaultMonster}DisplayName"),
                    ["spawnInfo"] = Language.GetText($"Mods.Vaultaria.BossChecklistIntegration.Vault{borderlandsVaultMonster}SpawnInfo").WithFormatArgs(biome, borderlandsVaultMonster, $"[i:{key}]"),
                    ["spawnItems"] = key,
                    ["treasureBag"] = bag,
                }
            );
        }
    }
}