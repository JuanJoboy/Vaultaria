using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Items.Weapons.Ammo
{
    public class SniperAmmo : ModItem
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
            Item.ammo = ModContent.ItemType<SniperAmmo>();

            // Item Config
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = Item.buyPrice(silver: 30);
            Item.rare = ItemRarityID.White;
        }
    }
}