using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class CommanderPlanetoid : ModRelic
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(31, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+20 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Melee strikes deal an additional 25% Elemental Damage\nThe element randomizes every hit", Utilities.VaultarianColours.Radiation);
            Utilities.RedText(tooltips, Mod, "The power is YOURS!");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 2;
        }
    }
}