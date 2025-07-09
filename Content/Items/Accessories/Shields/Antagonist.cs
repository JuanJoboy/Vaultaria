using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Antagonist : ModShield
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Deflects enemy bullets, sending them flying toward nearby enemies.")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Launches Slag homing balls at attackers")
            {
                OverrideColor = new Color(142, 94, 235) // Purple
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "I'm rubber, you're glue.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 60;
            player.statDefense += 5;
            player.lifeRegen += 3;
        }
    }
}