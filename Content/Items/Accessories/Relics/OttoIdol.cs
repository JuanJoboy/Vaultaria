using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class OttoIdol : ModRelic
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(38, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+60 HP\n+5 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Get 10% health back on every kill", Utilities.VaultarianColours.Healing);
            Utilities.RedText(tooltips, Mod, "Every man for himself.");
            Utilities.CursedText(tooltips, Mod, "Curse of the Sudden-er Death!\n(-1 HP/s)");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 60;
            player.statDefense += 5;
            player.lifeRegen += -20;
        }
    }
}