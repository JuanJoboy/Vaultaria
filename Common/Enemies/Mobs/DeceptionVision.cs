using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Content.Buffs.PotionEffects;

public class DeceptionVision : GlobalNPC
{
    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        base.PostDraw(npc, spriteBatch, screenPos, drawColor);

        if(!npc.townNPC)
        {
            Player player = Main.player[Main.myPlayer];

            if (player.HasBuff(ModContent.BuffType<DeceptionBuff>()))
            {
                AddDrawing(npc, spriteBatch, screenPos);
            }   
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        base.DrawEffects(npc, ref drawColor);

        if(!npc.townNPC)
        {
            Player player = Main.player[Main.myPlayer];

            // Check if the name contains "Slime" (case-insensitive for robustness)
            bool isSlime = npc.TypeName.Contains("Slime", System.StringComparison.OrdinalIgnoreCase);

            if (player.HasBuff(ModContent.BuffType<DeceptionBuff>()))
            {
                drawColor = Color.SkyBlue;
            }
        }
    }

    private void AddDrawing(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos)
    {
        // --- 1. Load the Texture (This should usually be cached in Load() for efficiency) ---

        // Replace "MyIndicator" with the path to your texture (e.g., "Vaultaria/Textures/Indicator")
        Texture2D texture = ModContent.Request<Texture2D>("Vaultaria/Common/Textures/zero").Value;

        // --- 2. Calculate Drawing Position ---

        // npc.Center is the world position.
        // screenPos converts it to a screen position.
        Vector2 drawCenter = npc.Center - screenPos;

        // --- 3. Define Drawing Parameters ---

        Rectangle sourceRectangle = texture.Frame(); // Use the whole texture
        Vector2 origin = sourceRectangle.Size() / 2f; // Draw from the center of the texture

        // --- 4. Draw the Texture ---

        spriteBatch.Draw(
            texture,                  // The texture to draw
            drawCenter,             // The screen position to draw at
            sourceRectangle,          // Which part of the texture to use
            Color.White * 0.75f,              // Drawing color (White uses the texture's native color)
            0f,                       // Rotation (none)
            origin,                   // Origin for rotation and positioning
            0.5f,                       // Scale (0.5x size)
            SpriteEffects.None,       // Flip effects
            0f                        // Layer depth (0f is foreground)
        );
    }
}