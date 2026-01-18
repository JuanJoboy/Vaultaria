// using Microsoft.Xna.Framework;
// using Terraria;
// using Terraria.ID;
// using Terraria.ModLoader;
// using Vaultaria.Common.Utilities;
// using System.Collections.Generic;

// namespace Vaultaria.Content.Items.Armours.Vanity
// {
// 	// This tells tModLoader to look for a texture called PsychoMask_Head, which is the texture on the player
// 	// and then registers this item to be accepted in head equip slots
// 	[AutoloadEquip(EquipType.Head)]
// 	public class PsychoMask : ModItem
// 	{
//         public override void SetStaticDefaults()
//         {
//             Item.ResearchUnlockCount = 1;
//         }

// 		public override void SetDefaults()
// 		{
//             Item.Size = new Vector2(22, 28);

// 			// Common values for every boss mask
// 			Item.rare = ItemRarityID.Blue;
// 			Item.value = Item.sellPrice(silver: 75);
// 			Item.vanity = true;
// 			Item.maxStack = 1;
// 		}

//         public override void ModifyTooltips(List<TooltipLine> tooltips)
//         {
//             Utilities.RedText(tooltips, Mod, "No, no, no... I can't die like this...\nNot when I'm so close... And not at the hands of a filthy bandit.\nI could have saved this planet; I could have actually restored order!\nAnd I wasn't supposed to die... by the hands of a CHILD KILLING PSYCHOPATH!!\nYou're a savage! You're a maniac, you are a bandit,\nAND I AM THE GODDAMN HERO!!");
//         }
// 	}
// }