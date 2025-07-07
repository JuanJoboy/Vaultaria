using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Vaultaria.Content.Items.Tiles.VendingMachines
{
    public class MarcusVendingMachine : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Item Config
            Main.tileFrameImportant[Type] = true; // Tells Terraria that there is TileObjectData that is used for rendering
            Main.tileSolidTop[Type] = true; // The tile is solid on top
            Main.tileNoAttach[Type] = true; // Doesn't attach to other tiles
            Main.tileLavaDeath[Type] = true; // This tile is killed by Lava
            Main.tileTable[Type] = true; // This tile acts as a table

            // Tile Config
            TileID.Sets.DisableSmartCursor[Type] = false; // Enables smart cursor interaction with this tile
            TileID.Sets.IgnoredByNpcStepUp[Type] = true; // Prevents NPCs from standing on top of this tile

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4); // Copies the existing style of 3x4
            TileObjectData.addTile(Type); // Adding the tile type to this style

            // Crafting
            AdjTiles = new int[] {TileID.WorkBenches, TileID.Anvils};

            // Map & Housing
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable); // Counts as a table for a room
            AddMapEntry(new Microsoft.Xna.Framework.Color(200, 200, 200), CreateMapEntryName()); // Adds the name to the minimap
        }
    }
}