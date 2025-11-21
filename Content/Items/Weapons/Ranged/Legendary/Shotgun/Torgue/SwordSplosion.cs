using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Shotgun.Torgue;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue
{
    public class SwordSplosion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<SwordSplosionKnife>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 15;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 35;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Item.UseSound = SoundID.Item41;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 5);
            
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
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

            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Shotgun Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "KBecause Mister Torgue said so.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}