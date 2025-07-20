using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class StopGap : ModShield
    {
        int usage = 0;

        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "When under 10% health, become immune to all damage for 5 seconds\nMust have full health before immunity can be used again"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "The process is called 'living'")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 40;
            player.statDefense += 3;
            player.lifeRegen += 2;

            if (player.statLife <= player.statLifeMax2 * 0.1 && usage == 0)
            {
                player.immune = true;
                player.immuneTime = 300;
                usage = 1;
            }

            if (player.statLife == player.statLifeMax2)
            {
                usage = 0;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(40)
                .AddIngredient(ItemID.HallowedBar, 40)
                .AddIngredient(ItemID.SoulofLight, 40)
                .AddIngredient(ItemID.CrossNecklace, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}