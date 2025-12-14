using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Projectiles.Summoner.Minion;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.SummonerEffects;

namespace Vaultaria.Content.Items.Weapons.Summoner.Minion
{
    public class BloodwingStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Type] = true; // For game pads
            ItemID.Sets.LockOnIgnoresCollision[Type] = true; // Lets summon go through walls
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(31, 29);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.White;

            // Combat properties
            Item.damage = 10;
            Item.DamageType = DamageClass.Summon;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 2;
            Item.knockBack = 4f;
            Item.autoReuse = true;
            Item.mana = 15;

            // Other properties
            Item.value = Item.buyPrice(silver: 50);
            Item.UseSound = SoundID.Item44;

            Item.noMelee = true;
            Item.shootSpeed = 0f;
            Item.shoot = ModContent.ProjectileType<BloodwingMinion>();
            Item.buffType = ModContent.BuffType<Bloodwing>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld; // Spawns the minion at the mouse
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, player.whoAmI);
            projectile.originalDamage = Item.damage;
            
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(10)
                .AddIngredient(ItemID.BabyBirdStaff, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 0f);
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<MagicTrickshot>() &&
                   pre != ModContent.PrefixType<MagicDP>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Summons a bird to fight for you");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Has a chance to inflict a random element on enemies");
            Utilities.RedText(tooltips, Mod, "Oh, where the hell is... argh, I had a violin somewhere,\nI was gonna play it all sarcastically... goddammit, it was gonna be awesome.\nBLAKE! WHERE'S THE BLOODY VIOLIN?!");
        }
    }
}