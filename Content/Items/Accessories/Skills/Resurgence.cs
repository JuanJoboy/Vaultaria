using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Resurgence : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(copper: 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusHealth = Utilities.DisplaySkillBonusText(300f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Killing an enemy with Melee Damage restores Health", Utilities.VaultarianColours.Healing);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusHealth}% Health Gained");
        }
    }
}