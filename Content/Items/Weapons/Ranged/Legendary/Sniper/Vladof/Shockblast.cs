using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Vladof;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Vladof
{
    public class Shockblast : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.LightPurple;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 60f;
            Item.useAmmo = ModContent.ItemType<SniperAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 70;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 15;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click
            {
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 60f;
                Item.shoot = ModContent.ProjectileType<ShockblastExBullet>();
                Item.useAmmo = ModContent.ItemType<SniperAmmo>();
                Utilities.ItemSound(Item, Utilities.Sounds.ETechLauncher, 60);

                Item.damage = 200;
                Item.crit = 0;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.reuseDelay = 30;
                Item.autoReuse = true;
                Item.useTurn = false;
            }
            else // Left-click
            {
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 60f;
                Item.shoot = ModContent.ProjectileType<ShockblastElBullet>();
                Item.useAmmo = ModContent.ItemType<SniperAmmo>();
                Utilities.ItemSound(Item, Utilities.Sounds.ETechAR, 60);

                Item.damage = 70;
                Item.crit = 0;
                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.reuseDelay = 0;
                Item.autoReuse = true;
                Item.useTurn = false;
            }

            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 2; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "tooltip1", "Consumes 3 Sniper Ammo per shot");
            Utilities.Text(tooltips, Mod, "tooltip2", "Left-click to shoot fast Shock e-tech rounds", Utilities.VaultarianColours.Shock);
            Utilities.Text(tooltips, Mod, "tooltip3", "Right-click to shoot more powerful Explosive-Shock rounds", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "Blast them to smithereens!");
        }
    }
}