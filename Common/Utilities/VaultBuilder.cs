using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Setup.Configuration;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.ModLoader;
using StructureHelper.API;
using StructureHelper;
using SubworldLibrary;
using Humanizer;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Vaultaria.Common.Utilities
{
    public static class VaultBuilder
    {
        public static int vault1positionX;
        public static int vault1positionY;

        public static int vault2positionX;
        public static int vault2positionY;

        public static void GenerateVault(string vault, int topLeftX, int topLeftY)
        {
            string path = $"Common/Systems/GenPasses/Vaults/{vault}";
            Point16 pos = new Point16(topLeftX, topLeftY);
            Mod mod = ModContent.GetInstance<Vaultaria>();
            GenFlags flags = GenFlags.NullsKeepGivenSlope;
            Point16 dimensions = StructureHelper.API.Generator.GetStructureDimensions(path, mod);

            CanVaultBeBuilt(path, pos, mod, flags, dimensions, false, topLeftX, topLeftY);
        }

        public static void GenerateVault(string vault)
        {
            string path = $"Common/Systems/GenPasses/Vaults/{vault}";

            Mod mod = ModContent.GetInstance<Vaultaria>();

            Point16 dimensions = StructureHelper.API.Generator.GetStructureDimensions(path, mod);

            int x = Main.rand.Next((int) Main.leftWorld + 200, (int) Main.rightWorld - 200 - dimensions.X);
            int y = Main.rand.Next((int) Main.worldSurface + 100, Main.UnderworldLayer - 100 - dimensions.Y);

            Point16 pos = new Point16(x, y);

            GenFlags flags = GenFlags.NullsKeepGivenSlope;

            CanVaultBeBuilt(path, pos, mod, flags, dimensions, true);
        }

        public static void GenerateVaultBattleGround(string battleGround)
        {
            string path = $"Common/Systems/GenPasses/Vaults/{battleGround}";

            Mod mod = ModContent.GetInstance<Vaultaria>();

            Point16 dimensions = StructureHelper.API.Generator.GetStructureDimensions(path, mod);

            int x = (Main.maxTilesX / 2) - (dimensions.X / 2);
            int y = (Main.maxTilesY / 2) - (dimensions.Y / 2);

            x = Utils.Clamp(x, 10, Main.maxTilesX - dimensions.X - 10);
            y = Utils.Clamp(y, 10, Main.maxTilesY - dimensions.Y - 10);

            if(battleGround == "Vault1BattleGround")
            {
                y = Main.maxTilesY - dimensions.Y - 10; // Make it spawn in hell
            }

            Point16 pos = new Point16(x, y);

            GenFlags flags = GenFlags.NullsKeepGivenSlope;

            // Used in multiplayer so that the network isnt being overloaded with alot of packets. Just make it singleplayer for loading and then make it multiplayer after the world has been made
            int oldNet = Main.netMode;
            Main.netMode = NetmodeID.SinglePlayer;

            StructureHelper.API.Generator.GenerateStructure(path, pos, mod, false, false, flags);

            Main.netMode = oldNet;
        }

        private static void CanVaultBeBuilt(string path, Point16 pos, Mod mod, GenFlags flags, Point16 dimensions, bool spawnRandomly, int topLeftX = 0, int topLeftY = 0)
        {
            bool structGenInBounds = StructureHelper.API.Generator.IsInBounds(path, mod, pos);
            bool notInZone = NotInZone(pos, dimensions);

            string[] pathArray = path.Split('/');
            string vault = pathArray[pathArray.Length - 1];

            if(structGenInBounds && notInZone)
            {
                StructureHelper.API.Generator.GenerateStructure(path, pos, mod, false, false, flags);

                switch(vault)
                {
                    case "Vault1":
                        vault1positionX = pos.X;
                        vault1positionY = pos.Y;
                        Main.tile[pos.X, pos.Y].LiquidAmount = 0; // Idk, i guess it removes the lava at that spot
                        break;
                    case "Vault2":
                        vault2positionX = pos.X;
                        vault2positionY = pos.Y;
                        break;
                }
            }
            else
            {
                if(spawnRandomly == true)
                {
                    GenerateVault(vault);
                }
                else
                {
                    int minX = topLeftX - 200;
                    int maxX = topLeftX + 200 - dimensions.X;

                    int minY = topLeftY - 100;
                    int maxY = topLeftY + 100 - dimensions.Y;

                    if (minX < maxX && minY < maxY)
                    {
                        topLeftX = Main.rand.Next(minX, maxX);
                        topLeftY = Main.rand.Next(minY, maxY);

                        GenerateVault(vault, topLeftX, topLeftY);
                    }
                }
            }
        }

        private static bool NotInZone(Point16 pos, Point16 dimensions)
        {
            for(int i = pos.X; i < pos.X + dimensions.X; i++)
            {
                for(int j = pos.Y; j < pos.Y + dimensions.Y; j++)
                {
                    // 1. World Bounds Check: Check the current iterating coordinates (i, j)
                    if (!WorldGen.InWorld(i, j))
                    {
                        // If any part of the structure is out of world bounds, it's unsafe.
                        return false; 
                    }

                    Tile tile = Main.tile[i, j];

                    // 2. Dangerous Tile Check: Check if the current tile type is in the dangerous list.
                    if(Utilities.badTiles.Contains(tile.TileType) || Utilities.badLiquids.Contains(tile.LiquidType))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}