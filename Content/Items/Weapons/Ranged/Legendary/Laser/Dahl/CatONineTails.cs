using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Dahl
{
    public class CatONineTails : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 25);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ProjectileID.HeatRay;
            Item.mana = 10;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 50;
            Item.crit = 6;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 5;
            Item.useAnimation = 30;
            Item.reuseDelay = 5;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.ETechSMGBurst, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "ToolTip1", "Shoots piercing Incendiary lasers that ricochet on surface impact", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "The cat's out of the bag.");
        }
    }
}