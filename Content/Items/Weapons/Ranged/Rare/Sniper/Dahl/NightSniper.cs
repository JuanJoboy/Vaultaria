using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Dahl
{
    public class NightSniper : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(116, 32);
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
            Item.damage = 20;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 8;
            Item.useAnimation = 24;
            Item.reuseDelay = 45;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 50);
            Utilities.ItemSound(Item, Utilities.Sounds.DahlSniperBurst, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Found in Frozen Chests", Utilities.VaultarianColours.Information);
        }
    }
}