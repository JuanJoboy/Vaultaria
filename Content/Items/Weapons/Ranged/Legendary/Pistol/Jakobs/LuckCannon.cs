using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs
{
    public class LuckCannon : ModItem
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
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<LuckCannonBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 120;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.reuseDelay = 0;
            Item.autoReuse = false;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
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

            tooltips.Add(new TooltipLine(Mod, "Red Text", "Better lucky than good!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}