using Terraria.ModLoader;
using Vaultaria.Common.Utilities; // To access your VaultBuilder class
using Terraria.DataStructures;
using Terraria.WorldBuilding;
using Terraria;
using System;
using Terraria.ModLoader.IO;

namespace Vaultaria.Common.Systems.GenPasses
{
    public class WorldGenerator : ModSystem
    {
        public static bool pedestalInVault1 = false;

        // This hook runs after the main world generation is complete.
        public override void PostWorldGen()
        {
            base.PostWorldGen();

            // VaultBuilder.GenerateVault("Vault1");

            VaultBuilder.GenerateVault("Vault1", Main.spawnTileX, Main.spawnTileY); // Find a suitable tile coordinate (Point16) for the top-left of the structure.
        }

        public override void SaveWorldData(TagCompound tag)
        {
            base.SaveWorldData(tag);

            // Save the X and Y coordinates of the generated vault.
            // VaultBuilder.positionX and VaultBuilder.positionY are set when the vault successfully generates in PostWorldGen.
            tag.Add("Vault1X", VaultBuilder.positionX);
            tag.Add("Vault1Y", VaultBuilder.positionY);

            tag["pedestalInVault1"] = pedestalInVault1;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            base.LoadWorldData(tag);

            // Load the coordinates from the saved data.
            // The "Get" methods safely return 0 if the key isn't found (e.g., loading an old world).
            VaultBuilder.positionX = tag.GetInt("Vault1X");
            VaultBuilder.positionY = tag.GetInt("Vault1Y");

            pedestalInVault1 = tag.GetBool("pedestalInVault1");
        }
        
        // This resets the flag when a new world is created
        public override void ClearWorld()
        {
            pedestalInVault1 = false;
        }
    }
}