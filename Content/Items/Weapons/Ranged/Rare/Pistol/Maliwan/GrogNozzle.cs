using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Maliwan;
using Vaultaria.Content.Buffs;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Maliwan
{
    public class GrogNozzle : ModItem
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
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<GrogBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 5;
            Item.crit = 100;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Item.UseSound = SoundID.Item11;
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
            return new Vector2(6f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(
            source,
            position,
            velocity,
            ModContent.ProjectileType<GrogBullet>(),
            damage,
            knockback,
            player.whoAmI
            );

            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+50% chance to apply Ichor"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Hand over the keys, Sugar...")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void HoldItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<DrunkEffect>()))
            {
                if(Main.rand.Next(0, 1000) == 500)
                {
                    player.AddBuff(ModContent.BuffType<DrunkEffect>(), 30000); // 0.1% chance to apply a drunk effect on yourself for 5 seconds
                }
            }
        }
    }
}