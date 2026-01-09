using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Aequitas : ModShield
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(47, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+15 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Has a 50% chance to absorb any Projectile", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "The second is better.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 15;
            player.statDefense += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(20)
                .AddIngredient(ItemID.DemoniteBar, 12)
                .AddIngredient(ItemID.Cobweb, 50)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient<Eridium>(20)
                .AddIngredient(ItemID.CrimtaneBar, 12)
                .AddIngredient(ItemID.Cobweb, 50)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}