using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.AssaultRifle.Vladof
{
    public class Shredifier : ModItem
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
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 18;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 50;
            Item.crit = 21;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 4);
            Item.UseSound = SoundID.Item41;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);
            Projectile.NewProjectile(source, position - new Vector2(0, -7), velocity, type, damage, knockback, player.whoAmI);

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(50)
                .AddIngredient(ItemID.ChlorophyteBar, 25)
                .AddIngredient(ItemID.ChainGun, 1)
                .AddIngredient(ItemID.SoulofNight, 25)
                .AddIngredient(ItemID.IllegalGunParts, 5)
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
                damageLine.Text = finalDamage + " x 2 ranged damage";
            }

            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses any normal bullet type as ammo\n+100% Fire rate"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Speed kills.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}