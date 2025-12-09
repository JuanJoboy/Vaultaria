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
    public class Fleet : ModSkill
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.Blue;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusSpeed = Utilities.DisplaySkillBonusText(27f, 0.1f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "While under 30% health you gain increased Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bonuses increase as you progress", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusSpeed}% Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip4", "Found in Frozen Chests", Utilities.VaultarianColours.Information);
        }
    }
}