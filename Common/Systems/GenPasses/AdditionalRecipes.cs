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
        public override void PostAddRecipes()
        {
            for(int i = 0; i < Main.item.Length; i++)
            {
                Item item = new Item();
                item.SetDefaults(i);
                
                if (item.ammo == AmmoID.Bullet && item.damage > 0)
                {
                    Recipe.Create(i, 100)
                        .AddIngredient(ItemID.GoldCoin, 20 * item.damage)
                        .AddTile(ModContent.TileType<MarcusVendingMachine>())
                        .Register();
                }
            }
        }
    }   
}