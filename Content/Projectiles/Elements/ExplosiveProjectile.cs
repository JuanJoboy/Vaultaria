using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.DataStructures;

namespace Vaultaria.Content.Projectiles.Elements
{
    public class ExplosiveProjectile : ElementalProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(90, 90);
            Projectile.scale = 1f;

            // Change penetrate to -1 so it doesn't die when touching enemies
            Projectile.penetrate = -1; 
            Projectile.aiStyle = -1; // Use -1 for custom AI
            
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = false; // Explosions usually shouldn't stop at walls

            // Set timeLeft to (Number of Frames * Speed per frame)
            // If your FrameRotator uses 3 ticks per frame and you have 7 frames:
            Projectile.timeLeft = 28;

            // Use a private immunity timer for this projectile only
            Projectile.usesLocalNPCImmunity = true; 
            // -1 means once it hits an NPC, it can NEVER hit that same NPC again for its entire life
            Projectile.localNPCHitCooldown = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);

            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
        }

        public override void AI()
        {
            base.AI();

            Utilities.FrameRotator(4, Projectile);
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive"
            };
        }
    }
}