using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class PackTactics : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusHealth = Utilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusDamage = Utilities.DisplaySkillBonusText(88f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Increases your Maximum Health and your Summon Damage");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusHealth}% Maximum Health");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusDamage}% Summon Damage");
            Utilities.Text(tooltips, Mod, "Tooltip5", "Found in Rich Mahogany Chests", Utilities.VaultarianColours.Information);
        }
    }
}