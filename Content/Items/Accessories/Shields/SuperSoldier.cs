using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Materials;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class SuperSoldier : ModShield
    {
        int usage = 0;

        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ItemRarityID.Master;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+20 HP\n+4 Defense\nRegenerates health"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "When under 10% health, become invulnerable, & gain the following increases while health is full:\n\t+50% Fire Rate\n\t+25% Move Speed\n\t+25% No Ammo Consumption Chance\nMust have full health before immunity can be used again")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Roland, out.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 4;
            player.lifeRegen += 2;

            if (player.statLife <= player.statLifeMax2 * 0.1 && usage == 0)
            {
                SoundEngine.PlaySound(SoundID.Item176);
                player.immune = true;
                player.immuneTime = 300;
                usage = 1;
            }

            if (player.statLife == player.statLifeMax2)
            {
                usage = 0;

                player.GetAttackSpeed(DamageClass.Ranged) *= 0.50f;
                player.moveSpeed *= 1.25f;
                player.ammoCost75 = true; // This gives a 25% chance not to consume ammo
            }

            if (player.statLife != player.statLifeMax2)
            {
                player.GetAttackSpeed(DamageClass.Ranged) /= 0.50f;
                player.moveSpeed /= 1.25f;
                player.ammoCost75 = false; // This gives a 25% chance not to consume ammo
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(40)
                .AddIngredient(ItemID.SpectreBar, 40)
                .AddIngredient(ItemID.SoulofMight, 25)
                .AddIngredient<StopGap>(1)
                .AddIngredient(ItemID.SwiftnessPotion, 25)
                .AddIngredient(ItemID.AmmoBox, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}