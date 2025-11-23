using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Maliwan
{
    public class Hellfire : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(58, 30);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 18;
            Item.shoot = ModContent.ProjectileType<HellfireBullet>();
            Item.useAmmo = ModContent.ItemType<SubmachineGunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 11;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.MaliwanSMG, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 5f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(20)
                .AddIngredient(ItemID.LivingFireBlock, 10)
                .AddIngredient(ItemID.PhoenixBlaster, 1)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses SMG Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Rapidly shoots Incendiary bullets", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "We don't need no fire…");
        }
    }
}