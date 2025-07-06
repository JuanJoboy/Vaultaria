using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Build.Evaluation;
using System;
using Vaultaria.Content.Items.Weapons.Ammo;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Vladof
{
    public class ShredifierBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            // Turn off immunity frames so that every projectile can hit
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;

            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            AIType = ProjectileID.BulletHighVelocity;

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
                    const int numberOfClones = 2; // I want 2 bullets total
                    const float totalSpreadDegrees = 1; // Total angle of the semi-circle spread in degrees
                    float baseAngle = Projectile.velocity.ToRotation(); // Get the current direction of the parent bullet
                    float angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / (numberOfClones - 1)); // Angle between each cloned bullet
                                                                                                            // (numberOfClones - 1) because there are (n-1) gaps for n bullets

                    // Adjust the base angle so the spread is centered around the original velocity.
                    // This moves the starting point of the loop to the far left of the desired spread arc.
                    baseAngle -= MathHelper.ToRadians(totalSpreadDegrees) / 2f;

                    for (int i = 0; i < numberOfClones; i++)
                    {
                        // Calculate the new angle for this specific clone
                        float newAngle = baseAngle + (i * angleIncrement);

                        // Create the new velocity vector using the calculated angle and the parent's speed.
                        // Projectile.velocity.Length() gives the current speed of the parent.
                        Vector2 newVelocity = newAngle.ToRotationVector2() * Projectile.velocity.Length();

                        // Spawn the new projectile (clone)
                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(), // Source for the new projectile (this parent projectile)
                            Projectile.Center,               // Spawn at the center of the parent bullet
                            newVelocity,                     // The newly calculated velocity for spread
                            Projectile.type,                 // Same projectile type as this one
                            15,                               // Each bullet does 15 damage
                            Projectile.knockBack,            // Same knockback as this one
                            Projectile.owner,                // Same owner as this one
                            0f,                              // ai[0] = 0f: This clone is NOT a parent; it won't clone further
                            0f                               // Other ai values, if you had any
                        );
                    }
                    Projectile.ai[0] = 0f; // Important! Prevents infinite cloning.
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