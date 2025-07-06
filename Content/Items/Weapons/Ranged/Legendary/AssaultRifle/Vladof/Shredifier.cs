using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Vladof
{
    public class Shredifier : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1; // How many items need for research in Journey Mode
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.width = 60;
            Item.height = 20;
            Item.scale = 1.05f;
            Item.useStyle = ItemUseStyleID.Shoot; // Use style for guns
            Item.rare = ItemRarityID.Yellow;

            // Combat properties
            Item.damage = 100; // Gun damage + bullet damage = final damage
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 1; // Delay between shots.
            Item.useAnimation = 1; // How long shoot animation lasts in ticks.
            Item.reuseDelay = 0; // How long the gun will be unable to shoot after useAnimation ends
            Item.knockBack = 2.3f; // Gun knockback + bullet knockback = final knockback
            Item.autoReuse = true;

            // Other properties
            Item.value = 10000;
            Item.UseSound = SoundID.Item11; // Gun use sound

            // Gun properties
            Item.noMelee = true; // Item not dealing damage while held, we don’t hit mobs in the head with a gun
            Item.shootSpeed = 10f; // Speed of a projectile. Mainly measured by eye
            Item.shoot = ModContent.ProjectileType<ShredifierBullet>(); // What kind of projectile the gun fires, does not mean anything here because it is replaced by ammo
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();
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
            ModContent.ProjectileType<ShredifierBullet>(),
            damage,
            knockback,
            player.whoAmI,
            1f, // Projectile.ai[0] = 1f; (This bullet is the cloner)
            0f  // Projectile.ai[1] = 0f; (Optional, if you don't need ai[1] yet)
            );

            return false; // Prevent vanilla from spawning the default ammo projectile
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+100% Fire rate"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Speed kills.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}