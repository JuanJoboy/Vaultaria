using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Vladof
{
    public class Infinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.6f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 35;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Item.UseSound = SoundID.Item31;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(50)
                .AddIngredient(ItemID.ChlorophyteBar, 25)
                .AddIngredient(ItemID.Handgun, 1)
                .AddIngredient(ItemID.EndlessMusketPouch, 2)
                .AddIngredient(ItemID.IllegalGunParts, 5)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses any normal bullet type as ammo\nConsumes no ammo"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "It's closer than you think! (no it isn't)")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }
    }
}