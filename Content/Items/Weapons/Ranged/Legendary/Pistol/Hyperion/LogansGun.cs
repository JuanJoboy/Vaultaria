using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Hyperion;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Hyperion
{
    public class LogansGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<LoganBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 18;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Item.UseSound = SoundID.Item11;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(35)
                .AddIngredient(ItemID.SpaceGun, 1)
                .AddIngredient(ItemID.IllegalGunParts, 2)
                .AddIngredient(ItemID.Grenade, 60)
                .AddIngredient(ItemID.HellstoneBar, 8)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }   

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Pistol Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Shoots Explosive-Fire Rockets")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Gun, Gunner!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}