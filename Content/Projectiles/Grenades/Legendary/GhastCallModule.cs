using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Common.Configs;

namespace Vaultaria.Content.Projectiles.Grenades.Legendary
{
    public class GhastCallModule : ElementalProjectile
    {
        public float corrosiveMultiplier = 1f;
        private float elementalChance = 100f;
        private short corrosiveProjectile = ElementalID.CorrosiveProjectile;
        private int corrosiveBuff = ElementalID.CorrosiveBuff;
        private int buffTime = 180;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(26, 26);
            Projectile.scale = 1.6f;

            // Damage
            Projectile.damage = 1;
            Projectile.CritChance = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override bool PreAI()
        {
            ThrowGrenade();

            return base.PreAI();
        }

        public override void AI()
        {
            base.AI();
            
            NPC target = FindTarget();

            if (target != null && target.active && !target.friendly)
            {
                EnemyFoundToExplode(target);
                Utilities.MoveToPosition(Projectile, target.Center, 5, 1);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, corrosiveMultiplier, player, corrosiveProjectile, corrosiveBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, corrosiveMultiplier, player, corrosiveProjectile, corrosiveBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, corrosiveMultiplier, player, corrosiveProjectile);
            }

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 100; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0f, 0f, 100, default(Color), 2f);
            }
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Corrosive"
            };
        }

        private NPC FindTarget()
        {
            float range = 500f; // 500 pixels
            NPC closest = null;

            // Loops through every NPC in the world
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this)) // Filters to only hostile and valid targets
                {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center);
                    if (dist < range)
                    {
                        closest = npc;
                        range = dist;
                    }
                }
            }

            return closest; // Returns the best valid NPC target, or null if none found
        }

        private bool EnemyFoundToExplode(NPC target)
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 25f)
            {
                if (target != null)
                {
                    // Face target
                    if (target.Center.X > Projectile.Center.X)
                    {
                        Projectile.spriteDirection = 1; // Face right
                    }
                    else
                    {
                        Projectile.spriteDirection = -1; // Face left
                    }

                    return true;
                }
            }

            return false;
        }

        private void ThrowGrenade()
        {
            Vector2 mouse = Main.MouseWorld;
            Player player = Main.player[Main.myPlayer];

            // Face target
            if (mouse.X > player.Center.X)
            {
                Projectile.spriteDirection = 1; // Face right
            }
            else
            {
                Projectile.spriteDirection = -1; // Face left
            }
        }
    }
}