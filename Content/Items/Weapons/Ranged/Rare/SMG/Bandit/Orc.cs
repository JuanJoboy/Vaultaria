using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.GunEffects;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Bandit
{
    public class Orc : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(40, 29);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Purple;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 7f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 20;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.reuseDelay = 1;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.BanditSMG, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                type,
                damage,
                knockback,
                player.whoAmI
            );

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override void HoldItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<OrcEffect>()))
            {
                if (Main.rand.Next(1, 700) == 500)
                {
                    player.AddBuff(ModContent.BuffType<OrcEffect>(), 600);   
                }
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses any normal bullet type as ammo\nHolding the Orc has a chance to buff its wielder for 10 seconds.\nThe buff grants the following effects:");
            Utilities.Text(tooltips, Mod, "Tooltip2", "\t+1 Projectile\n\t+20% Damage\n\t+50% Fire Rate\n\tProjectiles ricochet", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Have I achieved worth yet?");
        }
    }
}