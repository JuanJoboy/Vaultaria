using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Pearlescent.Shotgun.Hyperion
{
    public class Butcher : ModItem
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
            Item.rare = ItemRarityID.Cyan;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 14;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 110;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 7;
            Item.useAnimation = 7;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Item.UseSound = SoundID.Item36;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 4, 5);

            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            if (Utilities.Randomizer(85f))
            {
                return false;
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(75)
                .AddIngredient(ItemID.FragmentVortex, 50)
                .AddIngredient(ItemID.LunarBar, 25)
                .AddIngredient(ItemID.TacticalShotgun, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine damageLine = tooltips.Find(tip => tip.Name == "Damage");

            if (damageLine != null)
            {
                Player player = Main.LocalPlayer;
                int finalDamage = (int)player.GetTotalDamage(Item.DamageType).ApplyTo(Item.damage);
                damageLine.Text = finalDamage + " x 5 ranged damage";
            }

            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "85% chance to not consume ammo"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Fresh meat!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}