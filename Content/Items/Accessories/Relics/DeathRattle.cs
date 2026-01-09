using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.AccessoryEffects;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class DeathRattle : ModRelic
    {
        private int cooldown = 60 * 35;
        public int usage = 1;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 29);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+40 HP\n+5 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Every 30 seconds, for 10 seconds, get the following bonuses:\n\t+20% Damage\n\t+20% Fire Rate\nAnd if you are under 20% health, take damage to regain full health", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "I always hated you the most.");
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