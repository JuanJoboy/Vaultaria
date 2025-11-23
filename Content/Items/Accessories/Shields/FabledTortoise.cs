using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class FabledTortoise : ModShield
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+320 HP\n+5 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "While above 50% HP, movement speed is reduced by 80%\nBut once below 50% HP, movement speed is increased by 50%", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Win by a hare.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 320;
            player.statDefense += 5;

            if(player.statLife > player.statLifeMax2 / 2)
            {
                player.moveSpeed *= 0.2f;
                player.runAcceleration *= 0.2f;
                player.accRunSpeed *= 0.2f;
                player.maxRunSpeed *= 0.2f;
                player.wingRunAccelerationMult *= 0.2f;
                player.wingAccRunSpeed *= 0.2f;
            }
            else
            {
                player.moveSpeed += 0.5f;
                player.maxRunSpeed += 0.5f;
            }
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