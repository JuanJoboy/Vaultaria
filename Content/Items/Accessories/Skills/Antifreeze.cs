using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Antifreeze : ModSkill
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            float realSpeed = Main.LocalPlayer.velocity.Length();

            int bonusSpeed = Utilities.DisplaySkillBonusText(40f, 0.05f);
            int bonusDamage = (int) (100f * (Utilities.ComparativeBonus(1f, -realSpeed, 25f)) + Utilities.DisplaySkillBonusText(87f, 0.05f));

            Utilities.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Killing an enemy increases your Movement Speed for 8 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip3", "While moving, you gain increased Damage (doesn't apply to Summon Damage). The faster you move, the greater this bonus");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusSpeed}% Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip5", $"Up to +{bonusDamage}% Damage");
            Utilities.RedText(tooltips, Mod, "Jet propulsion, disengage.");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient<SeraphCrystal>(1)
                .AddIngredient(ItemID.SwiftnessPotion, 100)
                .AddIngredient<ViolentSpeed>(1)
                .AddIngredient<ViolentMomentum>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}