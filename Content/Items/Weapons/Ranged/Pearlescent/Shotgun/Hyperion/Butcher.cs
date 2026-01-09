using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

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
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(80, 29);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Cyan;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 14;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 64;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Utilities.ItemSound(Item, Utilities.Sounds.HyperionShotgun, 30);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 5, 5, 1, 11);
            
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
            return new Vector2(-16f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 5);
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "tooltip2", "85% chance to not consume ammo");
            Utilities.RedText(tooltips, Mod, "Fresh meat!");
        }
    }
}