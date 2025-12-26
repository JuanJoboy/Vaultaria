using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Vaultaria.Content.Prefixes.Shields;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class FlameOfTheFirehawk : ModShield
    {
        private int novaCooldown = 0; // Cooldown in ticks

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;

            Main.buffNoTimeDisplay[BuffID.ObsidianSkin] = true;
            Main.buffNoTimeDisplay[BuffID.Inferno] = true;
            Main.buffNoTimeDisplay[BuffID.Warmth] = true;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(45, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Yellow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+20 HP\n+4 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Grants immunity to Incendiary damage", Utilities.VaultarianColours.Incendiary);
            Utilities.Text(tooltips, Mod, "Tooltip3", $"Continually releases Fire Nova blasts that deals {Main.LocalPlayer.statDefense * 2} damage when under 30% health", Utilities.VaultarianColours.Explosive);
            Utilities.Text(tooltips, Mod, "Tooltip4", $"Damage is based on your defense");
            Utilities.RedText(tooltips, Mod, "From the ashes she will rise.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 4;

            player.AddBuff(BuffID.ObsidianSkin, 60); // Obsidian Skin
            player.AddBuff(BuffID.Warmth, 60); // Warmth
            player.buffImmune[ModContent.BuffType<IncendiaryBuff>()] = true;

            // 1. Decrement the cooldown timer each tick
            if (novaCooldown > 0)
            {
                novaCooldown--;
            }

            if(player.whoAmI == Main.myPlayer)
            {
                // 2. Check the condition for triggering the nova
                //    - Player's health is below or equal to 30% of max health
                //    - The cooldown timer has reached 0 (or less)
                if (player.statLife <= (player.statLifeMax2 * 0.3f) && novaCooldown <= 0)
                {
                    player.AddBuff(BuffID.Inferno, 60); // Inferno
                    // 3. If conditions are met, spawn the nova
                    int novaDamage = (int)player.GetTotalDamage(DamageClass.Generic).ApplyTo(player.statDefense * 2);
                    float novaKnockback = 5f;
                    int novaType = ElementalID.IncendiaryNovaProjectile;

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

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<Inflammable>();
        }
    }
}