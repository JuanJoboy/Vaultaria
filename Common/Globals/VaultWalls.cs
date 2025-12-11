using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Content.Buffs.PotionEffects;
using Terraria.DataStructures;
using Vaultaria.Common.Systems.GenPasses.Vaults;

namespace Vaultaria.Common.Globals
{
    public class VaultWalls : GlobalWall
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

        public override void KillWall(int i, int j, int type, ref bool fail)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());
            Point16 vault2Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault2", ModContent.GetInstance<Vaultaria>());

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                fail = true;
            }

            Utilities.Utilities.VaultArea(vault1Dimensions, VaultBuilder.vault1positionX, VaultBuilder.vault1positionY, i, j, ref fail);
            Utilities.Utilities.VaultArea(vault2Dimensions, VaultBuilder.vault2positionX, VaultBuilder.vault2positionY, i, j, ref fail);
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
    }
}