using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue;

namespace Vaultaria.Content.Items.Consumables.Bags
{
    // The icon on the bag is 15x15 pixels
    public abstract class VaultBag : ModItem
    {
        internal string? MinRarity { get; set; }
        internal string? MaxRarity { get; set; }

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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Right Click to open");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Right Click in your inventory to get a random Vaultarian item");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"Right Click while holding the bag to get a random Terrarian item that has a rarity of {MinRarity} - {MaxRarity}");
        }

        public abstract bool ItemRarityIsValid(Item item);

        internal Item FindRandomItemInList()
        {
            int itemIndex = Main.rand.Next(0, ItemID.Count); // Picks a random index from 0 to the end of the array
            Item item = new Item(itemIndex); // Get whatever item is at that index

            return item;
        }

        internal void GetRandomItems(Player player)
        {
            Item item = FindRandomItemInList();

            if(ItemRarityIsValid(item))
            {
                SpawnAllItems(player, item);

                if(player.HeldItem.type == ModContent.ItemType<Vault1Bag>() || player.HeldItem.type == ModContent.ItemType<Vault2Bag>())
                {
                    player.HeldItem.stack--;
                }
            }
            else
            {
                GetRandomItems(player); // If the rarity was too high, then try again
            }
        }

        internal void SpawnAllItems(Player player, Item item)
        {
            player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type, 1);

            // If it's a specific item, then drop more of it, but reduce it by 1 cause it's already being spawned above
            SpawnItem2(player, item, 299, "Block");
            SpawnItem2(player, item, 299, "Brick");
            SpawnItem2(player, item, 299, "Wall");
            SpawnItem(player, item, 9, "Potion");
            SpawnItem2(player, item, 499, "Bullet");
            SpawnItem2(player, item, 19, "Dye");
            SpawnItem(player, item, 2, "Crate");
            SpawnItem2(player, item, 99, "Ore");
            SpawnItem2(player, item, 24, "Bar");
            SpawnItem2(player, item, 499, "Arrow");
            SpawnItem2(player, item, 49, "Flare");
            SpawnItem2(player, item, 99, "Dart");
        }

        internal void SpawnItem(Player player, Item item, int stack = 1, string condition = "12345678910")
        {
            if(item != null)
            {
                if(item.Name.Contains(condition))
                {
                    player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type, stack);
                }
            }
        }

        internal void SpawnItem2(Player player, Item item, int stack = 1, string condition = "12345678910")
        {
            if(item != null)
            {
                if(item.Name.Length > condition.Length && item.Name[^condition.Length..] == condition) // Gets the last x letters of the name 
                {
                    player.QuickSpawnItem(player.GetSource_DropAsItem(), item.type, stack);
                }
            }
        }
    }
}