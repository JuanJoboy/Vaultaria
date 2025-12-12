using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using System.Collections;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Systems.GenPasses
{
    public class Vault2GenPass : GenPass
    {
        public Vault2GenPass() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            if(Main.netMode != NetmodeID.MultiplayerClient)
            {
                progress.Message = "Generating Vault"; // Sets the text displayed for this pass

                progress.Set(0.01f);

                Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
                Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds

                VaultBuilder.GenerateVaultBattleGround("Vault2BattleGround");

                progress.Set(1f);   
            }
        }
    }    
}