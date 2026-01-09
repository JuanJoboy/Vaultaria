using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class StarterShield : ModShield
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(35, 34);
            Item.accessory = true;
            Item.value = Item.buyPrice(copper: 90);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+10 HP\n+2 Defense");
            Utilities.RedText(tooltips, Mod, "Your ability to walk short distances without dying\nwill surely be Handsome Jack's downfall!");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 10;
            player.statDefense += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 5)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}