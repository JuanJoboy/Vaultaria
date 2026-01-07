using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Accessories.Skills;

namespace Vaultaria.Content.Items.Consumables.Bags
{
    public class Vault2Bag : VaultBag
    {
        public Vault2Bag()
        {
            MinRarity = "Pink";
            MaxRarity = "Master";
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Items
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemID.FrozenKey, ItemID.JungleKey, ItemID.CrimsonKey, ItemID.CorruptionKey, ItemID.HallowedKey, ItemID.DungeonDesertKey, ItemID.Uzi, ItemID.DiscountCard, ItemID.RodofDiscord, ItemID.CoinGun, ItemID.TheAxe, ItemID.TeleportationPylonVictory, ItemID.DiggingMoleMinecart, ModContent.ItemType<Bore>(), ModContent.ItemType<Bloodsplosion>()));

            // Potions
            itemLoot.Add(ItemDropRule.Common(ItemID.SuperHealingPotion, 10, 10, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.SuperManaPotion, 10, 10, 20));

            // Money
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.MoonLordCore));
        }

        public override bool ItemRarityIsValid(Item item)
        {
            if(item.rare >= ItemRarityID.Pink || item.rare <= ItemRarityID.Quest)
            {
                return true;
            }

            return false;
        }
    }
}