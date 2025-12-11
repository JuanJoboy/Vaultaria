using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Systems.GenPasses.Vaults;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Globals
{
    public class VaultTiles : GlobalTile
    {
        public override bool CanExplode(int i, int j, int type)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j))
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i, j))
            {
                return false;
            }

            return base.CanExplode(i, j, type);
        }

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            // Remove once you know what to do with the subworlds
            // if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            // {
            //     Tile tile = Main.tile[i, j];

            //     if(tile.HasActuator)
            //     {
            //         return true;
            //     }
            //     else
            //     {
            //         return false;
            //     }
            // }

            if(Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j))
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i, j))
            {
                return false;
            }

            return base.CanKillTile(i, j, type, ref blockDamaged);
        }

        public override bool CanPlace(int i, int j, int type)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j))
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i, j))
            {
                return false;
            }

            return base.CanPlace(i, j, type);
        }

        public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j))
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i, j))
            {
                return false;
            }

            return base.CanReplace(i, j, type, tileTypeBeingPlaced);
        }
    }
}