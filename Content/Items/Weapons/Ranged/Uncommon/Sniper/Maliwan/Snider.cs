using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Uncommon.Sniper.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Sniper.Maliwan
{
    public class Snider : ElementalItem
    {
        protected override Utilities.Sounds[] ItemSounds => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(138, 30);
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Green;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ModContent.ProjectileType<SniderBullet>();
            Item.useAmmo = ModContent.ItemType<SniperAmmo>();

            // Combat properties
            Item.knockBack = 2f;
            Item.damage = 30;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 30;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 10);
            Utilities.SetItemSound(Item, Utilities.Sounds.MaliwanSniper, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-17f, -3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Sniper Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots Incendiary Bullets", Utilities.VaultarianColours.Incendiary);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Found in Golden Chests", Utilities.VaultarianColours.Information);
        }
    }
}