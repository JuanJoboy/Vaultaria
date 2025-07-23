using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Terraria.GameContent.Biomes.CaveHouse;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class AsteroidBelt : ModShield
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 90);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+20 HP\n+2 Defense"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Launch homing Explosive Meteors when damaged")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Straight from the bug homeworld.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
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