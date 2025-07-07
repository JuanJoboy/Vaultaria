using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion
{
    public class Fibber : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1.3f;
            Item.useStyle = ItemUseStyleID.Shoot; // Use style for guns
            Item.rare = ItemRarityID.Blue;

            // Combat properties
            Item.damage = 40; // Gun damage + bullet damage = final damage
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 17; // Delay between shots.
            Item.useAnimation = 17; // How long shoot animation lasts in ticks.
            Item.reuseDelay = 2; // How long the gun will be unable to shoot after useAnimation ends
            Item.knockBack = 2.3f; // Gun knockback + bullet knockback = final knockback
            Item.autoReuse = true;

            // Other properties
            Item.value = 10000;
            Item.UseSound = SoundID.Item11; // Gun use sound

            // Gun properties
            Item.noMelee = true; // Item not dealing damage while held, we don’t hit mobs in the head with a gun
            Item.shootSpeed = 4f; // Speed of a projectile. Mainly measured by eye
            Item.shoot = ModContent.ProjectileType<FibberBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();
        }

        public override void AddRecipes()
        {
            // CreateRecipe()
            //     .AddIngredient<SteelShard>(9)
            //     .AddTile(TileID.Anvils)
            //     .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(
            source,
            position,
            velocity,
            ModContent.ProjectileType<FibberBullet>(),
            damage,
            knockback,
            player.whoAmI,
            1f, // Projectile.ai[0] = 1f; (This bullet is the cloner)
            1f  // Projectile.ai[1] = 0f; (Optional, if you don't need ai[1] yet)
            );

            return false; // Prevent vanilla from spawning the default ammo projectile
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+50% Love"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "+3000% Damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip3", "Firing Increases Accuracy"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Would I lie to you?")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}