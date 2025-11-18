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
using Vaultaria.Content.Items.Placeables.Vaults;

namespace Vaultaria.Content.Items.Tiles.Vaults
{
    public class PedestalTile2 : ModTile
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

        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            base.DrawEffects(i, j, spriteBatch, ref drawData);

            if (WorldGenerator.pedestalInVault2 == true)
            {
                Texture2D texture = ModContent.Request<Texture2D>("Vaultaria/Common/Textures/OpenedPedestal2").Value;

                Vector2 tilePosition = new Vector2(i * 16, j * 16); // tile to pixel
                Vector2 zero = Main.drawToScreen? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange); // TModLoader says to use this
                tilePosition = tilePosition - Main.screenPosition + zero;

                Rectangle sourceRectangle = texture.Frame(); // Use the whole texture
                Vector2 origin = (sourceRectangle.Size() / 2) - new Vector2(8, 0); // Draw from the center of the texture

                spriteBatch.Draw(
                    texture,                  // The texture to draw
                    tilePosition,             // The screen position to draw at
                    sourceRectangle,          // Which part of the texture to use
                    Color.White,              // Drawing color (White uses the texture's native color)
                    0f,                       // Rotation (none)
                    origin,                   // Origin for rotation and positioning
                    1f,                       // Scale (0.5x size)
                    SpriteEffects.None,       // Flip effects
                    0f                        // Layer depth (0f is foreground)
                );

                NPC.SetEventFlagCleared(ref WorldGenerator.pedestalInVault2, -1);
            }
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];

            if(player.HeldItem.type == ModContent.ItemType<VaultKey2>())
            {
                SoundEngine.PlaySound(SoundID.Item4);
                WorldGenerator.pedestalInVault2 = true;
            }

            if(!SubworldSystem.AnyActive<Vaultaria>() && WorldGenerator.pedestalInVault2 == true)
            {
                SubworldSystem.Enter<Vault2Subworld>();
            }

            return base.RightClick(i, j);
        }
    }
}