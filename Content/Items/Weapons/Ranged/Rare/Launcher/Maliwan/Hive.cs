using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Launcher.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Launcher.Maliwan
{
    public class Hive : ModItem
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
            Item.shootSpeed = 5;
            Item.shoot = ModContent.ProjectileType<HiveRocket>();
            Item.useAmmo = ModContent.ItemType<LauncherAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 125;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 30;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Item.UseSound = SoundID.Item14;
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-60f, 5f);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 14; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ToolTip1", "Consumes 15 Launcher Ammo per shot"));
            tooltips.Add(new TooltipLine(Mod, "ToolTip2", "After a second, the initial projectile will spawn Corrosive homing rockets")
            {
                OverrideColor = new Color(136, 235, 94) // Light Green
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Full of bees.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}