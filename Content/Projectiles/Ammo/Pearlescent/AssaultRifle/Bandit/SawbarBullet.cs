using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Pearlescent.AssaultRifle.Bandit
{
    public class SawbarBullet : ElementalProjectile
    {
        public float incendiaryMultiplier = 2f;
        private float elementalChance = 100f;
        private short incendiaryProjectile = ElementalID.IncendiaryProjectile;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int buffTime = 180;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;

            Projectile.ai[0] = 0f;
        }

        public override void AI()
        {
            base.AI();

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.ai[0] == 1f)
            {
                if (Projectile.timeLeft <= 560)
                {
                    const int numberOfClones = 10;
                    const float totalSpreadDegrees = 270;
                    float baseAngle = Projectile.velocity.ToRotation();
                    float angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / (numberOfClones - 1));

                    baseAngle -= MathHelper.ToRadians(totalSpreadDegrees) / 2f;

                    for (int i = 0; i < numberOfClones; i++)
                    {
                        float newAngle = baseAngle + (i * angleIncrement);

                        Vector2 newVelocity = newAngle.ToRotationVector2() * Projectile.velocity.Length();

                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            newVelocity,
                            incendiaryProjectile,
                            65,
                            2.3f,
                            Projectile.owner,
                            0f,
                            0f
                        );
                    }

                    Projectile.ai[0] = 0f;
                }
            }
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
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
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
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Incendiary"
            };
        }
    }
}