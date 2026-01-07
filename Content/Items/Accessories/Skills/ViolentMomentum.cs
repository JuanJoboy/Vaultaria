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
    public class ViolentMomentum : ModSkill
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            float realSpeed = Main.LocalPlayer.velocity.Length();

            int bonusDamage = (int) (100f * (Utilities.ComparativeBonus(1f, -realSpeed, 25f)) + Utilities.DisplaySkillBonusText(87f, 0.05f));

            Utilities.Text(tooltips, Mod, "Tooltip1", "While moving, you gain increased Damage. The faster you move, the greater this bonus");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"Up to +{bonusDamage}% Damage");
        }
    }
}