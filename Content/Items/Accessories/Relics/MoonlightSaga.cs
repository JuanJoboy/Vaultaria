using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class MoonlightSaga : ModRelic
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+20 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "High health regen and life steal when shooting enemies in space", Utilities.VaultarianColours.Healing);
            Utilities.RedText(tooltips, Mod, "You give me everything just by breathing.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if(player.ZoneSkyHeight)
            {
                player.lifeRegen += 40;
            }

            player.statLifeMax2 += 20;
            player.statDefense += 2;
        }
    }
}