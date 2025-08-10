using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Torgue;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue
{
    public class UnkemptHarold : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot; // Use style for guns
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true; // Item not dealing damage while held, we don’t hit mobs in the head with a gun
            Item.shootSpeed = 4f; // Speed of a projectile. Mainly measured by eye
            Item.shoot = ModContent.ProjectileType<UHBullet>(); // Shoots this
            Item.useAmmo = ModContent.ItemType<PistolAmmo>(); // Uses this ammo

            // Combat properties
            Item.knockBack = 2.3f; // Gun knockback + bullet knockback = final knockback
            Item.damage = 20; // Gun damage + bullet damage = final damage
            Item.crit = 10; // Crit chance + 4% base
            Item.DamageType = DamageClass.Ranged; // Does ranged damage

            Item.useTime = 20; // Delay between shots.
            Item.useAnimation = 20; // How long shoot animation lasts in ticks.
            Item.reuseDelay = 2; // How long the gun will be unable to shoot after useAnimation ends
            Item.autoReuse = true; // Auto fire

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Item.UseSound = SoundID.Item11;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);
            
            Projectile projectile = Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ModContent.ProjectileType<UHBullet>(),
                damage,
                knockback,
                player.whoAmI,
                1f, // Projectile.ai[0] = 1f; (This bullet is the cloner)
                0f  // Projectile.ai[1] = 0f; (Optional, if you don't need ai[1] yet)
            );

            if (projectile.ModProjectile is UHBullet bullet)
            {
                bullet.explosiveMultiplier = 1f;
            }

            return false; // Prevent vanilla from spawning the default ammo projectile
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 2; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Consumes 3 Pistol Ammo per shot"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Fires multiple Explosive rounds")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Did I fire six shots, or only five? Three? Seven. Whatever.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}