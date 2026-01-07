using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.Sniper.Jakobs
{
    public class GearboxMuckamuck : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(123, 29);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 30;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 30;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(copper: 0);
            Utilities.ItemSound(Item, Utilities.Sounds.JakobsSniper, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.RedText(tooltips, Mod, "Courtesy of being a Premiere Club member.");
        }
    }
}