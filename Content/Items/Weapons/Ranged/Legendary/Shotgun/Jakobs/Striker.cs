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

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Jakobs
{
    public class Striker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(80, 32);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 20;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 6;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.JakobsShotgun, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 5, 5);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 5);
            Utilities.Text(tooltips, Mod);
            Utilities.RedText(tooltips, Mod, "Fandir? Thirteen.");
        }
    }
}