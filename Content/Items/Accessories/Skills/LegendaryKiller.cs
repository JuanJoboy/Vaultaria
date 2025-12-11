using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class LegendaryKiller : ModSkill
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
            int bonusCrit = Utilities.DisplaySkillBonusText(150f, 0.05f) + Utilities.DisplaySkillBonusText(80f, 0.05f);
            int bonusDamage = Utilities.DisplaySkillBonusText(200f, 0.05f);
            int bonusProjectileSpeed = Utilities.DisplaySkillBonusText(15f, 0.1f);
            int bonusKillCrit = Utilities.DisplaySkillBonusText(55f, 0.05f);
            int bonusFireRate = Utilities.DisplaySkillBonusText(40f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            Utilities.Text(tooltips, Mod, "Tooltip1", "Increases your Projectile's Damage, Crit Damage and Speed");
            Utilities.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Crit Damage and Fire Rate for 7 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusCrit}% Crit Damage");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusDamage}% Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusProjectileSpeed}% Projectile Speed");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusKillCrit}% Crit Damage on kill");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusFireRate}% Fire Rate on kill");
            Utilities.RedText(tooltips, Mod, "Well, that escalated quickly.");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient<Moonstone>(1)
                .AddIngredient<Headshot>(1)
                .AddIngredient<Velocity>(1)
                .AddIngredient<Killer>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}