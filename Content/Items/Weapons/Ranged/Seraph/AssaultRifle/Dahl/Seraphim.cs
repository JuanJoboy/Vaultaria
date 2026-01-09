using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Ammo.Seraph.AssaultRifle.Vladof;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Seraph.AssaultRifle.Dahl;

namespace Vaultaria.Content.Items.Weapons.Ranged.Seraph.AssaultRifle.Dahl
{
    public class Seraphim : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(71, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Pink;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 30f;
            Item.shoot = ModContent.ProjectileType<SeraphimBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 28;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 6;
            Item.useAnimation = 18;
            Item.reuseDelay = 15;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.DahlARBurst, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 4f);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Assault Rifle Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Fires a high burst of Incendiary bullets", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "Holy? Holy? Holey!");
        }
    }
}