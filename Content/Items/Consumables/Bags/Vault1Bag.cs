using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue;

namespace Vaultaria.Content.Items.Consumables.Bags
{
    public class Vault1Bag : VaultBag
    {
        public Vault1Bag()
        {
            MinRarity = "Blue";
            MaxRarity = "Light Red";
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Flakker>(), 1, 1, 1));
            itemLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 1, 20, 20));

            // Add Money
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.WallofFlesh));
        }

        public override bool ItemRarityIsValid(Item item)
        {
            if(item.rare >= ItemRarityID.Blue && item.rare <= ItemRarityID.LightRed)
            {
                return true;
            }

            return false;
        }
    }
}