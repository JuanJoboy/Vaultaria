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
    public class Gub : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<GubBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 29;
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
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Pistol Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Shoots Corrosive bullets")
            {
                OverrideColor = new Color(136, 235, 94) // Light Green
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Abt natural.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "The Curse of the Gub")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}