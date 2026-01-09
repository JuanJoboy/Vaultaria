using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Antagonist : ModShield
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(46, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+60 HP\n+5 Defense\nRegenerates health\n+50% Deflection Chance\n+880% Deflected Bullet Damage\n+50% Damage Reduction");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Deflects enemy bullets, sending them flying toward nearby enemies.", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Launches Slag homing balls at attackers", Utilities.VaultarianColours.Slag);
            Utilities.RedText(tooltips, Mod, "I'm rubber, you're glue.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 60;
            player.statDefense += 5;
            player.lifeRegen += 2;
        }
    }
}