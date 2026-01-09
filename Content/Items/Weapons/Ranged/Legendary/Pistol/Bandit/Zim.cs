using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Bandit;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Bandit
{
    public class Zim : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(43, 29);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<ZimBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();
            ItemID.Sets.ShimmerTransformToItem[Item.type] = ModContent.ItemType<Gub>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 40;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.BanditPistol, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Pistol Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Guaranteed Cryo", Utilities.VaultarianColours.Cryo);
            Utilities.RedText(tooltips, Mod, "Would you like to know more?");
        }
    }
}