using Terraria;

namespace Vaultaria.Common.Utilities
{
    public static class Healing
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
    }
}