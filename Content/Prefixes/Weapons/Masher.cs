using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Systems;

namespace Vaultaria.Content.Prefixes.Weapons
{
    public class Masher : ModPrefix
    {
        public override float RollChance(Item item)
        {
            if (BossDownedSystem.skeletron == false)
            {
                return 0;
            }

            return 0; // Masher doesn't work right now so im blocking it permanently for the time being
            // return 2f;
        }

        // Determines if it can roll at all.
        // Use this to control if a prefix can be rolled or not.
        public override bool CanRoll(Item item)
        {
            if (BossDownedSystem.skeletron == false)
            {
                return false;
            }

            return false; // Masher doesn't work right now so im blocking it permanently for the time being
            // return true;
        }

        // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 0.6f;
            knockbackMult *= 0.4f;
            shootSpeedMult *= 0.9f;
        }

        // Modify the cost of items with this modifier with this function.
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.3f;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "Masher", "+5 projectile count")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            };
        }
	}
}