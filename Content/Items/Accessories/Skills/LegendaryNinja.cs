using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class LegendaryNinja : ModSkill
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
            int backstab = Utilities.DisplaySkillBonusText(65f, 0.05f);
            int resurgence = Utilities.DisplaySkillBonusText(300f);
            int killingBlow = Utilities.DisplaySkillBonusText(16.67f, 0.20f);
            int followThroughDamage = Utilities.DisplaySkillBonusText(60f, 0.05f);
            int followThroughSpeed = Utilities.DisplaySkillBonusText(42f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Melee Attacks that hit an enemy from behind, deal increased Melee Damage");
            Utilities.Text(tooltips, Mod, "Tooltip3", "Killing an enemy with Melee Damage restores Health", Utilities.VaultarianColours.Healing);
            Utilities.Text(tooltips, Mod, "Tooltip4", "You deal increased Melee Damage to enemies below 20% Health");
            Utilities.Text(tooltips, Mod, "Tooltip5", "Killing an enemy increases your Damage and Movement Speed for 7 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip7", $"+{backstab}% Backstab Melee Damage");
            Utilities.Text(tooltips, Mod, "Tooltip8", $"+{resurgence}% Health Gained");
            Utilities.Text(tooltips, Mod, "Tooltip9", $"+{killingBlow}% Killing Blow Melee Damage");
            Utilities.Text(tooltips, Mod, "Tooltip10", $"+{followThroughDamage}% Damage on Kill");
            Utilities.Text(tooltips, Mod, "Tooltip11", $"+{followThroughSpeed}% Movement Speed on Kill");
            Utilities.RedText(tooltips, Mod, "In the midst of combat, keep stillness inside of you.");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.LunarBar, 50)
                .AddIngredient(ItemID.FragmentSolar, 100)
                .AddIngredient<Backstab>(1)
                .AddIngredient<Resurgence>(1)
                .AddIngredient<KillingBlow>(1)
                .AddIngredient<FollowThrough>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}