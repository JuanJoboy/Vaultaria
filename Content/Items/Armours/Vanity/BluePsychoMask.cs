using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Armours.Vanity
{
	// This tells tModLoader to look for a texture called PsychoMask_Head, which is the texture on the player
	// and then registers this item to be accepted in head equip slots
	[AutoloadEquip(EquipType.Head)]
	public class BluePsychoMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

		public override void SetDefaults()
		{
            Item.Size = new Vector2(22, 28);

			// Common values for every boss mask
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 75);
			Item.vanity = true;
			Item.maxStack = 1;
		}

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(3)
                .AddIngredient(ItemID.Silk, 5)
                .AddIngredient(ItemID.Bone, 25)
                .AddIngredient(ItemID.BlueDye, 1)
                .AddTile(TileID.Loom)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.RedText(tooltips, Mod, "It's time to go insane!");
        }
	}
}