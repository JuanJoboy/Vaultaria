using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Projectiles.Shields
{
    public class ImpalerSpike : ElementalProjectile
    {
        public float corrosiveMultiplier;
        private float elementalChance = 100f;
        private short corrosiveProjectile = ElementalID.CorrosiveProjectile;
        private int corrosiveBuff = ElementalID.CorrosiveBuff;
        private int buffTime = 180;


        public override void SetDefaults()
        {
            base.SetDefaults();
            // Size
            Projectile.Size = new Vector2(21, 29);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Config
            Projectile.timeLeft = 3600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.ai[0] = 1f;
        }

        public override void AI()
        {
            base.AI();
            Utilities.FrameRotator(4, Projectile);

            NPC target = FindTarget();

            if (target != null && target.active && !target.friendly)
            {
                Utilities.MoveToTarget(Projectile, target, 4, 1);
            }
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

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item126, Projectile.position);

            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.JungleSpore).noGravity = false;
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

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(61, 82, 58);
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Corrosive"
            };
        }
    }
}