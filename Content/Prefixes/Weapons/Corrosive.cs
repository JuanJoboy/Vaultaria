using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Prefixes.Weapons
{
    public class Corrosive : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.AnyWeapon;

        public override float RollChance(Item item)
        {
            return 2f;
        }

        // Determines if it can roll at all.
        // Use this to control if a prefix can be rolled or not.
        public override bool CanRoll(Item item)
        {
            return true;
        }

        // Damage Multiplier, Knockback Multiplier, Use Time Multiplier, Scale Multiplier (Size), Shoot Speed Multiplier, Mana Multiplier (Mana cost), Crit Bonus.
        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1.1f;
        }

        // Modify the cost of items with this modifier with this function.
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.15f;
        }
        
        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "Corrosive", "40% Chance to deal 40% bonus Corrosive damage")
            {
                OverrideColor = new Color(136, 235, 94) // Light Green
            };
        }
	}
}