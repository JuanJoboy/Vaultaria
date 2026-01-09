using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Common.Pistol.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Maliwan
{
    public class Aegis : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(45, 29);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<AegisBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 0.5f;
            Item.damage = 16;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.reuseDelay = 20;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.MaliwanPistol, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "ToolTip1", "Uses Pistol Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots Shock bullets", Utilities.VaultarianColours.Shock);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Found in Wooden Chests", Utilities.VaultarianColours.Information);
        }
    }
}