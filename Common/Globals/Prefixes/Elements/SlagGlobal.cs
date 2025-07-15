using Terraria;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class SlagGlobal : GlobalItem
    {
        private static float elementalChance = 40;
        private static float elementalMultiplier = 0.4f;
        private static int elementalPrefix = ElementalID.SlagPrefix;
        private static short elementalProjectile = ElementalID.SlagProjectile;
        private static int elementalBuff = ElementalID.SlagBuff;
        private static int elementalBuffTime = 120;

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ElementalProjectile.PrefixIs(item, elementalPrefix))
            {
                if (ElementalProjectile.SetElementalChance(elementalChance))
                {
                    ElementalProjectile.SetElementOnNPC(target, hit, elementalMultiplier, player, elementalProjectile, elementalBuff, elementalBuffTime);
                }
            }
        }

        public override void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
        {
            if (ElementalProjectile.PrefixIs(item, elementalPrefix))
            {
                if (ElementalProjectile.SetElementalChance(elementalChance))
                {
                    ElementalProjectile.SetElementOnPlayer(target, hurtInfo, elementalMultiplier, player, elementalProjectile, elementalBuff, elementalBuffTime);
                }
            }
        }
    }
}