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
    public class WilhelmsCombatDrones : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Type] = true; // For game pads
            ItemID.Sets.LockOnIgnoresCollision[Type] = true; // Lets summon go through walls
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(31, 29);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;

            // Combat properties
            Item.damage = 30;
            Item.DamageType = DamageClass.Summon;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 2;
            Item.knockBack = 4f;
            Item.autoReuse = true;
            Item.mana = 20;

            // Other properties
            Item.value = Item.buyPrice(silver: 50);

            Item.noMelee = true;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<Wolf>();
            Item.shoot = ModContent.ProjectileType<Saint>();
            Item.buffType = ModContent.BuffType<WolfAndSaint>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld; // Spawns the minion at the mouse
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Saint>(), damage, Main.myPlayer);
            Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Wolf>(), damage, Main.myPlayer);
            
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(25)
                .AddIngredient(ItemID.OpticStaff, 1)
                .AddIngredient(ItemID.SoulofLight, 20)
                .AddIngredient(ItemID.SoulofNight, 20)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Summons Wolf and Saint to fight for you");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Wolf attacks enemies", Utilities.VaultarianColours.Radiation);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Saint regenerates your health", Utilities.VaultarianColours.Healing);
            Utilities.RedText(tooltips, Mod, "You killed Wilhelm...?");
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<MagicTrickshot>() &&
                   pre != ModContent.PrefixType<MagicDP>();
        }
    }
}