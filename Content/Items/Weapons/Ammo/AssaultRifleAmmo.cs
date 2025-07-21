using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Vladof;

namespace Vaultaria.Content.Items.Weapons.Ammo
{
    public class AssaultRifleAmmo : ModItem
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
            Item.damage = 0; // Does no damage. It's just used as the consumable ammo for all AR's
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 6;

            // Ammo
            Item.ammo = ModContent.ItemType<AssaultRifleAmmo>();

            // Item Config
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = Item.buyPrice(copper: 30);
            Item.rare = ItemRarityID.White;
        }
    }
}