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
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 7.5f;
            Item.shoot = ModContent.ProjectileType<FlorentineBullet>();
            Item.useAmmo = ModContent.ItemType<SubmachineGunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 35;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 1;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Item.UseSound = SoundID.Item176;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

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
                bullet.shockMultiplier = 0.4f;
                bullet.slagMultiplier = 0.2f;
            }

            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses SMG Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Has a 40% chance to do 20% Slag & 40% Shock bonus damage")
            {
                OverrideColor = new Color(63, 71, 212)
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Double trouble.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}