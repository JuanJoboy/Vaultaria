using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Ammo.Effervescent.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Items.Weapons.Ranged.Effervescent.Pistol.Jakobs
{
    public class Prototype2599 : ModItem
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
            Item.rare = ItemRarityID.Master;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<Prototype2599Bullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 60;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 45;
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
            for (int i = 0; i < 2; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2) // Burst Fire (alt)
            {
                altFireMode = true;

                Item.damage = 15;
                Item.crit = 1;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ModContent.ProjectileType<Prototype2599Bullet>();

                Item.useTime = 12;
                Item.useAnimation = 36;
                Item.reuseDelay = 30;
                Item.autoReuse = true;
                Item.useTurn = false;

                Item.UseSound = SoundID.Item31;
            }
            else // Quad Shot (normal)
            {
                altFireMode = false;

                Item.damage = 60;
                Item.crit = 16;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ModContent.ProjectileType<Prototype2599Bullet>();

                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.reuseDelay = 45;
                Item.autoReuse = true;
                Item.useTurn = false;

                Item.UseSound = SoundID.Item41;
            }

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(Utilities.Randomizer(25f))
            {
                Utilities.CloneShots(player, source, position, velocity * 2f, type, damage, knockback, 2, 10, 4, 8);
            }

            if (altFireMode == false)
            {
                Utilities.CloneShots(player, source, position, velocity * 2f, type, damage, knockback, 4, 5, 4, 8);

                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(50)
                .AddIngredient(ItemID.HallowedBar, 25)
                .AddIngredient(ItemID.RainbowBrick, 20)
                .AddIngredient(ItemID.Ichor, 5)
                .AddIngredient(ItemID.PhoenixBlaster, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -5f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine damageLine = tooltips.Find(tip => tip.Name == "Damage");

            if(altFireMode == false)
            {
                if (damageLine != null)
                {
                    Player player = Main.LocalPlayer;
                    int finalDamage = (int)player.GetTotalDamage(Item.DamageType).ApplyTo(Item.damage);
                    damageLine.Text = finalDamage + " x 4 ranged damage";
                }
            }

            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses Pistol Ammo\nFires as fast as you can pull the trigger... but not too fast"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "An ode to Maxine")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
            tooltips.Add(new TooltipLine(Mod, "Cyan Text", "Developer Item")
            {
                OverrideColor = new Color(129, 247, 247) // Cyan
            });
        }
    }
}