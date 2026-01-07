using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Laser.Tediore;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Tediore
{
    public class LaserDisker : ModItem
    {
        private bool altFireMode = false;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(54, 30);
            Item.scale = 1.1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ModContent.ProjectileType<LaserDiskerBullet>();
            Item.mana = 20;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 100;
            Item.crit = 6;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 10;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.LaserDisker, 60);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
        {
            base.ModifyManaCost(player, ref reduce, ref mult);

            if(altFireMode)
            {
                mult = 4;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (altFireMode)
            {
                // Spawn the grenade manually
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LaserDiskerGrenade>(), damage, knockback, player.whoAmI);
                return false; // prevent vanilla bullet spawn
            }

            return true; // normal bullet behavior
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Throw
            {
                altFireMode = true;

                Item.damage = 200;
                Item.crit = 0;
                Item.DamageType = DamageClass.Magic;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = true;
                Item.shootSpeed = 17f;
                Item.shoot = ModContent.ProjectileType<LaserDiskerGrenade>();

                Item.useAnimation = 15;
                Item.reuseDelay = 10;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.TedioreLaserThrow, 120);
            }
            else // Shoot
            {
                altFireMode = false;

                Item.damage = 100;
                Item.crit = 16;
                Item.DamageType = DamageClass.Magic;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 17f;
                Item.shoot = ModContent.ProjectileType<LaserDiskerBullet>();
                Item.mana = 20;

                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.reuseDelay = 10;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.LaserDisker, 60);
            }

            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "ToolTip1", "Shoots Explosive Laser Disks", Utilities.VaultarianColours.Explosive);
            Utilities.Text(tooltips, Mod, "ToolTip2", "Right-Click to throw the weapon", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "Shazbot!");
        }
    }
}