using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Content.Buffs.PotionEffects;
using Terraria.DataStructures;

namespace Vaultaria.Common.Globals
{
    public class VaultWalls : GlobalWall
    {
        public override bool CanExplode(int i, int j, int type)
        {
            if(SubworldLibrary.SubworldSystem.AnyActive())
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea("Vault1", i, j))
            {
                return false;
            }

            return base.CanExplode(i, j, type);
        }

        public override void KillWall(int i, int j, int type, ref bool fail)
        {
            Point16 vault1Dimensions = StructureHelper.API.Generator.GetStructureDimensions($"Common/Systems/GenPasses/Vaults/Vault1", ModContent.GetInstance<Vaultaria>());

            int topLeftCorner = VaultBuilder.positionX;
            int topRightCorner = VaultBuilder.positionX + vault1Dimensions.X;
            int bottomLeftCorner = VaultBuilder.positionY;
            int bottomRightCorner = VaultBuilder.positionY + vault1Dimensions.Y;

            Vector2 leftBoundary = new Vector2(topLeftCorner, bottomLeftCorner);
            Vector2 rightBoundary = new Vector2(topRightCorner, bottomRightCorner);

            if(leftBoundary.Between(leftBoundary, rightBoundary))
            {
                fail = true;
            }
            else
            {
                fail = false;
            }
        }

        public override bool CanPlace(int i, int j, int type)
        {
            if(SubworldLibrary.SubworldSystem.AnyActive())
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea("Vault1", i, j))
            {
                return false;
            }

            return base.CanPlace(i, j, type);
        }
    }
}
