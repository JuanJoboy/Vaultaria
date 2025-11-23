using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Ranged.Common.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Tediore;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Shotgun.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Common.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Shotgun.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Sniper.Maliwan;

namespace Vaultaria.Common.Systems
{
	public class ChestItemWorldGen : ModSystem
	{
		// We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
		public override void PostWorldGen()
        {
            PlaceInWoodenChests();
            
            PlaceInGoldenChests();
            
            PlaceInShadowChests();
            
            PlaceInJungleChests();
            
            PlaceInFrozenChests();
            
            PlaceInSkyWareChests();
            
            PlaceInWaterChests();
        }

        private void PlaceItemsInChest(int[] itemsToPlaceInChest, int itemsToPlaceInChestChoice, int itemsPlaced, int maxItems, int internalChestID)
        {
			// Loop over all the chests
			for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
				Chest chest = Main.chest[chestIndex];

				if (chest == null)
                {
					continue;
				}

				Tile chestTile = Main.tile[chest.x, chest.y];
				// We need to check if the current chest is the Frozen Chest. We need to check that it exists and has the TileType and TileFrameX values corresponding to the Frozen Chest.
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Frozen Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. An alternate approach is to check the wiki and looking for the "Internal Tile ID" section in the infobox: https://terraria.wiki.gg/wiki/Frozen_Chest
				if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == internalChestID * 36)
                {
					// We have found a Frozen Chest
					// If we don't want to add one of the items to every Frozen Chest, we can randomly skip this chest with a 33% chance.
					if (WorldGen.genRand.NextBool(3))
                    {
						continue;
                    }

					// Next we need to find the first empty slot for our item
					for (int inventoryIndex = 0; inventoryIndex < Chest.maxItems; inventoryIndex++)
                    {
						if (chest.item[inventoryIndex].type == ItemID.None)
                        {
							// Place the item
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInChest[itemsToPlaceInChestChoice]);
							// Decide on the next item that will be placed.
							itemsToPlaceInChestChoice = (itemsToPlaceInChestChoice + 1) % itemsToPlaceInChest.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(WorldGen.genRand.Next(itemsToPlaceInChest));
							itemsPlaced++;
							break;
						}
					}
				}

				// Once we've placed as many items as we wanted, break out of the loop
				if (itemsPlaced >= maxItems)
                {
					break;
				}
			}
        }

        private void PlaceInWoodenChests()
        {
			// Place some additional items in Wooden Chests:
			// These are the new items we will place.
			int[] itemsToPlaceInChest = [ModContent.ItemType<LumpyRoot>(), ModContent.ItemType<Aegis>(), ModContent.ItemType<Handgun>(), ModContent.ItemType<Skatergun>(), ModContent.ItemType<SmoothFox>()];
			// This variable will help cycle through the items so that different Wooden Chests get different items
			int itemsToPlaceInChestChoice = 0;
			// Rather than place items in each chest, we'll place up to the array's length * 30 (30 of each). 
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, 0);
        }

        private void PlaceInGoldenChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<FlushRifle>(), ModContent.ItemType<Snider>(), ModContent.ItemType<ThreeWayHulk>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length / 3;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, 1);
        }

        private void PlaceInShadowChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Quad>(), ModContent.ItemType<OrphanMaker>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length * 5;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, 4);
        }

        private void PlaceInJungleChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Revenant>(), ModContent.ItemType<InspiringTransaction>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length * 20;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, 8);
        }

        private void PlaceInFrozenChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<TooScoops>(), ModContent.ItemType<NightSniper>(), ModContent.ItemType<Carbine>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length * 8;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, 11);
        }

        private void PlaceInSkyWareChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<OlPainful>(), ModContent.ItemType<Boomacorn>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, 13);
        }

        private void PlaceInWaterChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Lascaux>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, 17);
        }
	}
}