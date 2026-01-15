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

namespace Vaultaria.Content.Projectiles.Summoner.Minion
{
    public class BloodwingMinion : ElementalProjectile
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
            base.SetDefaults();
            Projectile.width = 56;
            Projectile.height = 44;

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.penetrate = -1;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;

            Projectile.extraUpdates = 1;
            Projectile.CritChance = 0;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.minion = true;
            Projectile.minionSlots = 1;
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
            base.AI();

            Utilities.MinionFrameRotator(8, Projectile);
            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.65f); // Gives it light

            Player player = Main.player[Projectile.owner];

            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<Bloodwing>());
                return;
            }

            if (player.HasBuff(ModContent.BuffType<Bloodwing>()))
            {
                Projectile.timeLeft = 2;
            }

            AIGeneral(player, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
            AISearchForTarget(player, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            AIMovement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
            Projectile.netUpdate = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);

            ElementRandomizer(target, hit);
        }

        // I think out is similar to a pointer
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

            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);
                if (between < 1200) // If the player targets an enemy, this code checks the distance between minion and target and if its in range then it attacks
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
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
                            targetCenter = npc.Center;
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

        private void ElementRandomizer(NPC target, NPC.HitInfo hit)
        {
            Player player = Main.LocalPlayer;

            switch (Main.rand.Next(1, 50))
            {
                case 1:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, player, ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff, 60);
                    break;
                case 2:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, player, ProjectileID.None, ElementalID.ShockBuff, 60);
                    break;
                case 3:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, player, ElementalID.CorrosiveProjectile, ElementalID.CorrosiveBuff, 60);
                    break;
                case 4:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, player, ElementalID.SlagProjectile, ElementalID.SlagBuff, 60);
                    break;
                case 5:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, player, ProjectileID.None, ElementalID.CryoBuff, 60);
                    break;
                case 6:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, player, ElementalID.RoundExplosiveProjectile, ElementalID.ExplosiveBuff, 60);
                    break;
                case 7:
                    ElementalProjectile.SetElementOnNPC(target, hit, 0.25f, player, ElementalID.RadiationProjectile, ElementalID.RadiationBuff, 60);
                    break;
                default:
                    break;
            }
        }

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(0, 0, 0);
        }
    }
}