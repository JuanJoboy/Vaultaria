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
    public class Vault1BossSummoner : ModTile
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
                ResetVaultMonsterSystems();
                Utilities.startedVault1BossRush = true;
            }

            NetMessage.SendData(MessageID.WorldData);

            return base.RightClick(i, j);
        }

        private void ResetVaultMonsterSystems()
        {
            VaultMonsterSystem.vaultKingSlime = false;
            VaultMonsterSystem.vaultKingSlimeDR = false;
            VaultMonsterSystem.vaultEyeOfCthulhu = false;
            VaultMonsterSystem.vaultEyeOfCthulhuDR = false;
            VaultMonsterSystem.vaultQueenBee = false;
            VaultMonsterSystem.vaultQueenBeeDR = false;
            VaultMonsterSystem.vaultDeerClops = false;
            VaultMonsterSystem.vaultDeerClopsDR = false;
            VaultMonsterSystem.vaultSkeletron = false;
            VaultMonsterSystem.vaultSkeletronDR = false;
        }

        private bool NoBossIsActive()
        {
            if(Utilities.startedVault1BossRush)
            {
                return false;
            }

            // if(Utilities.bossTimer < Utilities.countdown)
            // {
            //     return false;
            // }

            // foreach(NPC n in Main.ActiveNPCs)
            // {
            //     if(n.type == NPCID.KingSlime || n.type == NPCID.EyeofCthulhu || n.type == NPCID.QueenBee || n.type == NPCID.Deerclops || n.type == NPCID.SkeletronHead || n.type == NPCID.SkeletronHand)
            //     {
            //         return false;
            //     }
            // }

            return true;
        }
    }
}