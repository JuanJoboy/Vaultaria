using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Prefixes.Shields
{
    public class BlastProof : ModPrefix
    {
        // Change your category this way, defaults to PrefixCategory.Custom. Affects which items can get this prefix.
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[BuffID.Ironskin] = true;
        }

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
            player.noKnockback = true;
            player.AddBuff(BuffID.Ironskin, 60);
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[ModContent.BuffType<ExplosiveBuff>()] = true;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "BlastProof", "Reduces damage from explosions")
            {
                OverrideColor = Utilities.VaultarianColours.Explosive.GetVaultarianColor()
            };
        }
	}
}