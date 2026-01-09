using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Killer : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 80);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusCrit = Utilities.DisplaySkillBonusText(55f, 0.05f);
            int bonusFireRate = Utilities.DisplaySkillBonusText(40f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Projectile Crit Damage and Fire Rate for 7 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusCrit}% Crit Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusFireRate}% Fire Rate");
            Utilities.Text(tooltips, Mod, "Tooltip5", "Found in Rich Mahogany Chests", Utilities.VaultarianColours.Information);
        }
    }
}