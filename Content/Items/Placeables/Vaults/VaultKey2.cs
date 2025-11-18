using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Placeables.Vaults
{
    public class VaultKey2 : ModItem
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

            Item.maxStack = 1;

            Item.value = Item.buyPrice(gold: 1);

            // Sets the rarity of the item, which affects its name color in the inventory.
            // ItemRarityID.Blue corresponds to the default blue rarity color.
            Item.rare = ItemRarityID.Master;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(40)
                .AddIngredient<VaultFragment4>(1)
                .AddIngredient<VaultFragment5>(1)
                .AddIngredient<VaultFragment6>(1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}