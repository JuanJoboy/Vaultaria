using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Vladof
{
    public class LightShow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(80, 30);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 16;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Utilities.ItemSound(Item, Utilities.Sounds.VladofPistol, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 4, 5, 4, 8);

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(50)
                .AddIngredient(ItemID.ChlorophyteBar, 25)
                .AddIngredient(ItemID.Revolver, 1)
                .AddIngredient(ItemID.TacticalShotgun, 1)
                .AddIngredient(ItemID.Ectoplasm, 25)
                .AddIngredient(ItemID.IllegalGunParts, 2)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 4);
            Utilities.Text(tooltips, Mod);
            Utilities.RedText(tooltips, Mod, "Give me some light, away!");
        }
    }
}