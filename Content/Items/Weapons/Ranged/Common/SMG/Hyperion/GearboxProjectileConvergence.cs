using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.SMG.Hyperion
{
    public class GearboxProjectileConvergence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 27);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 5;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 4;
            Item.useAnimation = 4;
            Item.reuseDelay = 4;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(copper: 0);
            Utilities.ItemSound(Item, Utilities.Sounds.HyperionSMG, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.RedText(tooltips, Mod, "Courtesy of being a Premiere Club member.");
        }
    }
}