using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Vladof
{
    public class LyudaBullet : ElementalProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 2);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;

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
                if (Projectile.timeLeft == 580)
                {
                    const int numberOfClones = 3;
                    const float totalSpreadDegrees = 10f;
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
                            Projectile.type, 
                            80,
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
    }
}