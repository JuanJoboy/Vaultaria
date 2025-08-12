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
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1.1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 12;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.damage = 40;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = false;


            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Item.UseSound = SoundID.Item40;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click melee
            {
                Item.DamageType = DamageClass.Melee;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = false;
                Item.shootSpeed = 0f;
                Item.shoot = ProjectileID.None;
                Item.useAmmo = AmmoID.None;
                Item.UseSound = SoundID.Item1;

                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.damage = 220;
                Item.crit = 0;
                Item.reuseDelay = 0;
                Item.autoReuse = true;
                Item.useTurn = false;

            }
            else // Left-click ranged
            {
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 12;
                Item.shoot = ProjectileID.Bullet;
                Item.useAmmo = AmmoID.Bullet;
                Item.UseSound = SoundID.Item40;

                Item.damage = 40;
                Item.crit = 0;
                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.reuseDelay = 0;
                Item.autoReuse = true;
                Item.useTurn = false;

            }

            return base.CanUseItem(player);
        }
        
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(75)
                .AddIngredient(ItemID.FragmentSolar, 50)
                .AddIngredient(ItemID.LunarBar, 25)
                .AddIngredient(ItemID.Ectoplasm, 25)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 5f);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses any normal bullet type as ammo\n+200% Melee Damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Right-Click to do a melee attack"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "As I end the refrain, thrust home.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
            tooltips.Add(new TooltipLine(Mod, "Curse", "Curse of the Porcelain Fist!\n(Take 3x more damage)")
            {
                OverrideColor = new Color(0, 249, 199) // Cyan
            });
        }
    }
}