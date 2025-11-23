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
            Item.Size = new Vector2(51, 60);
            Item.scale = 0.5f;
            Item.noMelee = true;
            Item.noUseGraphic = false;

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

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, -30);
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
            Utilities.Text(tooltips, Mod, "Tooltip1", "Throws a flurry of fists");
            Utilities.RedText(tooltips, Mod, "SLAB... Did you... Did you just jump of the BUZZARD'S NEST?!\nGOD DAMN YOU MAKE ME PROUD!");
        }
    }
}