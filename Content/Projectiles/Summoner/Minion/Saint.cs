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
using Vaultaria.Content.Projectiles.Ammo.Rare.AssaultRifle.Vladof;

namespace Vaultaria.Content.Projectiles.Summoner.Minion
{
    public class Saint : ElementalProjectile
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
            return false;
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
            AISearchForPlayer(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            AIMovement(player, foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);

            if (PlayerFoundToHeal(player, 0, 60f))
            {
                if(player.whoAmI == Main.myPlayer)
                {
                    // Calculates a normalized direction to the target and scales it to a bullet speed of 8
                    Vector2 direction = player.Center - Projectile.Center;
                    direction.Normalize();
                    direction *= 8f;

                    // Heal the player
                    int healAmount = 5;

                    // Fire a custom "heal line" projectile
                    Projectile.NewProjectile(
                        Projectile.GetSource_FromThis(),
                        Projectile.Center,
                        direction,
                        ProjectileID.SpiritHeal,
                        0,
                        0f,
                        Projectile.owner,
                        player.whoAmI, // pass target player
                        healAmount     // pass amount (for visuals)
                    );

                    // Reset fire timer
                    Projectile.ai[0] = 0f;
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

        private void AISearchForPlayer(Player player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Setting the out variables at the start as I may not find a target and will have to return default values
            distanceFromTarget = 700;
            targetCenter = Projectile.position;
            foundTarget = false;

            float between = Vector2.Distance(player.Center, Projectile.Center);
            if (between < 200) // If the player targets an enemy, this code checks the distance between minion and target and if its in range then it attacks
            {
                distanceFromTarget = between;

                if(player.direction == 1)
                {
                    targetCenter = player.Center + new Vector2(-70, -70);
                }
                if(player.direction == -1)
                {
                    targetCenter = player.Center + new Vector2(70, -70);
                }

                foundTarget = true;
            }

            if (!foundTarget)
            {
                between = Vector2.Distance(player.Center, Projectile.Center);
                bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                bool inRange = between < distanceFromTarget;
                bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, player.position, player.width, player.height);

                bool closeThroughWall = between < 100f;

                if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                {
                    distanceFromTarget = between;
                    targetCenter = player.Center;
                    foundTarget = true;
                }
            }
        }

        private void AIMovement(Player player, bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
        {
            if(player.velocity == Vector2.Zero)
            {
                if(player.direction == 1)
                {
                    Projectile.Center = player.Center + new Vector2(-70, -70);
                    Projectile.direction = 1;
                    Projectile.spriteDirection = 1;
                }
                if(player.direction == -1)
                {
                    Projectile.Center = player.Center + new Vector2(70, -70);
                    Projectile.direction = -1;
                    Projectile.spriteDirection = -1;
                }
            }
            else
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
            }
        }

        private bool PlayerFoundToHeal(Player player, int index, float ticks)
        {
            // Fire timer stored in ai[0]
            Projectile.ai[index]++;
            if (Projectile.ai[index] >= ticks) // Fire every 6 ticks (~0.1 sec)
            {
                if(player != null)
                {
                    // Face target
                    if (player.Center.X > Projectile.Center.X)
                    {
                        Projectile.spriteDirection = 1; // Face right
                    }
                    else
                    {
                        Projectile.spriteDirection = -1; // Face left
                    }

                    Projectile.netUpdate = true;
                    return true;   
                }
            }

            return false;
        }
    }
}