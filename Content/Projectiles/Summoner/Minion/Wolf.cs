using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.SummonerEffects;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Bandit;

namespace Vaultaria.Content.Projectiles.Summoner.Minion
{
    public class Wolf : ElementalProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true;

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 28;

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 36000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            Projectile.extraUpdates = 1;
            Projectile.CritChance = 0;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 0.5f;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Utilities.MinionFrameRotator(8, Projectile);
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.65f); // Gives it light

            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<WolfAndSaint>());
                return;
            }

            if (player.HasBuff(ModContent.BuffType<WolfAndSaint>()))
            {
                Projectile.timeLeft = 2;
            }

            AIGeneral(player, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            AISearchForTarget(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            AIMovement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);

            NPC target = FindTarget();
            
            if (target != null)
            {
                if (target.Center.X > Projectile.Center.X)
                {
                    Projectile.spriteDirection = 1; // Face right
                }
                else
                {
                    Projectile.spriteDirection = -1; // Face left
                }
            
                if(player.whoAmI == Main.myPlayer)
                {
                    if (EnemyFoundToShoot(target, 0, 120f))
                    {
                        // Calculates a normalized direction to the target and scales it to a bullet speed of 8
                        Vector2 direction = target.Center - Projectile.Center;
                        direction.Normalize();
                        direction *= 8f;

                        Projectile proj = Projectile.NewProjectileDirect(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            direction,
                            ProjectileID.ChlorophyteBullet,
                            Projectile.damage / 2,
                            0f,
                            Projectile.owner
                        );

                        proj.DamageType = DamageClass.Summon;

                        // Reset fire timer
                        Projectile.ai[0] = 0f;
                    }

                    if (EnemyFoundToShoot(target, 1, 180f))
                    {
                        Vector2 direction = target.Center - Projectile.Center;
                        direction.Normalize();
                        direction *= 8f;

                        Projectile.NewProjectileDirect(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            direction,
                            ModContent.ProjectileType<ZimBullet>(),
                            Projectile.damage / 4,
                            0f,
                            Projectile.owner
                        );

                        Projectile.NewProjectileDirect(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            direction,
                            ModContent.ProjectileType<GubBullet>(),
                            Projectile.damage / 4,
                            0f,
                            Projectile.owner
                        );
                        
                        Projectile.ai[1] = 0f;
                    }

                    if (EnemyFoundToShoot(target, 2, 240f))
                    {
                        Vector2 direction = target.Center - Projectile.Center;
                        direction.Normalize();
                        direction *= 8f;

                        Projectile proj = Projectile.NewProjectileDirect(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            direction,
                            ProjectileID.ClusterRocketI,
                            Projectile.damage,
                            2f,
                            Projectile.owner
                        );
                        
                        proj.DamageType = DamageClass.Summon;

                        Projectile.ai[2] = 0f;
                    }
                }

            }
            
            Projectile.netUpdate = true;
        }

        private void AIGeneral(Player player, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
        {
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f;

            float minionPositionOffset = (10 + Projectile.minionPos * 40) * -player.direction; // Minion will always be behind the player
            idlePosition.X = player.Center.X + minionPositionOffset;

            vectorToIdlePosition = idlePosition - Projectile.Center;
            distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
            {
                Projectile.position = idlePosition;
                Projectile.velocity *= 0.1f;
            }

            float overlapVelocity = 0.04f;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile other = Main.projectile[i];

                if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < other.position.X)
                    {
                        Projectile.velocity.X -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.X += overlapVelocity;
                    }

                    if (Projectile.position.Y < other.position.Y)
                    {
                        Projectile.velocity.Y -= overlapVelocity;
                    }
                    else
                    {
                        Projectile.velocity.Y += overlapVelocity;
                    }
                }
            }
        }

        private void AISearchForTarget(Player player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Setting the out variables at the start as I may not find a target and will have to return default values
            distanceFromTarget = 700;
            targetCenter = Projectile.position;
            foundTarget = false;
            int offset = 100;

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);
                if (between < 1200) // If the player targets an enemy, this code checks the distance between minion and target and if its in range then it attacks
                {
                    distanceFromTarget = between;

                    if (npc.Center.X > Projectile.Center.X)
                    {
                        targetCenter = npc.Center + new Vector2(-offset, -offset);
                    }
                    else
                    {
                        targetCenter = npc.Center + new Vector2(offset, -offset);
                    }

                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);

                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center + new Vector2(-offset, -offset);
                            foundTarget = true;
                        }
                    }
                }
            }
        }

        private void AIMovement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            float speed = 8f;
            float inertia = 20f;

            if(foundTarget)
            {
                if(distanceFromTarget > 40f)
                {
                    Vector2 direction = targetCenter - Projectile.Center;
                    direction.Normalize();
                    direction *= speed;

                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                return;
            }

            if(distanceToIdlePosition > 600f)
            {
                speed = 12f;
                inertia = 60f;
            }
            else
            {
                speed = 4f;
                inertia = 80f;
            }

            if(distanceToIdlePosition > 20f)
            {
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (Projectile.velocity == Vector2.Zero) // Make it not stand still, make it do a little idle animation
            {
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
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
                    float dist = Vector2.Distance(Projectile.Center, npc.Center); // Measures the distance from turret to NPC
                    if (dist < range && Collision.CanHitLine(Projectile.Center, 1, 1, npc.Center, 1, 1)) // Checks if the NPC is closer than any previously checked NPC and if there's a clear line of sight
                    {
                        closest = npc;
                        range = dist;
                    }
                }
            }

            return closest; // Returns the best valid NPC target, or null if none found
        }

        private bool EnemyFoundToShoot(NPC target, int index, float ticks)
        {
            // Fire timer stored in ai[0]
            Projectile.ai[index]++;
            if (Projectile.ai[index] >= ticks) // Fire every 6 ticks (~0.1 sec)
            {
                if (target != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}