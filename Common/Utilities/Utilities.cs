using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace Vaultaria.Common.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// Heals the player based on the healingPercentage.
        /// <br/> The full formula for healing is:
        /// <br/> ((damageDone * healingPercentage) / 0.075)
        /// <br/> This is because vampireHeal already multiplies the first parameter by 0.075, so this method divides it by that to make it normal. That way, if you enter in 0.65f as your healingPercentage, then your item will heal you for 65% of your damage.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damageDone"></param>
        /// <param name="healingPercentage"></param>
        /// <param name="projectile"></param>
        public static void HealOnNPCHit(NPC target, int damageDone, float healingPercentage, Projectile projectile)
        {
            int heal = (int)(damageDone * healingPercentage);
            heal = (int)(heal / 0.075f); // Divide by 0.075f to bring it back to normal
            projectile.vampireHeal(heal, projectile.Center, target);
        }

        /// <summary>
        /// Heals the player based on the healingPercentage.
        /// <br/> The full formula for healing is:
        /// <br/> ((damageDone * healingPercentage) / 0.075)
        /// <br/> This is because vampireHeal already multiplies the first parameter by 0.075, so this method divides it by that to make it normal. That way, if you enter in 0.65f as your healingPercentage, then your item will heal you for 65% of your damage.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="damageDone"></param>
        /// <param name="healingPercentage"></param>
        /// <param name="projectile"></param>
        public static void HealOnPlayerHit(Player target, int damageDone, float healingPercentage, Projectile projectile)
        {
            int heal = (int)(damageDone * healingPercentage);
            heal = (int)(heal / 0.075f); // Divide by 0.075f to bring it back to normal
            projectile.vampireHeal(heal, projectile.Center, target);
        }

        /// <summary>
        /// Takes the parameters of an item's Shoot() method along with how much the new clones should spread and how many clones should appear.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="source"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="type"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="numberOfAdditionalBullets"></param>
        /// <param name="degreeSpread"></param>
        public static void CloneShots(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int numberOfAdditionalBullets, float degreeSpread)
        {

            // Define a slight spread angle for the bullets (e.g., degreeSpread = 5, 5 degrees total spread)
            float spreadAngle = MathHelper.ToRadians(degreeSpread); // Convert degrees to radians

            // Calculate the base rotation of the velocity vector
            float baseRotation = velocity.ToRotation();

            for (int i = 0; i <= numberOfAdditionalBullets; i++)
            {
                // Calculate the individual bullet's angle
                // This distributes the bullets symmetrically around the original velocity direction
                float bulletAngle = baseRotation + MathHelper.Lerp(-spreadAngle / 2, spreadAngle / 2, (float)i / (numberOfAdditionalBullets - 1));

                // Calculate the new velocity vector for this bullet
                Vector2 bulletVelocity = bulletAngle.ToRotationVector2() * velocity.Length();

                Projectile.NewProjectile(source, position, bulletVelocity, type, damage, knockback, player.whoAmI);
            }
        }

        /// <summary>
        /// A wrapper method for the randomizer.
        /// <br/> To use chance, put in a float from 1 - 100. So if you put in 23.5, there would be a 23.5% chance of something happening.
        /// </summary>
        /// <param name="chance"></param>
        /// <returns>True if the randomizer picks a number within your range, and false otherwise.</returns>
        public static bool Randomizer(float chance)
        {
            if (Main.rand.Next(1, 101) <= chance)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This will cycle through all of the frames in the sprite sheet.
        /// <br/> Frame speed is how fast you want it to animate (lower = faster).
        /// </summary>
        /// <param name="frameSpeed"></param>
        public static void FrameRotator(int frameSpeed, Projectile projectile)
        {
            projectile.rotation = projectile.velocity.ToRotation();

            projectile.frameCounter++;
            if (projectile.frameCounter >= frameSpeed)
            {
                projectile.frameCounter = 0;
                projectile.frame++;

                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
        }
    }
}