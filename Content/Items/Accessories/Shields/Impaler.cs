using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Impaler : ModShield
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(46, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+25 HP\n+3 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Launches Corrosive homing spikes when damaged with a projectile", Utilities.VaultarianColours.Corrosive);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Deals Corrosive Thorn Damage to melee attackers", Utilities.VaultarianColours.Corrosive);
            Utilities.RedText(tooltips, Mod, "Vlad would be proud");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 25;
            player.statDefense += 3;
        }
    }
}