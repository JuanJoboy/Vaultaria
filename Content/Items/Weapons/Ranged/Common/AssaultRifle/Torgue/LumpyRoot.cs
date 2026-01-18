using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using Vaultaria.Content.Projectiles.Ammo.Common.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.AssaultRifle.Torgue
{
    public class LumpyRoot : ElementalItem
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
            Item.Size = new Vector2(92, 30);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 14;
            Item.shoot = ModContent.ProjectileType<LumpyBullet>();
            Item.useAmmo = ModContent.ItemType<AssaultRifleAmmo>();

            // Combat properties
            Item.knockBack = 0f;
            Item.damage = 15;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 1);
            Utilities.SetItemSound(Item, Utilities.Sounds.TorgueAR, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13f, 3.5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Assault Rifle Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots Explosive bullets", Utilities.VaultarianColours.Explosive);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Found in Wooden Chests", Utilities.VaultarianColours.Information);
        }
    }
}