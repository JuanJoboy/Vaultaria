using Terraria.ModLoader;
using Vaultaria.Common.Utilities; // To access your VaultBuilder class
using Terraria.DataStructures;
using Terraria.WorldBuilding;
using Terraria;
using System;
using Terraria.ModLoader.IO;
using Terraria.ID;
using static System.Math;
using Vaultaria.Common.Players;
using System.IO;

namespace Vaultaria.Common.Systems.GenPasses
{
    public class WorldGenerator : ModSystem
    {
        public static bool pedestalInVault1 = false;
        public static bool pedestalInVault2 = false;

        // This hook runs after the main world generation is complete.
        public override void PostWorldGen()
        {
            base.PostWorldGen();

            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                VaultBuilder.GenerateVault("Vault1", Main.maxTilesX - GenVars.dungeonLocation, Main.UnderworldLayer + 50);
                VaultBuilder.GenerateVault("Vault2", (GenVars.snowOriginLeft + GenVars.snowOriginRight) / 2, GenVars.snowTop - 95);

                // VaultBuilder.GenerateVault("Vault1", Main.spawnTileX, Main.spawnTileY); // Find a suitable tile coordinate (Point16) for the top-left of the structure.
                // VaultBuilder.GenerateVault("Vault2", Main.spawnTileX, Main.spawnTileY - 100); // Find a suitable tile coordinate (Point16) for the top-left of the structure.   
            }
        }

        // When a new player joins the world after a pedestal has been activated, this helps sync it for them so that it isn't deactivated
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(pedestalInVault1);
            writer.Write(pedestalInVault2);
        }

        public override void NetReceive(BinaryReader reader)
        {
            pedestalInVault1 = reader.ReadBoolean();
            pedestalInVault2 = reader.ReadBoolean();
        }

        public override void SaveWorldData(TagCompound tag)
        {
            base.SaveWorldData(tag);

            // Save the X and Y coordinates of the generated vault.
            // VaultBuilder.positionX and VaultBuilder.positionY are set when the vault successfully generates in PostWorldGen.
            tag.Add("Vault1X", VaultBuilder.vault1positionX);
            tag.Add("Vault1Y", VaultBuilder.vault1positionY);

            tag.Add("Vault2X", VaultBuilder.vault2positionX);
            tag.Add("Vault2Y", VaultBuilder.vault2positionY);

            tag["pedestalInVault1"] = pedestalInVault1;
            tag["pedestalInVault2"] = pedestalInVault2;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            base.LoadWorldData(tag);

            // Load the coordinates from the saved data.
            // The "Get" methods safely return 0 if the key isn't found (e.g., loading an old world).
            VaultBuilder.vault1positionX = tag.GetInt("Vault1X");
            VaultBuilder.vault1positionY = tag.GetInt("Vault1Y");

            VaultBuilder.vault2positionX = tag.GetInt("Vault2X");
            VaultBuilder.vault2positionY = tag.GetInt("Vault2Y");

            pedestalInVault1 = tag.GetBool("pedestalInVault1");
            pedestalInVault2 = tag.GetBool("pedestalInVault2");
        }
        
        // This resets the flag when a new world is created
        public override void ClearWorld()
        {
            pedestalInVault1 = false;
            pedestalInVault2 = false;
        }
    }
}