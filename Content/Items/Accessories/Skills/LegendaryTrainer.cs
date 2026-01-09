using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class LegendaryTrainer : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusWhip = Utilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusSummon = Utilities.DisplaySkillBonusText(120f, 0.05f);
            int bonusSpeed = Utilities.DisplaySkillBonusText(170f, 0.025f);
            int bonusHealth = Utilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusDamage = Utilities.DisplaySkillBonusText(88f, 0.05f);
            int bonusCrit = Utilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusReduction = Utilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusDamageBehind = Utilities.DisplaySkillBonusText(60f, 0.05f);
            int number = !Main.hardMode ? 1 : 2; // if not hardmode, then 1, else 2

            Utilities.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Increases your Maximum Health and your Summon Damage");
            Utilities.Text(tooltips, Mod, "Tooltip3", "You get different Bonuses when fighting different enemy types");
            Utilities.Text(tooltips, Mod, "Tooltip4", "While above 50% Health, you gain increased Whip Damage, Summon Damage, and Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip5", "Your Summons deal increased damage to enemies that you are behind");
            Utilities.Text(tooltips, Mod, "Tooltip5", "Your Summons deal increased damage to enemies that you are behind");
            Utilities.Text(tooltips, Mod, "Tooltip6", $"+{bonusHealth}% Maximum Health");
            Utilities.Text(tooltips, Mod, "Tooltip7", $"+{bonusDamage}% Summon Damage");
            Utilities.Text(tooltips, Mod, "Tooltip8", $"Mobs: +{bonusCrit}% Crit Damage");
            Utilities.Text(tooltips, Mod, "Tooltip9", $"Bosses: +{bonusReduction}% Damage Reduction");
            Utilities.Text(tooltips, Mod, "Tooltip10", $"+{bonusWhip}% Whip Damage while above 50% Health");
            Utilities.Text(tooltips, Mod, "Tooltip11", $"+{bonusSummon}% Summon Damage while above 50% Health");
            Utilities.Text(tooltips, Mod, "Tooltip12", $"+{bonusSpeed}% Movement Speed while above 50% Health");
            Utilities.Text(tooltips, Mod, "Tooltip13", $"+{bonusDamageBehind}% Summon Damage while behind your target");
            Utilities.Text(tooltips, Mod, "Tooltip13", $"Increases your max number of minions by {number}");
            Utilities.RedText(tooltips, Mod, "If you can make God bleed, people will cease to believe in them.");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.LunarBar, 50)
                .AddIngredient(ItemID.FragmentStardust, 100)
                .AddIngredient<TheFastAndTheFurryous>(1)
                .AddIngredient<PackTactics>(1)
                .AddIngredient<HuntersEye>(1)
                .AddIngredient<HiddenMachine>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}