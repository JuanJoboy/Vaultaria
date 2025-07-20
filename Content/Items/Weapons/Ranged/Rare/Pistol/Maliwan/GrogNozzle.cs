using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Maliwan;
using Vaultaria.Content.Buffs.GunEffects;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Maliwan
{
    public class GrogNozzle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<GrogBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 20;
            Item.crit = 0;
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
            return new Vector2(6f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+65% Lifesteal\nEvery 10 seconds the Grog has a 20% chance to Buff its wielder for 10 seconds.\nThe buff grants the following effects:"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "\t+5 Projectiles\n\t-50% Fire Rate")
            {
                OverrideColor = new Color(224, 224, 224) // Light Grey
            });
            tooltips.Add(new TooltipLine(Mod, "Tooltip3", "+100% Chance to Apply Slag")
            {
                OverrideColor = new Color(142, 94, 235) // Purple
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Hand over the keys, Sugar...")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void HoldItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<DrunkEffect>()))
            {
                if (Main.rand.Next(0, 10) == 5)
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