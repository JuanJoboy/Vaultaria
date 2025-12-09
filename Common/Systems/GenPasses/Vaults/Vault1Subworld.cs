using SubworldLibrary;
using Terraria;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Magic;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Jakobs;

namespace Vaultaria.Common.Systems.GenPasses.Vaults
{
	public class Vault1Subworld : Subworld
	{
		public override int Width => 1000;
		public override int Height => 1000;

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => false;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new Vault1GenPass()
		};

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
					// if (WorldGen.genRand.NextBool(3))
                    // {
					// 	continue;
                    // }

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

        private void PlaceInGoldenChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<FlushRifle>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 1;

            PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        public override void Update()
        {
            base.Update();
			Player player = Main.LocalPlayer;

			Main.dayTime = false;
			Main.time = Main.nightLength;
			Main.dayRate = 0;

			Wiring.UpdateMech(); // Make wiring work
			DestroyPressurePlate(); // After crossing the lead-point, destroy the pressure plate to go back

			Utilities.Utilities.SpawnPreHardmodeBosses(player);
        }

        public override void OnLoad()
        {
            base.OnLoad();

			FindPlatinum(out int x, out int y);

            if(x != -1 && y != -1)
            {
                Main.spawnTileX = x;
                Main.spawnTileY = y - 2; // Set spawn point to the start instead of the main arena
            }

			ActuateTiles();

			PlaceInGoldenChests();
        }

        private void FindPlatinum(out int x, out int y)
        {
            x = -1;
            y = -1;

            for(int i = 0; i < Main.maxTilesX; i++)
			{
				for(int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];

					if(tile.TileType == TileID.Platinum)
					{
						x = i;
						y = j;

                        return;
					}
				}
			}
        }

		private void DestroyPressurePlate()
        {
			bool allPlayersCrossed = false;
			int plateX = -1;
			int plateY = -1;

			for(int i = 0; i < Main.maxTilesX; i++)
			{
				for(int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];

					// if(tile.TileType == TileID.Lead)
                    // {
					// 	foreach(Player p in Main.ActivePlayers)
                    //     {
                    //         if(p.Center.X > i * 16)
                    //         {
					// 			allPlayersCrossed = true;	
                    //         }
                    //     }
                    // }

					// if(tile.TileType == TileID.Chlorophyte)
                    // {
                    //     plateX = i;
					// 	plateY = j - 1; // Coordinates of the pressure plate to destroy
                    // }

					if(tile.TileType == TileID.Chlorophyte)
                    {
						plateX = i;
						plateY = j - 1;

						foreach(Player p in Main.ActivePlayers)
                        {
                            if(p.Center.X > i * 16)
                            {
								allPlayersCrossed = true;	
                            }
                        }
                    }
				}
			}

			if(allPlayersCrossed == true && plateX != -1 && plateY != -1)
            {
                WorldGen.KillTile(plateX , plateY, false, false, true);
            }
        }

        private void ActuateTiles()
        {
            for(int i = 0; i < Main.maxTilesX; i++)
			{
				for(int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];

					if(tile.TileType == TileID.Copper)
					{
                        if(tile.IsActuated == false)
                        {
                            Wiring.Actuate(i, j); // Actuate the tiles if it isn't already so that the player can go through
                        }
					}
				}
			}
        }
	}
}