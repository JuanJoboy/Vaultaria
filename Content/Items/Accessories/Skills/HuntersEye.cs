using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class HuntersEye : ModSkill
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
            int bonusCrit = Utilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusReduction = Utilities.DisplaySkillBonusText(150f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "You get different Bonuses when fighting different enemy types");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"Mobs: +{bonusCrit}% Summon / Whip Crit Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"Bosses: +{bonusReduction}% Damage Reduction");
        }
    }
}