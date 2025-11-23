using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class VaultHuntersRelic : ModRelic
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 32);
            Item.accessory = true;
            Item.value = Item.buyPrice(copper: 0);
            Item.rare = ItemRarityID.White;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+5 HP\n+1 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "+25% Luck", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Courtesy of being a Premiere Club member.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 5;
            player.statDefense += 1;
            player.luck += 1f;
        }
    }
}