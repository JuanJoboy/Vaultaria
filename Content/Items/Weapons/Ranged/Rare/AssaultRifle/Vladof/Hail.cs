using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Ammo.Rare.AssaultRifle.Vladof;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof
{
    public class Hail : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 13;
            Item.shoot = ModContent.ProjectileType<HailBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 15;
            Item.crit = 20;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 50);
            Item.UseSound = SoundID.Item11;
        }
        
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ToolTip1", "Uses Assault Rifle Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+25% Lifesteal per bullet")
            {
                OverrideColor = new Color(245, 201, 239) // Pink
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "What play thing can you offer me today?")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}