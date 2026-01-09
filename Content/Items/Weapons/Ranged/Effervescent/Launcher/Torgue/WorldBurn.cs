using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Effervescent.Launcher.Torgue;

namespace Vaultaria.Content.Items.Weapons.Ranged.Effervescent.Launcher.Torgue
{
    public class WorldBurn : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(115, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Expert;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<WorldBurnRocket>();
            Item.useAmmo = ModContent.ItemType<LauncherAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 170;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 45;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.TorgueLauncher, 60);
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
            return new Vector2(-60f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Consumes 20 Launcher Ammo per shot");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots a more powerful Fiery nuke", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "War does not compute.");
        }
    }
}