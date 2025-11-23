using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Hyperion;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Hyperion
{
    public class HeartBreaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<HeartBreakerBullet>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 3;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Utilities.ItemSound(Item, Utilities.Sounds.HyperionShotgun, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 12, 3, 2, 10);

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(30)
                .AddIngredient(ItemID.HellstoneBar, 15)
                .AddIngredient(ItemID.Boomstick, 1)
                .AddIngredient(ItemID.HealingPotion, 50)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 12);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Shotgun Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip3", "+50% Lifesteal per bullet", Utilities.VaultarianColours.Healing);
            Utilities.RedText(tooltips, Mod, "I don't want to set the world on fire…");
        }
    }
}