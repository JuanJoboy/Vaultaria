using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SubworldLibrary;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
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
            Player player = Main.LocalPlayer;

            if(NoBossIsActive())
            {
                ResetVaultMonsterSystems();
                Utilities.SpawnBoss(player, NPCID.QueenSlimeBoss);
            }

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
        }

        private bool NoBossIsActive()
        {
            if(Utilities.bossTimer < Utilities.countdown)
            {
                return false;
            }

            foreach(NPC n in Main.ActiveNPCs)
            {
                if(n.type == NPCID.QueenSlimeBoss || n.type == NPCID.Retinazer || n.type == NPCID.Spazmatism || n.type == NPCID.SkeletronPrime || n.type == NPCID.DD2Betsy || n.type == NPCID.Plantera || n.type == NPCID.PlanterasHook || n.type == NPCID.PlanterasTentacle || n.type == NPCID.Golem || n.type == NPCID.GolemFistLeft || n.type == NPCID.GolemFistRight || n.type == NPCID.GolemHead || n.type == NPCID.GolemHeadFree || n.type == NPCID.DukeFishron || n.type == NPCID.HallowBoss || n.type == NPCID.CultistBoss || n.type == NPCID.CultistBossClone || n.type == NPCID.CultistDragonBody1 || n.type == NPCID.CultistDragonBody2 || n.type == NPCID.CultistDragonBody3 || n.type == NPCID.CultistDragonBody4 || n.type == NPCID.CultistDragonHead || n.type == NPCID.CultistDragonTail || n.type == NPCID.MoonLordCore || n.type == NPCID.MoonLordFreeEye || n.type == NPCID.MoonLordHand || n.type == NPCID.MoonLordHead || n.type == NPCID.MoonLordLeechBlob)
                {
                    return false;
                }
            }

            return true;
        }
    }
}