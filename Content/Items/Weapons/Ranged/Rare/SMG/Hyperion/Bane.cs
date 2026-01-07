using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Hyperion;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Hyperion
{
    public class Bane : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(64, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 18;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 20;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.Bane, 30);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-18f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Screams while firing and greatly reduces movement speed", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "In Spain, stays mainly on the plain.");
        }
    }
}