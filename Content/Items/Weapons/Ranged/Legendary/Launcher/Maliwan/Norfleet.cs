using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Maliwan;
using Vaultaria.Content.Prefixes.Weapons;

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
            Item.Size = new Vector2(115, 34);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Master;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 5;
            Item.shoot = ModContent.ProjectileType<NorfleetRocket>();
            Item.useAmmo = ModContent.ItemType<LauncherAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 200;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.reuseDelay = 60;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(platinum: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.Norfleet, 120);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 35);

            return false;
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-45f, 3f);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 24; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<RangerTrickshot>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 3);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Consumes 25 Launcher Ammo per shot");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots 3 elemental orbs that deal massive damage", Utilities.VaultarianColours.Master);
            Utilities.RedText(tooltips, Mod, "Blows up everything!");
        }
    }
}