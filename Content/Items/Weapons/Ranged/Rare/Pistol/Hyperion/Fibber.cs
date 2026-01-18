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
    public class Fibber : ElementalItem
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
            Item.Size = new Vector2(40, 29);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 11f;
            Item.shoot = ModContent.ProjectileType<FibberBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 26;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.SetItemSound(Item, Utilities.Sounds.HyperionPistol, 60);
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
                1f,
                1f
            );        
                
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3f, 2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Pistol Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "+50% Love");
            Utilities.Text(tooltips, Mod, "Tooltip3", "+3000% Damage");
            Utilities.Text(tooltips, Mod, "Tooltip4", "Firing Increases Accuracy");
            Utilities.Text(tooltips, Mod, "Tooltip5", "On tile collision, the initial Projectile splits into 10 Projectiles", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Would I lie to you?");
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<RangerTrickshot>();
        }
    }
}