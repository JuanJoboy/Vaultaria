using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Ammo.Seraph.AssaultRifle.Vladof;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Jakobs
{
    public class FlushRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(98, 26);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 30;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 10;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 10);
            Utilities.ItemSound(Item, Utilities.Sounds.JakobsAR, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-11f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Found in Golden Chests", Utilities.VaultarianColours.Information);
        }
    }
}