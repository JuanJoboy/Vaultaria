using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Uncommon.Shotgun.Torgue;

namespace Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Shotgun.Torgue
{
    public class ThreeWayHulk : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(78, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 30;
            Item.shoot = ModContent.ProjectileType<HulkBullet>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 3;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.reuseDelay = 30;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.TorgueShotgun, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 15, 2, 4, 7);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 15);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Shotgun Ammo");
        }
    }
}