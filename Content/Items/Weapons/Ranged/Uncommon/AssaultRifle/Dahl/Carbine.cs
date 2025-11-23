using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Ammo.Seraph.AssaultRifle.Vladof;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Dahl
{
    public class Carbine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(71, 29);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 7;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.reuseDelay = 7;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.DahlARBurst, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
        }
    }
}