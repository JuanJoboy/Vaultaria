using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Redistribution : ModSkill
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Scoring a Critical Hit with a Ranged weapon grants Health and Ammo Regeneration for that weapon for 3 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip2", $"+1% Health Regen");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"Ammo Regen is granted on every shot while the buff is active");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(10)
                .AddIngredient(ItemID.SilverBar, 12)
                .AddIngredient(ItemID.Bomb, 100)
                .AddIngredient(ItemID.Grenade, 100)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient<Eridium>(10)
                .AddIngredient(ItemID.TungstenBar, 12)
                .AddIngredient(ItemID.Bomb, 100)
                .AddIngredient(ItemID.Grenade, 100)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    
        private float CloudOfLeadCounter()
        {
            float numberOfBossesDefeated = Utilities.DownedBossCounter();

            if(numberOfBossesDefeated > 25)
            {
                return 4;
            }
            else if(numberOfBossesDefeated > 19)
            {
                return 5;
            }
            else if(numberOfBossesDefeated > 13)
            {
                return 6;
            }
            else if(numberOfBossesDefeated > 7)
            {
                return 7;
            }
            else if(numberOfBossesDefeated > 1)
            {
                return 8;
            }
            else
            {
                return 9;
            }
        }
    }
}