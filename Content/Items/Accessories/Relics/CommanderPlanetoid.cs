using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class CommanderPlanetoid : ModRelic
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Expert;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+20 HP\n+2 Defense"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Melee strikes deal an additional 25% Elemental Damage\nThe element randomizes every hit")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "The power is YOURS!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 2;
        }
    }
}