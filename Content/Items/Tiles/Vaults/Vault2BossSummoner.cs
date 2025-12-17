using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SubworldLibrary;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Vaultaria.Common.Networking;
using Vaultaria.Common.Systems;
using Vaultaria.Common.Systems.GenPasses;
using Vaultaria.Common.Systems.GenPasses.Vaults;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Placeables.Vaults;
using Vaultaria.Content.Items.Weapons.Magic;

namespace Vaultaria.Content.Items.Tiles.Vaults
{
    public class Vault2BossSummoner : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Item Config
            Main.tileFrameImportant[Type] = true; // Tells Terraria that there is TileObjectData that is used for rendering
            Main.tileSolidTop[Type] = false; // The tile is solid on top
            Main.tileNoAttach[Type] = true; // Doesn't attach to other tiles
            Main.tileLavaDeath[Type] = false; // This tile is killed by Lava
            Main.tileShine2[Type] = true;
            Main.tileShine[Type] = 1000;

            // Tile Config
            TileID.Sets.DisableSmartCursor[Type] = false; // Enables smart cursor interaction with this tile
            TileID.Sets.IgnoredByNpcStepUp[Type] = true; // Prevents NPCs from standing on top of this tile

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.addTile(Type); // Adding the tile type to this style

            AddMapEntry(new Color(200, 200, 200), CreateMapEntryName()); // Adds the name to the minimap
        }

        public override bool RightClick(int i, int j)
        {
            if(NoBossIsActive())
            {
                Utilities.startedVault2BossRush = true;
                ResetVaultMonsterSystems();

                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    ModNetHandler.vault.SendBossRushStatus(Utilities.startedVault2BossRush, Main.myPlayer);
                }
            }

            NetMessage.SendData(MessageID.WorldData);

            return base.RightClick(i, j);
        }

        private void ResetVaultMonsterSystems()
        {
            VaultMonsterSystem.vaultQueenSlime = false;
            VaultMonsterSystem.vaultQueenSlimeDR = false;
            VaultMonsterSystem.vaultTwins = false;
            VaultMonsterSystem.vaultTwinsDR = false;
            VaultMonsterSystem.vaultSkeletronPrime = false;
            VaultMonsterSystem.vaultSkeletronPrimeDR = false;
            VaultMonsterSystem.vaultBetsy = false;
            VaultMonsterSystem.vaultBetsyDR = false;
            VaultMonsterSystem.vaultPlantera = false;
            VaultMonsterSystem.vaultPlanteraDR = false;
            VaultMonsterSystem.vaultGolem = false;
            VaultMonsterSystem.vaultGolemDR = false;
            VaultMonsterSystem.vaultDukeFishron = false;
            VaultMonsterSystem.vaultDukeFishronDR = false;
            VaultMonsterSystem.vaultEmpress = false;
            VaultMonsterSystem.vaultEmpressDR = false;
            VaultMonsterSystem.vaultLunaticCultist = false;
            VaultMonsterSystem.vaultLunaticCultistDR = false;
            VaultMonsterSystem.vaultMoonLord = false;
            VaultMonsterSystem.vaultMoonLordDR = false;

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                ModNetHandler.vault.SendBossDeath(Main.myPlayer);
            }
        }

        private bool NoBossIsActive()
        {
            if(Utilities.startedVault2BossRush)
            {
                return false;
            }

            return true;
        }
    }
}