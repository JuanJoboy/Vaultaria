using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Vladof;

namespace Vaultaria.Content.Items.Weapons.Ammo
{
    public class CopperAmmo : ModItem
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

            // Ammo
            Item.ammo = AmmoID.Bullet;

            // Item Config
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = Item.buyPrice(copper: 30);
            Item.rare = ItemRarityID.White;
        }
    }
}