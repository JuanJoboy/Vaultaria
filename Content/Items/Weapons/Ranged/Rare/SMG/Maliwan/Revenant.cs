using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Projectiles.Ammo.Rare.SMG.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Maliwan
{
    public class Revenant : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 18;
            Item.shoot = ModContent.ProjectileType<RevenantBullet>();
            Item.useAmmo = ModContent.ItemType<SubmachineGunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 14;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.MaliwanSMG, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses SMG Ammo"));
        }
    }
}