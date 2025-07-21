using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Placeables.VendingMachines
{
    public class ZedVendingMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(40, 40);

            Item.useTime = 15;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.autoReuse = true;
            Item.useTurn = true;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>();
            Item.placeStyle = 0;

            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddIngredient(ItemID.LifeCrystal, 2)
                .AddTile(ItemID.WorkBench)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 5)
                .AddIngredient(ItemID.LifeCrystal, 2)
                .AddTile(ItemID.WorkBench)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Used to craft unique shields"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Next time you're bleedin' to death, just think: Dr. Zed!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}