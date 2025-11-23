using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Sham : ModShield
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(48, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+30 HP\n+2 Defense\nRegenerates health");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Has a 94% chance to absorb any Projectile", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Wow, I CAN do this all day.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 30;
            player.statDefense += 2;
            player.lifeRegen += 2;
        }
    }
}