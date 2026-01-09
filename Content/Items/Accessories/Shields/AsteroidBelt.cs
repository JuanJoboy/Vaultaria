using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Common.Utilities;
using Terraria.GameContent.Biomes.CaveHouse;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class AsteroidBelt : ModShield
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(46, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 90);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+20 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Launch homing Explosive Meteors when damaged", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "Straight from the bug homeworld.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(30)
                .AddIngredient(ItemID.PlatinumBar, 8)
                .AddIngredient(ItemID.MeteoriteBar, 16)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient<Eridium>(30)
                .AddIngredient(ItemID.GoldBar, 8)
                .AddIngredient(ItemID.MeteoriteBar, 16)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}