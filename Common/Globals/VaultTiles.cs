using Terraria.ModLoader;

namespace Vaultaria.Common.Globals
{
    public class VaultTiles : GlobalTile
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

        public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
        {
            if(SubworldLibrary.SubworldSystem.AnyActive())
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea("Vault1", i, j))
            {
                return false;
            }

            return base.CanKillTile(i, j, type, ref blockDamaged);
        }

        public override bool CanPlace(int i, int j, int type)
        {
            if(Utilities.Utilities.VaultArea("Vault1", i, j) && !SubworldLibrary.SubworldSystem.AnyActive())
            {
                return false;
            }

            return base.CanPlace(i, j, type);
        }

        public override bool CanReplace(int i, int j, int type, int tileTypeBeingPlaced)
        {
            if(SubworldLibrary.SubworldSystem.AnyActive())
            {
                return false;
            }

            if(Utilities.Utilities.VaultArea("Vault1", i, j))
            {
                return false;
            }

            return base.CanReplace(i, j, type, tileTypeBeingPlaced);
        }
    }
}