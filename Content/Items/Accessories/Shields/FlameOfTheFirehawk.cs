using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.Utilities;
using Vaultaria.Content.Prefixes.Shields;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class FlameOfTheFirehawk : ModShield
    {
        // Static fields for cooldown and tracking health state across ticks.
        // Static means these values are shared across all instances of this item, which is appropriate for player-specific effects.
        private static int novaCooldown = 0; // Cooldown in ticks

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;

            Main.buffNoTimeDisplay[BuffID.ObsidianSkin] = true;
            Main.buffNoTimeDisplay[BuffID.Inferno] = true;
            Main.buffNoTimeDisplay[BuffID.Warmth] = true;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Grants immunity to Incendiary damage")
            {
                OverrideColor = new Color(231, 92, 22) // Orange
            });
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Continually releases Fire Nova blasts that deals 75 damage when under 30% health")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "From the ashes she will rise.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 40;
            player.statDefense += 4;
            player.lifeRegen += 2;

            player.AddBuff(1, 60); // Obsidian Skin
            player.AddBuff(116, 60); // Inferno
            player.AddBuff(124, 60); // Warmth
            player.buffImmune[ModContent.BuffType<IncendiaryBuff>()] = true;

            // 1. Decrement the cooldown timer each tick
            if (novaCooldown > 0)
            {
                novaCooldown--;
            }

            // 2. Check the condition for triggering the nova
            //    - Player's health is below or equal to 30% of max health
            //    - The cooldown timer has reached 0 (or less)
            if (player.statLife <= (player.statLifeMax2 * 0.3f) && novaCooldown <= 0)
            {
                // 3. If conditions are met, spawn the nova
                int novaDamage = (int)player.GetTotalDamage(DamageClass.Generic).ApplyTo(100);
                float novaKnockback = 5f;
                int novaType = ProjectileID.DD2ExplosiveTrapT3Explosion; // Using vanilla explosion projectile

                // Spawn the nova projectile
                // Projectile.NewProjectile
                Projectile.NewProjectile(
                    player.GetSource_Accessory(Item), // Source: This accessory
                    player.Center,                     // Spawn at player's center
                    Vector2.Zero,                      // Nova explosions usually have no initial velocity
                    novaType,                          // The projectile type (explosion)
                    novaDamage,                        // Damage of the nova
                    novaKnockback,                     // Knockback of the nova
                    player.whoAmI                      // Owner is the player
                );

                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);

                // 4. Reset the cooldown timer after spawning the nova
                novaCooldown = 60; // 120 ticks = 2 seconds
            }
        }
    }
}