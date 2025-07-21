using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Maliwan
{
    public class Norfleet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Master;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 5;
            Item.shoot = ModContent.ProjectileType<NorfleetRocket>();
            Item.useAmmo = ModContent.ItemType<LauncherAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 1000;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 60;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(platinum: 1);
            Item.UseSound = SoundID.Item14;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 2, 35);

            return false;
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-70f, 0f);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 24; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
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

            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Shoots 3 elemental orbs that deal massive damage\nConsumes 25 Launcher Ammo per shot"));

            tooltips.Add(new TooltipLine(Mod, "Red Text", "Blows up everything!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}