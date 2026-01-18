using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Maliwan;
using Vaultaria.Content.Buffs.GunEffects;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Maliwan
{
    public class GrogNozzle : ElementalItem
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
            Item.Size = new Vector2(48, 30);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<GrogBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 7;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.SetItemSound(Item, Utilities.Sounds.MaliwanPistol, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile projectile = Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ModContent.ProjectileType<GrogBullet>(),
                damage,
                knockback,
                player.whoAmI
            );

            if (projectile.ModProjectile is GrogBullet bullet)
            {
                bullet.slagMultiplier = 0.2f;
            }      
                
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Pistol Ammo\nHolding the Grog Nozzle has a chance to buff its wielder for 10 seconds.\nThe buff grants the following effects:");
            Utilities.Text(tooltips, Mod, "Tooltip2", "\t+5 Projectiles\n\t-50% Fire Rate", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Heals the player on enemy hit", Utilities.VaultarianColours.Healing);
            Utilities.Text(tooltips, Mod, "Tooltip3", "+100% Chance to Apply Slag", Utilities.VaultarianColours.Slag);
            Utilities.RedText(tooltips, Mod, "Hand over the keys, Sugar...");
        }

        public override void HoldItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<DrunkEffect>()))
            {
                if (Main.rand.Next(1, 2000) == 1750)
                {
                    player.AddBuff(ModContent.BuffType<DrunkEffect>(), 600);   
                }
            }
        }
        
        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<Slag>();
        }
    }
}