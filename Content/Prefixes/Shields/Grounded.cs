using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Prefixes.Shields
{
    public class Grounded : ModPrefix
    {
        // Change your category this way, defaults to PrefixCategory.Custom. Affects which items can get this prefix.
        public override PrefixCategory Category => PrefixCategory.Accessory;
        
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

        // Modify the cost of items with this modifier with this function.
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.25f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[ModContent.BuffType<ShockBuff>()] = true;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "Grounded", "Grants immunity to Shock damage")
            {
                OverrideColor = Utilities.VaultarianColours.Shock.GetVaultarianColor()
            };
        }
	}
}