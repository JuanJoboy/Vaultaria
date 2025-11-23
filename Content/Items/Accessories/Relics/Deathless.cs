using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class Deathless : ModRelic
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(32, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+20 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Health is reduced but damage is doubled", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "What do we say to the God of Death?");
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