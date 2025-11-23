using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Torgue
{
    public class Ogre : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(103, 29);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 16;
            Item.shoot = ModContent.ProjectileType<OgreBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 25;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.TorgueAR, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 5, 4, 8);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, -2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 3);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Assault Rifle Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Rapidly fires 3 explosive bullets", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "Ogres chew their food.");
        }
    }
}