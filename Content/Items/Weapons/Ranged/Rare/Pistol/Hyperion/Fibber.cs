using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion
{
    public class Fibber : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 11f;
            Item.shoot = ModContent.ProjectileType<FibberBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 40;
            Item.crit = 16;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Item.UseSound = SoundID.Item11;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Pistol Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "+50% Love"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip3", "+3000% Damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip4", "Firing Increases Accuracy"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip5", "On tile collision, the initial Projectile splits into 10 Projectiles")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Would I lie to you?")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<RangerTrickshot>();
        }
    }
}