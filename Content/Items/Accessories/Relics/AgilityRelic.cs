using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class AgilityRelic : ModRelic
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(38, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+10 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "+31% Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip3", "+22% Jump Height");
            Utilities.Text(tooltips, Mod, "Tooltip4", "Found in Rich Mahogany Chests", Utilities.VaultarianColours.Information);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 10;
            player.statDefense += 2;

            player.jumpSpeedBoost += 1.27f;
            player.runAcceleration += 0.35f;
            player.accRunSpeed += 1.31f;
            player.maxRunSpeed += 1.31f;
        }
    }
}