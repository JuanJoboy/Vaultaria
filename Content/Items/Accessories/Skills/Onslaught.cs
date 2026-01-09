using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Onslaught : ModSkill
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
            int bonusDamage = Utilities.DisplaySkillBonusText(50f, 0.05f);
            int bonusSpeed = Utilities.DisplaySkillBonusText(30f, 0.1f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Ranged Damage and Movement Speed for 7 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusDamage}% Ranged Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusSpeed}% Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip5", "Found in Locked Shadow Chests", Utilities.VaultarianColours.Information);
        }
    }
}