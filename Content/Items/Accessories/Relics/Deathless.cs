using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class Deathless : ModRelic
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
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Health is reduced but damage is doubled")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "What do we say to the God of Death?")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 = 10;
            player.statDefense += 150;
            player.GetDamage<MagicDamageClass>() += 1;
            player.GetDamage<MeleeDamageClass>() += 1;
            player.GetDamage<RangedDamageClass>() += 1;
            player.GetDamage<SummonDamageClass>() += 1;
            player.moveSpeed += 1;
        }
    }
}