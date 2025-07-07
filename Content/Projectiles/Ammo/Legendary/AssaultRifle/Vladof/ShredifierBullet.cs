using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Vladof
{
    public class ShredifierBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            // Turn off immunity frames so that every projectile can hit
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;

            Projectile.ai[0] = 0f;
        }

        public override void AI()
        {
            base.AI();

            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Projectile.ai[0] == 1f) // Check if this is the designated "parent" bullet
            {
                if (Projectile.timeLeft <= 600)
                {
                    const int numberOfClones = 2;
                    const float totalSpreadDegrees = 1;
                    float baseAngle = Projectile.velocity.ToRotation();
                    float angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / (numberOfClones - 1));

                    // Adjust the base angle so the spread is centered around the original velocity.
                    baseAngle -= MathHelper.ToRadians(totalSpreadDegrees) / 2f;

                    for (int i = 0; i < numberOfClones; i++)
                    {
                        // Calculate the new angle for this specific clone
                        float newAngle = baseAngle + (i * angleIncrement);

                        // Create the new velocity vector using the calculated angle and the parent's speed.
                        Vector2 newVelocity = newAngle.ToRotationVector2() * Projectile.velocity.Length();

                        // Spawn the new projectile (clone)
                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            newVelocity,
                            Projectile.type,
                            15,
                            Projectile.knockBack,
                            Projectile.owner,
                            0f,
                            0f
                        );
                    }
                    
                    Projectile.ai[0] = 0f;
                }
            }
        }

        public override void OnKill(int timeLeft) // What happens on Projectile death
        {
            int numDust = 20;
            for (int i = 0; i < numDust; i++) // Loop through code below numDust times
            {
                Vector2 newVelocity = Vector2.One.RotatedBy(MathHelper.ToRadians(360 / numDust * i)); // Circular velocity
                Dust.NewDustPerfect(Projectile.Center, DustID.RedMoss, newVelocity).noGravity = true; // Creating dust
            }
        }
    }
}