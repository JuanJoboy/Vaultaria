using Terraria;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class ShockGlobalProjectile : GlobalProjectile
    {
        private static float elementalChance = 40;
        private static float elementalMultiplier = 0.4f;
        private static int elementalPrefix = ElementalID.ShockPrefix;
        private static short elementalProjectile = ElementalID.ShockProjectile;
        private static int elementalBuff = ElementalID.ShockBuff;
        private static int elementalBuffTime = 120;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ElementalProjectile.AbleToProc(projectile, elementalProjectile, out Player player, elementalPrefix))
            {
                if (ElementalProjectile.SetElementalChance(elementalChance))
                {
                    ElementalProjectile.SetElementOnNPC(target, hit, elementalMultiplier, player, elementalProjectile, elementalBuff, elementalBuffTime);
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (ElementalProjectile.AbleToProc(projectile, elementalProjectile, out Player player, elementalPrefix))
            {
                if (ElementalProjectile.SetElementalChance(elementalChance))
                {
                    ElementalProjectile.SetElementOnPlayer(target, info, elementalMultiplier, player, elementalProjectile, elementalBuff, elementalBuffTime);
                }
            }
        }
    }
}