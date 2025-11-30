// using Terraria;
// using Terraria.ModLoader;
// using Microsoft.Xna.Framework;
// using Vaultaria.Content.Buffs.Prefixes.Elements;
// using Terraria.ID;
// using Vaultaria.Common.Utilities;
// using Microsoft.Xna.Framework.Graphics;
// using Vaultaria.Content.Buffs.PotionEffects;
// using Terraria.GameContent;

// public class Explosion : GlobalProjectile
// {
//     public override void SetDefaults(Projectile entity)
//     {
//         base.SetDefaults(entity);

//         if (entity.type == ElementalID.SmallExplosiveProjectile || entity.type == ElementalID.ExplosiveProjectile || entity.type == ElementalID.LargeExplosiveProjectile)
//         {
//             Main.projFrames[entity.type] = 4;
//         }
//     }

//     public override void AI(Projectile projectile)
//     {
//         base.AI(projectile);

//         if (projectile.type == ElementalID.SmallExplosiveProjectile || projectile.type == ElementalID.ExplosiveProjectile || projectile.type == ElementalID.LargeExplosiveProjectile)
//         {
//             Utilities.FrameRotator(5, projectile);
//         }
//     }

//     public override bool PreDraw(Projectile projectile, ref Color lightColor)
//     {
//         // return CustomTexture(projectile);
//         return ReSkinnedTexture(projectile);
//     }

//     private bool ReSkinnedTexture(Projectile projectile)
//     {
//         // 1. Identify the projectile you want to reskin
//         if (projectile.type == ElementalID.SmallExplosiveProjectile || projectile.type == ElementalID.ExplosiveProjectile || projectile.type == ElementalID.LargeExplosiveProjectile)
//         {
//             // 2. Load your custom texture
//             Texture2D texture = TextureAssets.Projectile[projectile.type].Value;
            
//             // 3. Define drawing parameters (position, source rectangle, color, etc.)
//             Vector2 drawPosition = projectile.Center - Main.screenPosition;
//             Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            
//             // 4. Draw your custom texture
//             Main.EntitySpriteDraw(
//                 texture,
//                 drawPosition,
//                 sourceRectangle,
//                 Color.White, // Use a brighter color than lightColor for bullets
//                 projectile.rotation,
//                 sourceRectangle.Size() / 2,
//                 projectile.scale,
//                 SpriteEffects.None,
//                 0
//             );
            
//             // 5. Return false to prevent the vanilla texture from being drawn
//             return false;
//         }

//         // Return true for all other projectiles to let them draw normally
//         return true;
//     }

//     private bool CustomTexture(Projectile projectile)
//     {
//         // 1. Identify the projectile you want to reskin
//         if (projectile.type == ElementalID.SmallExplosiveProjectile || projectile.type == ElementalID.ExplosiveProjectile || projectile.type == ElementalID.LargeExplosiveProjectile)
//         {
//             // 2. Load your custom texture
//             Texture2D texture = ModContent.Request<Texture2D>("Vaultaria/Common/Textures/Explosion").Value;
            
//             // 3. Define drawing parameters (position, source rectangle, color, etc.)
//             Vector2 drawPosition = projectile.Center - Main.screenPosition;
//             // Rectangle sourceRectangle = texture.Frame();
//             // Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
//             Rectangle sourceRectangle = texture.Frame(Main.projFrames[projectile.type], 1, projectile.frame, 0);
            
//             // 4. Draw your custom texture
//             Main.EntitySpriteDraw(
//                 texture,
//                 drawPosition,
//                 sourceRectangle,
//                 Color.White, // Use a brighter color than lightColor for bullets
//                 projectile.rotation,
//                 sourceRectangle.Size() / 2,
//                 projectile.scale / 4,
//                 SpriteEffects.None,
//                 0
//             );
            
//             // 5. Return false to prevent the vanilla texture from being drawn
//             return false;
//         }

//         // Return true for all other projectiles to let them draw normally
//         return true;
//     }
// }