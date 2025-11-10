using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.PotionEffects;

namespace Vaultaria.Content.Items.Potions
{
    public class DeceptionPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
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

            // Item.potion = true;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<DeceptionBuff>();
            Item.buffTime = 360;

            // Other properties
            Item.value = Item.buyPrice(silver: 1);
            Item.UseSound = SoundID.Item3;
        }

        public override bool? UseItem(Player player)
        {
            player.PlayerFrame();

            return base.UseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater, 1)
                .AddIngredient(ItemID.Daybloom, 1)
                .AddIngredient(ItemID.Shiverthorn, 1)
                .AddIngredient(ItemID.GlowingMushroom, 1)
                .AddTile(TileID.Bottles)
                .Register();
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "200% Increased Gun Damage & 300% increased Melee Damage while in Deception")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Your eyes deceive you\nAn illusion fools you all\nI move for the kill.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}