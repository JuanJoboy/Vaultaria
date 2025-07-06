using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Tiles.VendingMachines;

namespace Vaultaria.Content.Items.Placeables.VendingMachines
{
    public class ZedVendingMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Sets the width of the item's sprite when it's in the inventory or on the ground.
            // This does NOT affect the placed tile's size.
            Item.width = 40;
            // Sets the height of the item's sprite when it's in the inventory or on the ground.
            Item.height = 40;

            // Determines how quickly the player can use the item. A lower number means faster use.
            // This specifically affects the delay between successive uses.
            Item.useTime = 15;
            // Determines how long the item's "use animation" plays when the item is used.
            // This affects how long the player holds out the item.
            Item.useAnimation = 20;

            Item.useStyle = ItemUseStyleID.Swing;

            Item.autoReuse = true;
            Item.useTurn = true;

            Item.maxStack = 9999;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Content.Items.Tiles.VendingMachines.ZedVendingMachine>();
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