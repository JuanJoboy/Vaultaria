using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Impaler : ModShield
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+25 HP\n+3 Defense"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Launches Corrosive homing spikes when damaged with a projectile")
            {
                OverrideColor = new Color(136, 235, 94) // Light Green
            });
            tooltips.Add(new TooltipLine(Mod, "Tooltip3", "Deals Corrosive Thorn Damage to melee attackers")
            {
                OverrideColor = new Color(136, 235, 94) // Light Green
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Vlad would be proud")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 25;
            player.statDefense += 3;
        }
    }
}