using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.AccessoryEffects;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class DeathRattle : ModRelic
    {
        private int cooldown = 60 * 35;
        private int usage = 1;

        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+40 HP\n+5 Defense"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Every 30 seconds, for 10 seconds, if you are under 20% health, take damage to regain full health and the following bonuses:\n\t+30% Gun Damage\n\t+30% Fire Rate")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "I always hated you the most.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 40;
            player.statDefense += 5;

            if (cooldown > 0 && usage == 1)
            {
                cooldown--;
                usage = 1;
            }

            if (cooldown <= 0)
            {
                usage = 0;
            }

            if (usage == 0)
            {
                SoundEngine.PlaySound(SoundID.Item176);
                player.AddBuff(ModContent.BuffType<DeathEffect>(), 600);
                cooldown = 60 * 35;
                usage = 1;
            }
        }
    }
}