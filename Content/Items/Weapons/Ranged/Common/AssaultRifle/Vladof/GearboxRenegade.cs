using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.AssaultRifle.Vladof
{
    public class GearboxRenegade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 27);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 4;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.reuseDelay = 8;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(copper: 0);
            Utilities.ItemSound(Item, Utilities.Sounds.VladofAR, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3f, 4f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.RedText(tooltips, Mod, "Courtesy of being a Premiere Club member.");
        }
    }
}