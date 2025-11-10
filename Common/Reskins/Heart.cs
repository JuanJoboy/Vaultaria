using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Content.Buffs.PotionEffects;

public class Heart : GlobalItem
{
    public override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        if(item.type == ItemID.Heart)
        {
            AddDrawing(item, spriteBatch, 0, scale, position);
            return false;
        }
        
        return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
    }

    public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        if (item.type == ItemID.Heart)
        {
            AddDrawing(item, spriteBatch, rotation, scale, new Vector2());
            return false;
        }

        return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
    }

    private void AddDrawing(Item item, SpriteBatch spriteBatch, float rotation, float scale, Vector2 position)
    {
        // 1. Load the Texture
        Texture2D texture = ModContent.Request<Texture2D>("Vaultaria/Common/Textures/healthVial").Value;

        // 2. Determine Drawing Position based on context (Inventory vs. World)
        Vector2 finalDrawPosition;
        
        // Check if a position was supplied (indicating Inventory drawing)
        if (position != new Vector2())
        {
            // For INVENTORY: 'position' is the top-left corner of the slot.
            // We add the origin offset to center the sprite on the inventory slot.
            // The draw frame size is needed to get the center of the drawing area.
            Main.GetItemDrawFrame(item.type, out _, out var itemFrame);
            Vector2 itemCenterOffset = itemFrame.Size() * scale / 2f; 
            finalDrawPosition = position + itemCenterOffset;
        }
        else // Position is default/empty, assume World drawing
        {
            // For WORLD: Use the item's world position adjusted by screen position.
            finalDrawPosition = item.Center - Main.screenPosition;
        }

        // 3. Define Drawing Parameters
        Rectangle sourceRectangle = texture.Frame();
        Vector2 origin = sourceRectangle.Size() / 2f; // Draw from the center of the texture

        // 4. Draw the Texture
        spriteBatch.Draw(
            texture, 
            finalDrawPosition, // Use the corrected position
            sourceRectangle,
            Color.White,       
            rotation,
            origin,
            scale,
            SpriteEffects.None,
            0f
        );
    }
}