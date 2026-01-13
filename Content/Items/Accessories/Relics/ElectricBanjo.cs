using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class ElectricBanjo : ModRelic
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+20 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Attacks have a chance to chain Shock damage to nearby enemies", Utilities.VaultarianColours.Shock);
            Utilities.RedText(tooltips, Mod, "We never could have foreseen the success.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 2;
        }
    }
}