using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Tediore;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Tediore
{
    public class BabyMaker : ModItem
    {
        private bool altFireMode = false;

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
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 15;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 35;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Item.UseSound = SoundID.Item41;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (altFireMode == true)
            {
                for (int i = 0; i < 29; i++)
                {
                    player.ConsumeItem(ammo.type, false);
                }
            }
            
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2) // Throw
            {
                altFireMode = true;

                Item.damage = 0;
                Item.crit = 0;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ModContent.ProjectileType<BabyMakerGrenade>();

                Item.useTime = 15;
                Item.useAnimation = 15;
                Item.reuseDelay = 15;
                Item.autoReuse = true;
                Item.useTurn = false;

                Item.UseSound = SoundID.Item31;
            }
            else // Shoot
            {
                altFireMode = false;

                Item.damage = 60;
                Item.crit = 16;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ProjectileID.Bullet;

                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.reuseDelay = 45;
                Item.autoReuse = true;
                Item.useTurn = false;

                Item.UseSound = SoundID.Item41;
            }

            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(50)
                .AddIngredient(ItemID.HallowedBar, 25)
                .AddIngredient(ItemID.Revolver, 1)
                .AddIngredient(ItemID.Shotgun, 1)
                .AddIngredient(ItemID.SoulofSight, 25)
                .AddIngredient(ItemID.IllegalGunParts, 5)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses any normal bullet type as ammo"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Who's a widdle gunny-wunny?")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}