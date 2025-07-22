using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Torgue
{
    public class Ogre : ModItem
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
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<OgreBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 50;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Item.UseSound = SoundID.Item11;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 2, 5);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine damageLine = tooltips.Find(tip => tip.Name == "Damage");

            if (damageLine != null)
            {
                Player player = Main.LocalPlayer;
                int finalDamage = (int)player.GetTotalDamage(Item.DamageType).ApplyTo(Item.damage);
                damageLine.Text = finalDamage + " x 3 ranged damage";
            }

            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Assault Rifle Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Rapidly fires 3 explosive bullets")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Ogres chew their food.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}