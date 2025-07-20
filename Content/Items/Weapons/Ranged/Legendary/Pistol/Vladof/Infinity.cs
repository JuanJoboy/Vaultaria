using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Common.Globals.Prefixes.Elements;

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
            Item.scale = 1.3f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 30;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Item.UseSound = SoundID.Item11;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // 1. Get the current prefix of the weapon firing the projectile.
            // This is the "snapshot" of the weapon's elemental type at the moment of firing.
            int prefix = Item.prefix;

            // 2. Call a helper method to create the projectile and attach the snapped prefix.
            // This method handles the creation of the projectile and ensures its ModProjectile (or GlobalProjectile) receives the 'prefix' value that was snapped in the previous step.
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            return false;
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
            return new Vector2(4f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Consumes no ammo"));
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