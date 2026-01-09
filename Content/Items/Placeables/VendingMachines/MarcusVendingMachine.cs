using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Placeables.VendingMachines
{
    public class MarcusVendingMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Sets the width and height of the item's sprite when it's in the inventory or on the ground.
            // This does NOT affect the placed tile's size.
            Item.Size = new Vector2(40, 40);

            // Determines how quickly the player can use the item. A lower number means faster use.
            // This specifically affects the delay between successive uses.
            Item.useTime = 15;
            // Determines how long the item's "use animation" plays when the item is used.
            // This affects how long the player holds out the item.
            Item.useAnimation = 20;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.autoReuse = true;
            Item.useTurn = true;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>();

            // Specifies which "style" of the tile to place. Useful for multi-frame tiles
            // or tiles with different visual variations. 0 means the first (default) style.
            Item.placeStyle = 0;

            Item.value = Item.buyPrice(gold: 1);

            // Sets the rarity of the item, which affects its name color in the inventory.
            // ItemRarityID.Blue corresponds to the default blue rarity color.
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddIngredient(ItemID.WoodenArrow, 100)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 5)
                .AddIngredient(ItemID.WoodenArrow, 100)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Used to craft one of a kind weaponry");
            Utilities.RedText(tooltips, Mod, "No refunds.");
        }
    }
}