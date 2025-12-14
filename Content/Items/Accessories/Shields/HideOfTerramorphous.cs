using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class HideOfTerramorphous : ModShield
    {
        int usage = 0;

        public override void SetDefaults()
        {
            Item.Size = new Vector2(68, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(platinum: 1);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+150 HP\n+10 Defense\nRegenerates health rapidly");
            Utilities.Text(tooltips, Mod, "Tooltip2", "When under 65% health, melee attacks do 80% bonus damage", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Deals 100% bonus thorn damage", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip4", $"Releases a Fire Nova blast that deals {Main.LocalPlayer.statDefense * 4} damage when health dips under 30%", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "...His hide turned the mightiest tame...");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 150;
            player.statDefense += 10;
            player.lifeRegen += 10;

            // Spike
            player.thorns = 1f; // 100% thorn damage

            // Fire Nova
            if(player.whoAmI == Main.myPlayer)
            {
                if (player.statLife <= (player.statLifeMax2 * 0.3f) && usage == 1)
                {
                    usage = 0;
                    int novaDamage = (int)player.GetTotalDamage(DamageClass.Generic).ApplyTo(player.statDefense * 4);
                    float novaKnockback = 5f;
                    int novaType = ElementalID.LargeExplosiveProjectile;

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

        public override void UpdateEquip(Player player)
        {
            // Melee
            if (player.statLife <= (player.statLifeMax2 * 0.65f))
            {
                // Increases Melee damage by 80%
                player.GetDamage(DamageClass.Melee) += 0.8f;
            }
        }
    }
}