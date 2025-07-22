using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;

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
            Item.scale = 0.6f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 6f;
            Item.shoot = ModContent.ProjectileType<FibberBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 40;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Item.UseSound = SoundID.Item11;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            Projectile.NewProjectileDirect(
            source,
            position,
            velocity,
            ModContent.ProjectileType<FibberBullet>(),
            damage,
            knockback,
            player.whoAmI,
            1f,
            1f
            );

            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Pistol Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "+50% Love"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip3", "+3000% Damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip4", "Firing Increases Accuracy\nOn tile collision, the initial projectile splits into 10 projectiles"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Would I lie to you?")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<Trickshot>();
        }
    }
}