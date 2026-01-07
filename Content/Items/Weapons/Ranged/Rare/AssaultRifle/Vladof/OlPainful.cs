using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Ammo.Rare.AssaultRifle.Vladof;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof
{
    public class OlPainful : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(77, 30);
            Item.scale = 1.1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 13;
            Item.shoot = ProjectileID.HeatRay;
            Item.mana = 10;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 14;
            Item.crit = 16;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 50);
            Utilities.ItemSound(Item, Utilities.Sounds.GenericLaser, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 5, 4, 7);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "ToolTip1", "Shoots 3 lasers", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Found in Skyware Chests", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Come on in... Ol' Painful is waiting.");
        }
    }
}