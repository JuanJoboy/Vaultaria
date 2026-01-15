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
using Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Jakobs;
using Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Shotgun.Torgue;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Torgue
{
    public class Wombat : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(75, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ModContent.ProjectileType<WombatBullet>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 15;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 40;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.TorgueShotgun, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 7, 4, 3, 8);

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3f, 0f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(20)
                .AddIngredient<ThreeWayHulk>(1)
                .AddIngredient(ItemID.MeteoriteBar, 7)
                .AddIngredient(ItemID.Feather, 5)
                .AddIngredient(ItemID.SharkToothNecklace, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 7);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Shotgun Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots 7 Explosive shots", Utilities.VaultarianColours.Explosive);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Does friendly-fire damage", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "The bush bulldozer.");
        }
    }
}