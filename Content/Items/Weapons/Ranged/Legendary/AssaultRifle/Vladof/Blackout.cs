using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Vladof
{
    public class Blackout : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 33);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightPurple;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 18;
            Item.shoot = ModContent.ProjectileType<BlackoutBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 50;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 4);
            Utilities.ItemSound(Item, Utilities.Sounds.ETechARSingle, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 7f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Assault Rifle Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Rapidly fires Explosive-Slag rounds", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "It says it in chat.");
        }
    }
}