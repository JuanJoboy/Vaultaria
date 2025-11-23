using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Dahl
{
    public class NightHawkin : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(68, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 50;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 3;
            Item.useAnimation = 12;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.DahlSMGBurst, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots Cryo bullets during the day and Incendiary bullets at night", Utilities.VaultarianColours.Cryo);
            Utilities.RedText(tooltips, Mod, "Stranger than things.");
        }
    }
}