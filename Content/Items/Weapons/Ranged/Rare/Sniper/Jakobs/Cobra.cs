using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Rare.Sniper.Jakobs;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Jakobs
{
    public class Cobra : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(118, 32);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ModContent.ProjectileType<CobraBullet>();
            Item.useAmmo = ModContent.ItemType<SniperAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 200;
            Item.crit = 1;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.reuseDelay = 30;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Utilities.ItemSound(Item, Utilities.Sounds.JakobsSniper, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Sniper Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Bullets explode on impact and deal 100% Explosive Damage", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "Found out about this, I was like, `DAAAMN, Im bringing that gun BACK!`");
        }
    }
}