using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs
{
    public class Oracle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 50;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.reuseDelay = 5;
            Item.autoReuse = false;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.JakobsPistol, 60);
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(3f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Critical hits either hit the same target again or ricochet to the closest enemy", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "You seek guidance?");
            Utilities.CursedText(tooltips, Mod, "Exodus");
        }
    }
}