using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Sniper.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Maliwan
{
    public class Pimpernel : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(123, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 15;
            Item.shoot = ModContent.ProjectileType<PimpernelBullet>();
            Item.useAmmo = ModContent.ItemType<SniperAmmo>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 70;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.reuseDelay = 17;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 3);
            Utilities.ItemSound(Item, Utilities.Sounds.MaliwanSniper, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Sniper Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Spawns 4 pellets on impact", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Sink me!");
        }
    }
}