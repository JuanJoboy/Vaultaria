using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class LegendarySiren : ModSkill
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
            int bonusDamage = Utilities.DisplaySkillBonusText(100f, 0.05f);
            int bonusProjectileSpeed = Utilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusFireRate = Utilities.DisplaySkillBonusText(45f, 0.05f);
            int bonusPhaselockDamage = Utilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusReuseDelay = Utilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusReaperDamage = Utilities.DisplaySkillBonusText(35f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Increases your Magic Damage and Projectile Speed");
            Utilities.Text(tooltips, Mod, "Tooltip3", "Increases your Magic Fire Rate");
            Utilities.Text(tooltips, Mod, "Tooltip4", "You deal increased Magic Damage to enemies above 50% Health");
            Utilities.Text(tooltips, Mod, "Tooltip5", "While an enemy is Phaselocked you gain increased Fire Rate and Damage for Magic weapons");
            Utilities.Text(tooltips, Mod, "Tooltip6", $"+{bonusDamage}% Magic Damage");
            Utilities.Text(tooltips, Mod, "Tooltip7", $"+{bonusProjectileSpeed}% Projectile Speed");
            Utilities.Text(tooltips, Mod, "Tooltip8", $"+{bonusReuseDelay}% Fire Rate");
            Utilities.Text(tooltips, Mod, "Tooltip9", $"+{bonusReaperDamage}% Magic Damage while your target is above 50% health");
            Utilities.Text(tooltips, Mod, "Tooltip10", $"+{bonusFireRate}% Fire Rate while Phaselock is active");
            Utilities.Text(tooltips, Mod, "Tooltip11", $"+{bonusPhaselockDamage}% Magic Damage while Phaselock is active");
            Utilities.Text(tooltips, Mod, "Tooltip12", $"Gain +45% Movement Speed while Phaselock is active");
            Utilities.RedText(tooltips, Mod, "(giggles) I'm really good at this!");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.LunarBar, 50)
                .AddIngredient(ItemID.FragmentNebula, 100)
                .AddIngredient<Accelerate>(1)
                .AddIngredient<Wreck>(1)
                .AddIngredient<Foresight>(1)
                .AddIngredient<Reaper>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}