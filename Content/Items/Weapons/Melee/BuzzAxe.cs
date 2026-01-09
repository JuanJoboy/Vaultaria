using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Projectiles.Melee;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Content.Items.Weapons.Melee
{
    public class BuzzAxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(28, 54);
            Item.scale = 1f;
            Item.rare = ItemRarityID.Green;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.3f;
            Item.damage = 18;
            Item.crit = 0;
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 2;
            Item.autoReuse = true;
            Item.useTurn = true;

            // BuzzAxe Bombardier
            Item.shoot = ModContent.ProjectileType<BuzzAxeBombardier>();
            Item.shootSpeed = 11;

            // Other properties
            Item.value = Item.buyPrice(silver: 1);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(10, -7);
        }

        // This tells Terraria that this item has an alternate use mode (usually right-click)
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2) // Shoot
            {
                Item.damage = 18;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = true;
                Item.shootSpeed = 11f;
                Item.shoot = ModContent.ProjectileType<BuzzAxeBombardier>();

                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.reuseDelay = 2;
                Item.autoReuse = true;
                Item.useTurn = true;

                Item.UseSound = SoundID.Item23;
            }
            else // Melee
            {
                Item.damage = 18;
                Item.DamageType = DamageClass.Melee;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = false;
                Item.shootSpeed = 0f;
                Item.shoot = ProjectileID.None; // Disable shooting
                Item.useAmmo = AmmoID.None;

                Item.useTime = 20;
                Item.useAnimation = 20;
                Item.reuseDelay = 0;
                Item.autoReuse = true;
                Item.useTurn = true;

                Item.UseSound = SoundID.Item23;
            }

            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(5)
                .AddIngredient(ItemID.SilverBroadsword, 1)
                .AddIngredient(ItemID.SharpeningStation, 1)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient<Eridium>(5)
                .AddIngredient(ItemID.TungstenBroadsword, 1)
                .AddIngredient(ItemID.SharpeningStation, 1)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<Incendiary>() &&
                   pre != ModContent.PrefixType<Shock>() &&
                   pre != ModContent.PrefixType<Corrosive>() &&
                   pre != ModContent.PrefixType<Explosive>() &&
                   pre != ModContent.PrefixType<Slag>() &&
                   pre != ModContent.PrefixType<Cryo>() &&
                   pre != ModContent.PrefixType<Radiation>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Right-Click to throw an explosive buzz axe");
            Utilities.RedText(tooltips, Mod, "I'M THE CONDUCTOR OF THE POOP TRAIN!!!");
        }
    }
}