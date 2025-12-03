using Microsoft.VisualBasic;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Tiles.VendingMachines;

namespace Vaultaria.Common.Systems.GenPasses
{
    public class AdditionalRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            base.AddRecipes();

            Recipe.Create(ItemID.Heart, 1)
            .AddIngredient(ItemID.GoldCoin, 10) 
            .AddTile(ModContent.TileType<ZedVendingMachine>()) 
            .Register();
        }
    }   
}