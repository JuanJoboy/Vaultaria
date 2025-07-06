using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Items.Tiles.VendingMachines
{
    public class MarcusVendingMachine : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true; // Tells Terraria that there is tileobjectdata that is used for rendering
            Main.tileSolidTop[Type] = true; // The tile is solid on top
            Main.tileNoAttach[Type] = true; // Doesn't attach to other tiles
            Main.tileLavaDeath[Type] = true; // This tile is killed by Lava
            Main.tileTable[Type] = true; // This tile acts as a table

            TileID.Sets.DisableSmartCursor[Type] = true; // Disables smart cursor interaction with this tile
            TileID.Sets.IgnoredByNpcStepUp[Type] = true; // Prevents NPCs from standing on top of this tile

            AdjTiles = new int[] { TileID.WorkBenches }; // Tells Terraria this tile is part of the Workbenches Tiles
            AdjTiles = new int[] { TileID.Anvils };

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4); // Copying the existing style of 3x4
            TileObjectData.addTile(Type); // Adding the tile type to this style

            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

            AddMapEntry(new Microsoft.Xna.Framework.Color(200, 200, 200), CreateMapEntryName());
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            // Gets the numerical ID of your VendingMachine item. This is the item that will drop when the tile is broken.
            int itemType = ModContent.ItemType<Placeables.VendingMachines.MarcusVendingMachine>();

            // Spawns a new item in the world.
            Item.NewItem(
                // Specifies the source of the item creation. EntitySource_TileBreak(i, j) indicates
                // that the item is dropped because a tile at coordinates (i, j) was broken.
                new EntitySource_TileBreak(i, j),
                // X-coordinate in world units (pixels). i * 16 converts tile column 'i' to pixel X.
                i * 16,
                // Y-coordinate in world units (pixels). j * 16 converts tile row 'j' to pixel Y.
                j * 16,
                // Width of the bounding box for spawning the item. Here, 32 pixels wide.
                32,
                // Height of the bounding box for spawning the item. Here, 16 pixels high.
                // The item will drop within this defined rectangular area.
                16,
                // The actual item type (ID) to spawn.
                itemType
            );
        }
    }
}