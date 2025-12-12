using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.PotionEffects;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Potions
{
    public class GammaBurstPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(30, 30);
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 9999;

            // Combat properties
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.noMelee = true;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.potion = false;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<GammaBurstBuff>();
            Item.buffTime = 600;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Item.UseSound = SoundID.Item3;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater, 1)
                .AddIngredient(ItemID.Ichor, 1)
                .AddIngredient(ItemID.FallenStar, 1)
                .AddIngredient(ItemID.GlowingMushroom, 1)
                .AddTile(TileID.Bottles)
                .Register();
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Doubles minion damage and size");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Causes minion attacks to deal additional Radiation damage", Utilities.VaultarianColours.Radiation);
            Utilities.RedText(tooltips, Mod, "Good Boy.");
        }
    }
}