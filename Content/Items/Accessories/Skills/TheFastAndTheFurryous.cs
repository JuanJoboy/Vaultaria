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
    public class TheFastAndTheFurryous : ModSkill
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(copper: 0);
            Item.rare = ItemRarityID.Green;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusWhip = Utilities.DisplaySkillBonusText(150f, 0.05f);
            int bonusSummon = Utilities.DisplaySkillBonusText(120f, 0.05f);
            int bonusSpeed = Utilities.DisplaySkillBonusText(170f, 0.025f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "While above 50% Health, you gain increased Whip Damage, Summon Damage, and Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusWhip}% Whip Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusSummon}% Summon Damage");
            Utilities.Text(tooltips, Mod, "Tooltip5", $"+{bonusSpeed}% Movement Speed");
        }
    }
}