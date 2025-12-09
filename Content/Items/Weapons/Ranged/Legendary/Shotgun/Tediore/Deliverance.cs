using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Shotgun.Tediore;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Tediore
{
    public class Deliverance : ModItem
    {
        private bool altFireMode = false;
        public static bool thrown = false;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(70, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 15;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 35;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.TedioreShotgun, 60);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (altFireMode == true)
            {
                for (int i = 0; i < 100; i++)
                {
                    player.ConsumeItem(ammo.type, false);
                }
            }
            
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2) // Throw
            {
                altFireMode = true;

                Item.damage = 0;
                Item.crit = 0;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = true;
                Item.shootSpeed = 5f;
                Item.shoot = ModContent.ProjectileType<HomingDeliverance>();

                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.reuseDelay = 15;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.TedioreShotgunThrow, 120);
                thrown = true;
            }
            else // Shoot
            {
                altFireMode = false;

                Item.damage = 60;
                Item.crit = 16;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ProjectileID.Bullet;

                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.reuseDelay = 45;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.TedioreShotgun, 60);
            }

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (altFireMode == false)
            {
                Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 4, 5, 2, 10);
            }
            else
            {
                int homingProjectileType = ModContent.ProjectileType<HomingDeliverance>();

                Projectile.NewProjectile(source, position, velocity, homingProjectileType, damage, knockback, player.whoAmI);
                
                return false;
            }
            
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 8);
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Right-Click to throw a homing shotgun that shoots at enemies");
            Utilities.RedText(tooltips, Mod, "Kiki got a shotgun!");
        }
    }
}