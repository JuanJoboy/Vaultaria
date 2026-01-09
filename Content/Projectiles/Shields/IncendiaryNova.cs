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

namespace Vaultaria.Content.Projectiles.Shields
{
    public class IncendiaryNova : ElementalProjectile
    {
        public float incendiaryMultiplier = 0.5f;
        private float elementalChance = 100f;
        private short incendiaryProjectile = ProjectileID.None;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int buffTime = 120;
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.Size = new Vector2(30, 30);
            Projectile.scale = 3f;

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

        public override void AI()
        {
            base.AI();

            Utilities.FrameRotator(4, Projectile);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Projectile.owner != target.whoAmI)
            {
                if (SetElementalChance(elementalChance))
                {
                    Player player = Main.player[Projectile.owner];
                    SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, incendiaryMultiplier, player, incendiaryProjectile);
            }

            return false;
        }

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(253, 217, 208);
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Incendiary"
            };
        }
    }
}