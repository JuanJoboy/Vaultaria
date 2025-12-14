using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Shields;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Grenades.Epic
{
    public class MagicMissileEpic : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(22, 30);
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.Purple;
            Item.maxStack = 9999;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2.3f;
            Item.damage = 40;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = false;

            Item.shoot = ModContent.ProjectileType<HomingSlagBall>();
            Item.consumable = true;
            Item.ammo = Item.type;
            Item.shootSpeed = 4;

            // Other properties
            Item.value = Item.buyPrice(silver: 15);
            Item.UseSound = SoundID.DD2_GoblinBomberThrow;
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
                1f
            );

            Projectile projectile = Main.projectile[projectileIndex];

            if (projectile.ModProjectile is HomingSlagBall grenade)
            {
                grenade.slagMultiplier = 0.4f;
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Throws out 4 Slag balls that home in on an enemy and explode on impact", Utilities.VaultarianColours.Slag);
            Utilities.RedText(tooltips, Mod, "No wand required. Just point and shoot.");
        }
    }
}