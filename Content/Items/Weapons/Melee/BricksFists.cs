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
    public class BricksFists : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.noMelee = true;
            Item.noUseGraphic = true;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 2.3f;
            Item.damage = 20;
            Item.crit = 6;
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = false;

            // Berserker
            Item.shoot = ModContent.ProjectileType<BerserkerFists>();
            Item.shootSpeed = 8;

            // Other properties
            Item.value = Item.buyPrice(copper: 20);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.NPCHit16; 
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(7, -7);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(3)
                .AddIngredient(ItemID.CopperBar, 3)
                .AddIngredient(ItemID.Cactus, 50)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient<Eridium>(3)
                .AddIngredient(ItemID.TinBar, 3)
                .AddIngredient(ItemID.Cactus, 50)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Throws a flurry of fists")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "SLAB... Did you... Did you just jump of the BUZZARD'S NEST?!\nGOD DAMN YOU MAKE ME PROUD!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}