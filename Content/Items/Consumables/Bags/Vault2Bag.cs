using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue;

namespace Vaultaria.Content.Items.Consumables.Bags
{
    public class Vault2Bag : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.BossBag[Type] = true;
            Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Master;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override bool AltFunctionUse(Player player)
        {
            GetRandomItems(player);

            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Flakker>(), 1, 1, 1));

            // Add Money
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.MoonLordCore));
        }

        private void GetRandomItems(Player player)
        {
            int itemIndex = Main.rand.Next(0, ItemID.Count); // Picks a random index from 0 to the end of the array
            Item item = new Item(itemIndex); // Get whatever item is at that index

            if(item.rare >= ItemRarityID.Pink || item.rare <= ItemRarityID.Quest)
            {
                player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type); // Spawn the item at the player
            }
            else
            {
                GetRandomItems(player); // If the rarity was too high, then try again
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Right Click to open");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Right Click in your inventory to get a random Vaultarian item");
            Utilities.Text(tooltips, Mod, "Tooltip3", "Right Click while holding the bag to get a random Terrarian item that has a rarity of Pink - Master");
        }
    }
}