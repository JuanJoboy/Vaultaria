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
    public class DeceptionPotion : ModItem
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

            Item.potion = true;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<DeceptionBuff>();
            Item.buffTime = 420;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.Deception, 500);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+100% Increased Ranged Damage & +150% increased Melee Damage while in Deception\n300% Increased Melee Damage while holding Zero's Sword", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Your eyes deceive you\nAn illusion fools you all\nI move for the kill.");
        }
    }
}