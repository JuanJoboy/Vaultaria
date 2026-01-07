using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Hyperion
{
    public class FirstBlood : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(66, 30);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 9f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 60;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.reuseDelay = 1;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.HyperionSMG, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Deals 25% increased damage to enemies with more than 50% of their maximum health");
            Utilities.RedText(tooltips, Mod, "I don't do second chances.");
        }
    }
}