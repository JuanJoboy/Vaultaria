using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Projectiles.Minions;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Summoner.Sentry
{
    public class SabreTurret : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Type] = true; // For game pads
        }

        public override void SetDefaults()
        {
            // Visual properties
            int size = 12;
            Item.width = size;
            Item.height = size;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Combat properties
            Item.damage = 75;
            Item.DamageType = DamageClass.Summon;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 2;
            Item.knockBack = 2.3f;
            Item.autoReuse = true;
            Item.mana = 20;

            // Other properties
            Item.sellPrice(gold: 20);

            Item.noMelee = true;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<Turret>();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld; // Spawns the minion at the mouse
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(75)
                .AddIngredient(ItemID.FragmentStardust, 50)
                .AddIngredient(ItemID.LunarBar, 25)
                .AddIngredient(ItemID.MoonlordTurretStaff, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Deploy a Sabre Turret that targets and fires upon enemies\n2 Sentries can be summoned"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Shoots:"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip3", "    \tChlorophyte bullets rapidly")
            {
                OverrideColor = new Color(181, 240, 98) // Light Green
            });
            tooltips.Add(new TooltipLine(Mod, "Tooltip4", "    \tA homing slag ball per second")
            {
                OverrideColor = new Color(142, 94, 235) // Purple
            });
            tooltips.Add(new TooltipLine(Mod, "Tooltip5", "    \tA Cluster Rocket per 2 seconds")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Ten years of Dahl military experience at your service.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override bool AllowPrefix(int pre)
        {
            for (int i = 0; i < ElementalProjectile.elementalPrefix.Count; i++)
            {
                if (ElementalProjectile.elementalPrefix.Contains(pre))
                {
                    return false;
                }
            }

            return pre != ModContent.PrefixType<MagicTrickshot>() &&
                   pre != ModContent.PrefixType<MagicDP>();
        }
    }
}