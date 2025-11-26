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

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Jakobs
{
    public class OrphanMaker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(80, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 4, 5, 4, 6);

            return false;
        }

        public override bool CanUseItem(Player player)
        {
            if(Main.hardMode)
            {
                return true;
            }

            return false;
        }

        public override bool? UseItem(Player player)
        {
            player.statLife -= 5;

            return base.UseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 4);
            Utilities.Text(tooltips, Mod);

            if(!Main.hardMode)
            {
                Utilities.Text(tooltips, Mod, "Tooltip2", "Can only be used in Hardmode", Utilities.VaultarianColours.Information);
            }

            Utilities.Text(tooltips, Mod, "Tooltip3", "Found in Locked Shadow Chests", Utilities.VaultarianColours.Information);

            Utilities.RedText(tooltips, Mod, "Makes orphans. Often.");
            Utilities.CursedText(tooltips, Mod, "Curse of the Nefarious Backlash!\n(-5 HP on use)");
        }
    }
}