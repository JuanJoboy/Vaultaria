using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Common.Pistol.Tediore;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Tediore
{
    public class Handgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(41, 29);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 8;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 0.5f;
            Item.damage = 10;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.reuseDelay = 12;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.TediorePistol, 60);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < 29; i++)
                {
                    player.ConsumeItem(ammo.type, false);
                }
            }

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                // Spawn the grenade manually
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<HandgunGrenade>(), damage, knockback, player.whoAmI);
                return false; // prevent vanilla bullet spawn
            }

            return true; // normal bullet behavior
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Throw
            {
                Item.damage = 8;
                Item.crit = 1;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = true;
                Item.shootSpeed = 17f;
                Item.shoot = ModContent.ProjectileType<HandgunGrenade>();

                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.reuseDelay = 8;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.TediorePistolThrow, 120);
            }
            else // Shoot
            {
                Item.damage = 8;
                Item.crit = 0;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 17f;
                Item.shoot = ProjectileID.Bullet;

                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.reuseDelay = 12;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.TediorePistol, 60);
            }

            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Right-Click to throw the Pistol", Utilities.VaultarianColours.Explosive);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Found in Wooden Chests", Utilities.VaultarianColours.Information);
        }
    }
}