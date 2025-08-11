using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class ElementalGlobalItem : GlobalItem
    {
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Check if the item's prefix is one of the elemental prefixes
            if (ElementalProjectile.elementalPrefix.Contains(item.prefix))
            {
                // If it is, then create the projectile and correctly assign the prefix to its GlobalProjectile instance.
                ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, item.prefix);
            }

            // Return true to allow the original Shoot method to run for non-elemental items.
            return true;
        }
    }
}