using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Torgue;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;

namespace Vaultaria.Content.Items.Weapons.Ammo
{
    public class PistolAmmo : ModItem
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
            Item.damage = 0; // Does no damage. It's just used as the consumable ammo for all pistols
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;

            // Ammo
            Item.ammo = ModContent.ItemType<PistolAmmo>();
            Item.shoot = ModContent.ProjectileType<UHBullet>();
            Item.shoot = ModContent.ProjectileType<FibberBullet>();

            // Item Config
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = Item.buyPrice(copper: 15);
            Item.rare = ItemRarityID.Gray;
        }
    }
}