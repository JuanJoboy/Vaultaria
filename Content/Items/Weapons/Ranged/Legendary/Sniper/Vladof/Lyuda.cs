using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Vladof;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Vladof
{
    public class Lyuda : ElementalItem
    {
        protected override Utilities.Sounds[] ItemSounds => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 13f;
            Item.shoot = ModContent.ProjectileType<LyudaBullet>();
            Item.useAmmo = ModContent.ItemType<SniperAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 60;
            Item.crit = 11;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.SetItemSound(Item, Utilities.Sounds.VladofSniper, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ModContent.ProjectileType<LyudaBullet>(),
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
            Utilities.Text(tooltips, Mod, "tooltip1", "Uses Sniper Ammo");
            Utilities.Text(tooltips, Mod, "tooltip2", "Initial Projectile splits into 3 Projectiles", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Man killer.");
        }
    }
}