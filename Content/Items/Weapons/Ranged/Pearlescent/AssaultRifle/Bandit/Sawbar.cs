using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Pearlescent.AssaultRifle.Bandit;

namespace Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.AssaultRifle.Bandit
{
    public class Sawbar : ModItem
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
            Item.rare = ItemRarityID.Cyan;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 9f;
            Item.shoot = ModContent.ProjectileType<SawbarBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 40;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.BanditAR, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile projectile = Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ModContent.ProjectileType<SawbarBullet>(),
                damage,
                knockback,
                player.whoAmI,
                1f,
                0f
            );
            
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Assault Rifle Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Shoots bullets that create Fiery explosions")
            {
                OverrideColor = new Color(231, 92, 22) // Orange
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Suppressing fires!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}