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
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Maliwan;

namespace Vaultaria.Common.Systems.GenPasses.Vaults
{
	public class Vault2Subworld : Subworld
	{
		public override int Width => 2000;
		public override int Height => 2000;

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => false;

		private bool armouryOpened = false;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new Vault2GenPass()
		};

        public override void OnLoad()
        {
            base.OnLoad();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
    			FindSpawn();

				PlaceInFrozenChests();
				PlaceInIceChests();
            }
        }

        public override void Update()
        {
            base.Update();

			if(Main.netMode != NetmodeID.MultiplayerClient)
            {
				foreach(Player player in Main.ActivePlayers)
				{
					Main.dayTime = false;
					Main.time = Main.nightLength;
					Main.dayRate = 0;

					Utilities.Utilities.SpawnHardmodeBosses(player);

					if(VaultMonsterSystem.vaultQueenSlime && armouryOpened == false)
					{
						FindArmoury();
					}
				}
            }
		}

        private void FindSpawn()
        {
            for(int i = 0; i < Main.maxTilesX; i++)
			{
				for(int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];

					if(tile.TileType == TileID.Mudstone && WorldGen.InWorld(i, j))
					{
						if(i != -1 && j != -1)
						{
							Main.spawnTileX = i;
							Main.spawnTileY = j - 2;
						}

                        return;
					}
				}
			}
        }

        private void FindArmoury()
        {
            for(int i = 0; i < Main.maxTilesX; i++)
			{
				for(int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];

					if(tile.TileType == TileID.LeadBrick && WorldGen.InWorld(i, j))
					{
                		WorldGen.KillTile(i , j, false, false, true);
                		WorldGen.KillTile(i + 1, j, false, false, true);
						armouryOpened = true;
                        return;
					}
				}
			}
        }

        private void PlaceInFrozenChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<FlushRifle>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 11;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInIceChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Aegis>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 22;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }
	}
}