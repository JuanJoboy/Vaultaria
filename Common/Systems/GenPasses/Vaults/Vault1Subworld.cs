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
using Vaultaria.Content.Items.Consumables.Bags;
using Terraria.Localization;
using Vaultaria.Common.Networking;

namespace Vaultaria.Common.Systems.GenPasses.Vaults
{
	public class Vault1Subworld : Subworld
	{
		public override LocalizedText DisplayName => this.GetLocalization("Vault Of The Warrior");

		public override int Width => 2000;
		public override int Height => 2000;

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => false;

		private bool armouryOpened = false;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new Vault1GenPass()
		};

		public override void OnEnter()
		{
			SubworldSystem.hideUnderworld = false;

			Main.hideUI = false;
		}

        public override void OnExit()
        {
            base.OnExit();

			Main.hideUI = false;
        }

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
				Main.dayTime = false;
				Main.time = Main.nightLength;
				Main.dayRate = 0;

				if(Utilities.Utilities.startedVault1BossRush)
				{
					armouryOpened = false;
				}

				if(VaultMonsterSystem.vaultSkeletron && armouryOpened == false)
				{
					FindArmoury();
					Utilities.Utilities.startedVault1BossRush = false;
					SubworldSystem.noReturn = false;

					if(Main.netMode != NetmodeID.SinglePlayer)
					{
						ModNetHandler.vault.SendBossRushStatus1(Utilities.Utilities.startedVault1BossRush, Main.myPlayer);
						ModNetHandler.vault.SendNoReturn(SubworldSystem.noReturn, Main.myPlayer);
						NetMessage.SendData(MessageID.WorldData);
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

						if(Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileManipulation, number: 4, number2: i, number3: j, number4: 0);
							NetMessage.SendData(MessageID.TileManipulation, number: 4, number2: i + 1, number3: j, number4: 0);
						}

						return;
					}
				}
			}
        }

        private void PlaceInFrozenChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Vault1Bag>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 11;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInIceChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Vault1Bag>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 22;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

		// private void DestroyPressurePlate()
        // {
		// 	bool allPlayersCrossed = false;
		// 	int plateX = -1;
		// 	int plateY = -1;

		// 	for(int i = 0; i < Main.maxTilesX; i++)
		// 	{
		// 		for(int j = 0; j < Main.maxTilesY; j++)
		// 		{
		// 			Tile tile = Main.tile[i, j];

		// 			// if(tile.TileType == TileID.Lead)
        //             // {
		// 			// 	foreach(Player p in Main.ActivePlayers)
        //             //     {
        //             //         if(p.Center.X > i * 16)
        //             //         {
		// 			// 			allPlayersCrossed = true;	
        //             //         }
        //             //     }
        //             // }

		// 			// if(tile.TileType == TileID.Chlorophyte)
        //             // {
        //             //     plateX = i;
		// 			// 	plateY = j - 1; // Coordinates of the pressure plate to destroy
        //             // }

		// 			if(tile.TileType == TileID.Chlorophyte && WorldGen.InWorld(i, j))
        //             {
		// 				plateX = i;
		// 				plateY = j - 1;

		// 				foreach(Player p in Main.ActivePlayers)
        //                 {
        //                     if(p.Center.X > i * 16)
        //                     {
		// 						allPlayersCrossed = true;	
        //                     }
        //                 }
        //             }
		// 		}
		// 	}

		// 	if(allPlayersCrossed == true && plateX != -1 && plateY != -1)
        //     {
        //         WorldGen.KillTile(plateX , plateY, false, false, true);
        //     }
        // }

        // private void ActuateTiles()
        // {
        //     for(int i = 0; i < Main.maxTilesX; i++)
		// 	{
		// 		for(int j = 0; j < Main.maxTilesY; j++)
		// 		{
		// 			Tile tile = Main.tile[i, j];

		// 			if(tile.TileType == TileID.Copper && WorldGen.InWorld(i, j))
		// 			{
        //                 if(tile.IsActuated == false)
        //                 {
        //                     Wiring.Actuate(i, j); // Actuate the tiles if it isn't already so that the player can go through
        //                 }
		// 			}
		// 		}
		// 	}
        // }
	}
}