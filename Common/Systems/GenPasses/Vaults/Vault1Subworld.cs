using SubworldLibrary;
using Terraria;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Magic;

namespace Vaultaria.Common.Systems.GenPasses.Vaults
{
	public class Vault1Subworld : Subworld
	{
		public override int Width => 1000;
		public override int Height => 1000;

		public override bool ShouldSave => false;
		public override bool NoPlayerSaving => true;

		public override List<GenPass> Tasks => new List<GenPass>()
		{
			new Vault1GenPass()
		};

        public override void Update()
        {
            base.Update();

			Main.dayTime = false;
			Main.time = Main.nightLength;
			Main.dayRate = 0;

			Wiring.UpdateMech(); // Make wiring work
			DestroyPressurePlate(); // After crossing the lead-point, destroy the pressure plate to go back
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