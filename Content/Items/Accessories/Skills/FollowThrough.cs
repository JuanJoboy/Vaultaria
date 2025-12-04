using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class FollowThrough : ModSkill
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
           int numberOfBossesDefeated = Utilities.DownedBossCounter();

            float baseDamage = 0.15f;
            float baseSpeed = 0.1f;

            int bonusDamage = (int) (100 * ((numberOfBossesDefeated / 30f) + baseDamage));
            int bonusSpeed = (int) (100 * ((numberOfBossesDefeated / 20f) + baseSpeed));

            Utilities.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Damage and Movement Speed for 7 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusDamage}% Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusSpeed}% Movement Speed");
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
    }
}