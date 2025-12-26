using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Order : ModShield
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(54, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+15 HP\n+2 Defense");
            Utilities.Text(tooltips, Mod, "Tooltip2", "When under 30% health, melee attacks do 40% bonus damage", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Gives you lifesteal if the Law is also equipped", Utilities.VaultarianColours.Healing);
            Utilities.RedText(tooltips, Mod, "Chung-gunk!");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 15;
            player.statDefense += 2;
        }

        public override void UpdateEquip(Player player)
        {
            if (player.statLife <= (player.statLifeMax2 * 0.3f))
            {
                // Increases Melee damage by 40%
                player.GetDamage(DamageClass.Melee) += 0.4f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(20)
                .AddIngredient(ItemID.PlatinumBar, 8)
                .AddIngredient(ItemID.HealingPotion, 75)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}