using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Ammo.Terraria;

namespace Vaultaria.Content.Items.Weapons.Ammo
{
    public class CopperBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            // Size
            Item.Size = new Vector2(8, 8);

            // Damage
            Item.damage = 2;
            Item.DamageType = DamageClass.Ranged;
            Item.shootSpeed = 10f;

            // Ammo
            Item.ammo = AmmoID.Bullet;
            Item.shoot = ModContent.ProjectileType<CopperProjectile>();

            // Item Config
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = Item.buyPrice(copper: 30);
            Item.rare = ItemRarityID.White;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.CopperBar, 1)
                .AddTile(TileID.WorkBenches)
                .Register()
                .ReplaceResult(this, 75);
        }
    }
}