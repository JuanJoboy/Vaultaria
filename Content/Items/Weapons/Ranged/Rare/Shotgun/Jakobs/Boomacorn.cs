using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Hyperion;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Jakobs;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Jakobs
{
    public class Boomacorn : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(108, 30);
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 30;
            Item.shoot = ModContent.ProjectileType<BoomacornBullet>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 40;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 7;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.JakobsShotgun, 60);
        }

        public override bool CanUseItem(Player player)
        {
            if(Main.hardMode)
            {
                return true;
            }

            return false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 7, 5, 4, 6);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-0f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 7);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Shotgun Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots every element", Utilities.VaultarianColours.Information);

            if(!Main.hardMode)
            {
                Utilities.Text(tooltips, Mod, "Tooltip3", "Can only be used in Hardmode", Utilities.VaultarianColours.Information);
            }

            Utilities.Text(tooltips, Mod, "Tooltip4", "Found in Skyware Chests", Utilities.VaultarianColours.Information);

            Utilities.RedText(tooltips, Mod, "Always, I want to be with you.");
        }
    }
}