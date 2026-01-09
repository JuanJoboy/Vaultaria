using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Buffs.SkillEffects;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Wreck : ModSkill
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 10);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusFireRate = Utilities.DisplaySkillBonusText(45f, 0.05f);
            int bonusDamage = Utilities.DisplaySkillBonusText(80f, 0.05f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "While an enemy is Phaselocked you gain increased Fire Rate and Damage for Magic weapons");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusFireRate}% Fire Rate");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusDamage}% Magic Damage");
        }
    }
}