using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof
{
    public class Rapier : ModItem
    {
        private bool isInMeleeMode = false;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1.3f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 15;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.damage = 15;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Item.UseSound = SoundID.Item11;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click melee
            {
                isInMeleeMode = true;

                Item.DamageType = DamageClass.Melee;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = false;
                Item.shootSpeed = 0f;
                Item.shoot = ProjectileID.None;
                Item.useAmmo = AmmoID.None;
                Item.UseSound = SoundID.Item23;

                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.damage = 400;
                Item.reuseDelay = 0;
                Item.autoReuse = true;
                Item.useTurn = true;
            }
            else // Left-click ranged
            {
                isInMeleeMode = false;

                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 4f;
                Item.shoot = ProjectileID.Bullet;
                Item.useAmmo = AmmoID.Bullet;
                Item.UseSound = SoundID.Item1;

                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.reuseDelay = 1;
                Item.autoReuse = true;
                Item.useTurn = true;
            }

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            return !isInMeleeMode; // Only shoot if not in melee mode
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(9)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 0f);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+200% Melee Damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Right-Click to do a melee attack"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "As I end the refrain, thrust home.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
            tooltips.Add(new TooltipLine(Mod, "Curse", "Curse of the Porcelain Fist!")
            {
                OverrideColor = new Color(0, 249, 199) // Cyan
            });
        }
    }
}