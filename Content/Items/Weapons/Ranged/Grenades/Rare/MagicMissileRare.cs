using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Shields;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare
{
    public class MagicMissileRare : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(19, 29);
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 9999;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2.3f;
            Item.damage = 30;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = false;

            Item.shoot = ModContent.ProjectileType<HomingSlagBall>();
            Item.consumable = true;
            Item.ammo = Item.type;
            Item.shootSpeed = 4;

            // Other properties
            Item.value = Item.buyPrice(silver: 10);
            Item.UseSound = SoundID.Item169;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int projectileIndex = Projectile.NewProjectile(
                source,
                position,
                velocity,
                ModContent.ProjectileType<HomingSlagBall>(),
                damage,
                knockback,
                player.whoAmI,
                0f,
                0f,
                1f
            );

            Projectile projectile = Main.projectile[projectileIndex];

            if (projectile.ModProjectile is HomingSlagBall grenade)
            {
                grenade.slagMultiplier = 0.2f;
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Throws out 2 Slag balls that home in on an enemy and explode on impact", Utilities.VaultarianColours.Slag);
            Utilities.RedText(tooltips, Mod, "No wand required. Just point and shoot.");
        }
    }
}