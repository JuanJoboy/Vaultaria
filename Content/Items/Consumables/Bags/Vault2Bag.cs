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
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            GetRandomItems(player);

            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Flakker>(), 1, 1, 1));

            // Add Money
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.MoonLordCore));
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Right Click to open");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Right Click in your inventory to get a random Vaultarian item");
            Utilities.Text(tooltips, Mod, "Tooltip3", "Right Click while holding the bag to get a random Terrarian item that has a rarity of Pink - Master");
        }

        private void GetRandomItems(Player player)
        {
            int itemIndex = Main.rand.Next(0, ItemID.Count); // Picks a random index from 0 to the end of the array
            Item item = new Item(itemIndex); // Get whatever item is at that index

            if(item.rare >= ItemRarityID.Pink || item.rare <= ItemRarityID.Quest)
            {
                SpawnItem(player, item);

                // If it's a specific item, then drop more of it, but reduce it by 1 cause it's already being spawned above
                SpawnItem(player, item, 299, "Block");
                SpawnItem(player, item, 299, "Brick");
                SpawnItem(player, item, 299, "Wall");
                SpawnItem(player, item, 9, "Potion");
                SpawnItem(player, item, 499, "Bullet");
                SpawnItem2(player, item, 99, "Ore");
                SpawnItem2(player, item, 24, "Bar");
                SpawnItem2(player, item, 24, "Arrow");
                SpawnItem2(player, item, 49, "Flare");
                SpawnItem2(player, item, 99, "Dart");
            }
            else
            {
                GetRandomItems(player); // If the rarity was too high, then try again
            }
        }

        private void SpawnItem(Player player, Item item, int stack = 1, string condition = "12345678910")
        {
            if(item != null)
            {
                if(item.Name.Contains(condition))
                {
                    player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type, stack);
                }
                else
                {
                    player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type, stack); // Spawn the item at the player
                }   
            }
        }

        private void SpawnItem2(Player player, Item item, int stack = 1, string condition = "12345678910")
        {
            if(item != null)
            {
                if(item.Name.Length > 3 && item.Name[^condition.Length..] == condition) // Gets the last x letters of the name 
                {
                    player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type, stack);
                }
                else
                {
                    player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type, stack); // Spawn the item at the player
                }   
            }
        }
    }
}