using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Projectiles.Melee;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Melee
{
    public class BuzzAxe : ModItem
    {
        private bool isInMeleeMode = false;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1f;
            Item.rare = ItemRarityID.Green;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.3f;
            Item.damage = 25;
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
            Item.UseSound = SoundID.Item23;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(30, -7);
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
                isInMeleeMode = false;

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
            }
            else // Melee
            {
                isInMeleeMode = true;

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
            }

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);
            
            return !isInMeleeMode; // Only shoot if not in melee mode
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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Right-Click to throw an explosive buzz axe"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "I'M THE CONDUCTOR OF THE POOP TRAIN!!!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}