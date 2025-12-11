// using Terraria.UI;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Terraria;
// using Terraria.GameContent;

// namespace Vaultaria.Common.UI
// {
//     // MyCustomUI will define the panel, buttons, text, etc.
//     public class SkillTree : UIState
//     {
//         public override void Draw(SpriteBatch spriteBatch)
//         {
//             // Always call base.Draw, which handles children/events.
//             base.Draw(spriteBatch);

//             // Simple example: Draw a semi-transparent box in the center of the screen
//             Vector2 screenCenter = Main.ScreenSize.ToVector2() / 2f;
//             Rectangle panelRect = new Rectangle((int)screenCenter.X - 150, (int)screenCenter.Y - 100, 300, 200);

//             // Draw a background box
//             spriteBatch.Draw(
//                 TextureAssets.BlackTile.Value,
//                 panelRect,
//                 Color.Purple * 0.7f // Use a semi-transparent purple color
//             );

//             // Draw text
//             Utils.DrawBorderString(
//                 spriteBatch, 
//                 "Item Active UI", 
//                 screenCenter - new Vector2(0, 50), 
//                 Color.White, 
//                 1f, 
//                 0.5f, 
//                 0.5f
//             );
//         }
//     }
// }