using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Projectiles.Melee;
using Terraria.DataStructures;
using System.Collections.Generic;

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
            Item.damage = 25;
            Item.crit = 10;
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = true;

            // Berserker
            Item.shoot = ModContent.ProjectileType<BerserkerFists>();
            Item.shootSpeed = 8;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Item.rare = ItemRarityID.Green;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(7, -7);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Bleeding, 300);
        }

        // This tells Terraria that this item has an alternate use mode (usually right-click)
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(9)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Throws a flurry of fists"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "SLAB... Did you... Did you just jump of the BUZZARD'S NEST?! GOD DAMN YOU MAKE ME PROUD!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}