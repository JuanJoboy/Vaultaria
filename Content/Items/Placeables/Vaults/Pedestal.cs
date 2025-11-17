using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Placeables.Vaults
{
    public class Pedestal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Sets the width and height of the item's sprite when it's in the inventory or on the ground.
            // This does NOT affect the placed tile's size.
            Item.Size = new Vector2(40, 40);
            Item.useTime = 15;
            Item.useAnimation = 20;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.autoReuse = true;
            Item.useTurn = true;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Tiles.Vaults.PedestalTile>();

            // Specifies which "style" of the tile to place. Useful for multi-frame tiles
            // or tiles with different visual variations. 0 means the first (default) style.
            Item.placeStyle = 0;

            Item.value = Item.buyPrice(gold: 1);

            // Sets the rarity of the item, which affects its name color in the inventory.
            // ItemRarityID.Blue corresponds to the default blue rarity color.
            Item.rare = ItemRarityID.Blue;
        }
    }
}