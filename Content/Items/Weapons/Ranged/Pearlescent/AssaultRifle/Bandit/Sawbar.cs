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
    public class Sawbar : ElementalItem
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
            Item.Size = new Vector2(91, 30);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Cyan;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 9f;
            Item.shoot = ModContent.ProjectileType<SawbarBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 23;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.SetItemSound(Item, Utilities.Sounds.BanditAR, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(
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
            return new Vector2(-10f, 4f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "tooltip1", "Uses Assault Rifle Ammo");
            Utilities.Text(tooltips, Mod, "tooltip2", "Shoots bullets that create Fiery explosions", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "Suppressing fires!");
        }
    }
}