using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Seraph.SMG.Maliwan;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Maliwan
{
    public class Florentine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(70, 29);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 9f;
            Item.shoot = ModContent.ProjectileType<FlorentineBullet>();
            Item.useAmmo = ModContent.ItemType<SubmachineGunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 28;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.reuseDelay = 1;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.ETechSMGSingle, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile projectile = Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ModContent.ProjectileType<FlorentineBullet>(),
                damage,
                knockback,
                player.whoAmI
            );

            if (projectile.ModProjectile is FlorentineBullet bullet)
            {
                bullet.shockMultiplier = 0.6f;
                bullet.slagMultiplier = 0.6f;
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses SMG Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Has a 40% chance to do 60% Slag & 60% Shock bonus damage", Utilities.VaultarianColours.Slag);
            Utilities.RedText(tooltips, Mod, "Double trouble.");
        }
    }
}