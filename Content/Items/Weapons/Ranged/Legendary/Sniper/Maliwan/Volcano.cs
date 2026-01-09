using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Maliwan
{
    public class Volcano : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(118, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 30f;
            Item.shoot = ModContent.ProjectileType<VolcanoBullet>();
            Item.useAmmo = ModContent.ItemType<SniperAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 30;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 15;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.MaliwanSniper, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "tooltip1", "Uses Sniper Ammo");
            Utilities.Text(tooltips, Mod, "tooltip2", "Shoots bullets that explode on impact, dealing Incendiary Damage.", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "Pele humbly requests a sacrifice, if it's not too much trouble.");
        }
    }
}