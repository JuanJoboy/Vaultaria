using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Items.Weapons.Ammo
{
    public class ShotgunAmmo : ModItem
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
            Item.damage = 0;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;

            // Ammo
            Item.ammo = ModContent.ItemType<ShotgunAmmo>();

            // Item Config
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = Item.buyPrice(copper: 50);
            Item.rare = ItemRarityID.White;
        }
    }
}