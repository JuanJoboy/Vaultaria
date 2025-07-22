using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Ammo.Seraph.AssaultRifle.Vladof;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Seraph.AssaultRifle.Vladof
{
    public class LeadStorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.65f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<LeadStormBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 45;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Item.UseSound = SoundID.Item41;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 2, 5);

            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Fires 3 arching bullets\nUses Assault Rifle Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "What a glorious feeling!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}