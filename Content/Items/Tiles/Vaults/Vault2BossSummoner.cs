using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SubworldLibrary;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
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
            Player player = Main.player[Main.myPlayer];

            SpawnBoss(player);

            return base.RightClick(i, j);
        }

		private void StartInvasion(Player player)
        {
			Main.StartInvasion(InvasionID.GoblinArmy);

			if(Main.invasionProgress == Main.invasionProgressMax)
			{
				SpawnBoss(player);
			}
        }

		private void SpawnBoss(Player player)
        {
			NPC boss = NPC.NewNPCDirect(player.GetSource_DropAsItem(), (int) player.Center.X, (int) player.Center.Y - 50, NPCID.EyeofCthulhu);

			boss.lifeMax *= 3;
            boss.life = boss.lifeMax;
			boss.damage *= 5;
        }
    }
}