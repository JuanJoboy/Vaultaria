using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class HideOfTerramorphous : ModShield
    {
        static int usage = 0;

        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "When under 65% health, melee attacks do 80% bonus damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Deals 100% bonus thorn damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip3", "Releases a Fire Nova blast that deals 100 damage when health dips under 30%")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "...His hide turned the mightiest tame...")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 150;
            player.statDefense += 10;
            player.lifeRegen += 4;

            // Melee
            if (player.statLife <= (player.statLifeMax2 * 0.65f))
            {
                // Increases Melee damage by 80%
                player.GetDamage(DamageClass.Melee) += 0.8f;
            }

            // Spike
            player.thorns = 1f; // 100% thorn damage

            // Fire Nova
            if (player.statLife <= (player.statLifeMax2 * 0.3f) && usage == 1)
            {
                usage = 0;
                int novaDamage = (int)player.GetTotalDamage(DamageClass.Generic).ApplyTo(100);
                float novaKnockback = 5f;
                int novaType = ProjectileID.DD2ExplosiveTrapT3Explosion;

                Projectile.NewProjectile(
                    player.GetSource_Accessory(Item),
                    player.Center,
                    Vector2.Zero,
                    novaType,
                    novaDamage,
                    novaKnockback,
                    player.whoAmI
                );

                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
            }

            if (player.statLife > (player.statLifeMax2 * 0.3f))
            {
                usage = 1;
            }
        }
    }
}