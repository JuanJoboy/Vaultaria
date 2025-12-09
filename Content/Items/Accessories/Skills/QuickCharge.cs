using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class QuickCharge : ModSkill
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
            int bonusRegen = (int) (Main.LocalPlayer.statLifeMax2 * 0.01f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Killing an enemy grants you Health Regeneration for 7 seconds", Utilities.VaultarianColours.Healing);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusRegen}% Health Regeneration");
        }
    }
}