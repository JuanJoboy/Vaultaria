using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Tediore;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Tediore
{
    public class BabyMaker : ModItem
    {
        private bool altFireMode = false;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(68, 30);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 15;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.reuseDelay = 8;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.TedioreSMG, 60);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (altFireMode == true)
            {
                for (int i = 0; i < 75; i++)
                {
                    player.ConsumeItem(ammo.type, false);
                }
            }

            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (altFireMode)
            {
                // Spawn the grenade manually
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<BabyMakerGrenade>(), damage, knockback, player.whoAmI);
                return false; // prevent vanilla bullet spawn
            }

            return true; // normal bullet behavior
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Throw
            {
                altFireMode = true;

                Item.damage = 50;
                Item.crit = 0;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = true;
                Item.shootSpeed = 17f;
                Item.shoot = ModContent.ProjectileType<BabyMakerGrenade>();

                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.reuseDelay = 8;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.TedioreSMGThrow, 120);
            }
            else // Shoot
            {
                altFireMode = false;

                Item.knockBack = 1f;
                Item.damage = 15;
                Item.crit = 6;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 17f;
                Item.shoot = ProjectileID.Bullet;

                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.reuseDelay = 8;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.TedioreSMG, 60);
            }

            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-25f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Right-Click to throw an explosive SMG");
            Utilities.RedText(tooltips, Mod, "Who's a widdle gunny-wunny?");   
        }
    }
}