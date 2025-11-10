using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Content.Buffs.MagicEffects;
using Vaultaria.Content.Projectiles.Magic;
using Terraria.DataStructures;

public class PhaselockedNPC : GlobalNPC
{
    public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
        base.PostDraw(npc, spriteBatch, screenPos, drawColor);

        if (!npc.townNPC)
        {
            if (npc.HasBuff(ModContent.BuffType<Phaselocked>()))
            {
                AddDrawing(npc, spriteBatch, screenPos);
            }
        }
    }

    public override void OnKill(NPC npc)
    {
        // Check for the buff first.
        if (npc.HasBuff(ModContent.BuffType<Phaselocked>()))
        {
            Vector2 direction = npc.Center;
            direction.Normalize();
            direction *= 8f;

            Projectile.NewProjectileDirect(
                npc.GetSource_FromThis(),
                npc.Center,
                direction,
                ModContent.ProjectileType<PhaselockBubble>(),
                0,
                0f,
                Main.myPlayer
            );
        }

        base.OnKill(npc);
    }

    private void AddDrawing(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos)
    {
        // --- 1. Load the Texture (This should usually be cached in Load() for efficiency) ---

        // Replace "MyIndicator" with the path to your texture (e.g., "Vaultaria/Textures/Indicator")
        Texture2D texture = ModContent.Request<Texture2D>("Vaultaria/Common/Textures/bubble").Value;

        // --- 2. Calculate Drawing Position ---

        // npc.Center is the world position.
        // screenPos converts it to a screen position.
        Vector2 drawCenter = npc.Center - screenPos;

        // --- 3. Define Drawing Parameters ---

        Rectangle sourceRectangle = texture.Frame(); // Use the whole texture
        Vector2 origin = sourceRectangle.Size() / 2f; // Draw from the center of the texture

        float scale;
        if (npc.height < npc.width)
        {
            scale = 0.3f * (npc.height / 5); // was 0.25 before
        }
        else
        {
            scale = 0.3f * (npc.height / 7); // was 0.25 before
        }

        // --- 4. Draw the Texture ---
        spriteBatch.Draw(
            texture,                  // The texture to draw
            drawCenter,             // The screen position to draw at
            sourceRectangle,          // Which part of the texture to use
            Color.White * 0.5f,              // Drawing color (White uses the texture's native color)
            0f,                       // Rotation (none)
            origin,                   // Origin for rotation and positioning
            scale,                       // Scale (0.5x size)
            SpriteEffects.None,       // Flip effects
            0f                        // Layer depth (0f is foreground)
        );
    }
}