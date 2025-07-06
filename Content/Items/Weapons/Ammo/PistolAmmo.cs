using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Torgue;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;

namespace Vaultaria.Content.Items.Weapons.Ammo
{
    public class PistolAmmo : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 8;

            Item.damage = 0; // Does no damage. It's just used as the consumable ammo for all pistols
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;

            Item.maxStack = 9999;
            Item.consumable = true;

            Item.ammo = ModContent.ItemType<PistolAmmo>();
            Item.shoot = ModContent.ProjectileType<UHBullet>();
            Item.shoot = ModContent.ProjectileType<FibberBullet>();

            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Gray;
        }
    }
}