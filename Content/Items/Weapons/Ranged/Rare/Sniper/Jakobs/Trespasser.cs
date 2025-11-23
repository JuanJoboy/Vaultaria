using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Jakobs
{
    public class Trespasser : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(98, 26);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 55;
            Item.crit = 1;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.reuseDelay = 48;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 3);
            Utilities.ItemSound(Item, Utilities.Sounds.JakobsSniper, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, -2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bypasses all enemy defense", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "I infrequently perish.");
        }
    }
}