using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Torgue;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Torgue
{
    public class Nukem : ModItem
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
            Item.shoot = ModContent.ProjectileType<NukemRocket>();
            Item.useAmmo = ModContent.ItemType<LauncherAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 150;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 30;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 3);
            Item.UseSound = SoundID.Item62;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 19; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-70f, 4f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Consumes 20 Launcher Ammo per shot"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Fires a nuke")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Name dropper.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}