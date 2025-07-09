using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class TheBee : ModShield
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
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "When at max health, non-melee attacks deals 40% bonus Amp Damage")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Float like a butterfly...")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 50;
            player.statDefense += 4;
            player.lifeRegen += 2;

            if (player.statLife == player.statLifeMax2)
            {
                // Increases Ranged, Mage & Summoner damage by 40%
                player.GetDamage(DamageClass.Ranged) += 0.4f;
                player.GetDamage(DamageClass.Magic) += 0.4f;
                player.GetDamage(DamageClass.Summon) += 0.4f;
            }
        }
    }
}