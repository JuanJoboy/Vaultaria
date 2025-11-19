using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Sniper.Maliwan;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Maliwan
{
    public class Pimpernel : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1.2f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<PimpernelBullet>();
            Item.useAmmo = ModContent.ItemType<SniperAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 10;
            Item.crit = 1;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.reuseDelay = 48;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 3);
            Utilities.ItemSound(Item, Utilities.Sounds.MaliwanSniper, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Sniper Ammo"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Shoots a Bullet that spawns an additional bullet that rises into the air. Additional bullet splits into 5 more bullets in a star shaped pattern. These 5 Bullets can hit enemies twice, but the second hit deals 50% less damage. 50% Splash Damage (gets Reaper Buff, but no Grenade Damage Buff)"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Sink me!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}