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
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            float bonusDamage = Utilities.DisplaySkillBonusText(60f, 0.05f);
            float bonusSpeed = Utilities.DisplaySkillBonusText(42f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Killing an enemy increases your Damage and Movement Speed for 7 seconds");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusDamage}% Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusSpeed}% Movement Speed");
        }
    }
}