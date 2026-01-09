using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class TheBee : ModShield
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(59, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 3);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+25 HP\n+4 Defense\nRegenerates health");
            Utilities.Text(tooltips, Mod, "Tooltip2", "When at max health, non-melee attacks deals 25% bonus Amp Damage", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Float like a butterfly...");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 25;
            player.statDefense += 4;
            player.lifeRegen += 2;
        }

        public override void UpdateEquip(Player player)
        {
            if (player.statLife >= player.statLifeMax2 - 10)
            {
                // Increases Ranged, Mage & Summoner damage by 25%
                player.GetDamage(DamageClass.Ranged) *= 1.25f;
                player.GetDamage(DamageClass.Magic) *= 1.25f;
                player.GetDamage(DamageClass.Summon) *= 1.25f;
            }
        }
    }
}