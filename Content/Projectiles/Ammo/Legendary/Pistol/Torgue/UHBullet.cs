using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Torgue
{
    public class UHBullet : ElementalProjectile
    {
        public float explosiveMultiplier;
        private float elementalChance = 100f;
        private short explosiveProjectile = ElementalID.ExplosiveProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 90;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 14);

            // Damage
            Projectile.friendly = true; // Hurts enemies
            Projectile.hostile = false; // Doesn't hurt NPC's
            Projectile.penetrate = 1; // Penetrates once
            Projectile.aiStyle = 0; // Normal bullet style

            // Bullet Config
            Projectile.timeLeft = 600; // 10 seconds until despawn
            Projectile.ignoreWater = true; // Doesn't slow down in water
            Projectile.tileCollide = true; // Collides with tiles
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Ranged;

            // Turn off immunity frames so that every projectile can hit
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;

            // IMPORTANT: Initialize ai[0] here.
            // When the gun shoots, it should set ai[0] of the initial bullet to a special value (e.g., 1f)
            // Cloned bullets will have ai[0] = 0f (default).
            // This is just the default value if not set by the gun.
            Projectile.ai[0] = 0f; // Default: This projectile is NOT a parent that needs to clone. The gun's Shoot method will override this for the first bullet.
        }

        public override void AI()
        {
            // Always call base.AI() if you're using Projectile.aiStyle.
            // This ensures Terraria's built-in bullet AI (movement, gravity if any, etc.) is still applied.
            base.AI();

            // Handle projectile rotation to point in the direction of velocity.
            // Remove +MathHelper.PiOver2 if your sprite points right by default.
            // Add +MathHelper.PiOver2 if your sprite points UP by default.
            Projectile.rotation = Projectile.velocity.ToRotation();

            if(Projectile.owner == Main.myPlayer)
            {
                // --- CLONING LOGIC ---
                // Projectile.ai[0] is used to track if this is the "parent" bullet that should clone.
                // I set it to 1f in the gun's Shoot method for the initial bullet.
                if (Projectile.ai[0] == 1f) // Check if this is the designated "parent" bullet
                {
                    // I want the cloning to happen after a short delay (e.g., after 30 ticks = 0.5 seconds).
                    // Projectile.timeLeft counts down from 600. So, 600 - 30 = 570.
                    // I could also check Projectile.owner == Main.myPlayer to ensure the cloning only happens
                    // on the client that owns the projectile, preventing duplicate spawns in multiplayer.
                    if (Projectile.timeLeft == 575)
                    {
                        const int numberOfClones = 6; // I want 6 bullets total
                        const float totalSpreadDegrees = 25; // Total angle of the semi-circle spread in degrees
                        float baseAngle = Projectile.velocity.ToRotation(); // Get the current direction of the parent bullet
                        float angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / (numberOfClones - 1)); // Angle between each cloned bullet
                                                                                                                // (numberOfClones - 1) because there are (n-1) gaps for n bullets

                        // Adjust the base angle so the spread is centered around the original velocity.
                        // This moves the starting point of the loop to the far left of the desired spread arc.
                        baseAngle -= MathHelper.ToRadians(totalSpreadDegrees) / 2f;

                        for (int i = 0; i < numberOfClones; i++) // Loop to spawn the 6 clones
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
                                25,                               // Each bullet does 25 damage
                                Projectile.knockBack,            // Same knockback as this one
                                Projectile.owner,                // Same owner as this one
                                0f,                              // ai[0] = 0f: This clone is NOT a parent; it won't clone further
                                0f                               // Other ai values, if you had any
                            );
                        }

                        // After spawning all clones, the parent bullet should stop cloning.
                        // You can either kill it or just change its ai[0] to 0f.
                        // Killing it:
                        // Projectile.Kill();
                        // Changing its state so it continues as a normal bullet:
                        Projectile.ai[0] = 0f; // Important! Prevents infinite cloning.
                        Projectile.netUpdate = true;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);
            }

            return false;
        }

        public override void OnKill(int timeLeft) // What happens on Projectile death
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            int numDust = 20;
            for (int i = 0; i < numDust; i++) // Loop through code below numDust times
            {
                Vector2 newVelocity = Vector2.One.RotatedBy(MathHelper.ToRadians(360 / numDust * i)); // Circular velocity
                Dust.NewDustPerfect(Projectile.Center, DustID.YellowTorch, newVelocity).noGravity = false; // Creating dust
            }
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive"
            };
        }
    }
}