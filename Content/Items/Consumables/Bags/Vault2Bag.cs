using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue;

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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Flakker>(), 1, 1, 1));

            // Add Money
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