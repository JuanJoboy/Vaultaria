using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs
{
    public class LuckCannon : ElementalItem
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
            Item.Size = new Vector2(59, 29);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<LuckCannonBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 55;
            Item.crit = 26;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.reuseDelay = 10;
            Item.autoReuse = false;

            // Other properties
            Item.value = Item.buyPrice(gold: 4);
            Utilities.SetItemSound(Item, Utilities.Sounds.JakobsPistol, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(3f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Pistol Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Fires Explosive rounds", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "Better lucky than good!");
        }
    }
}