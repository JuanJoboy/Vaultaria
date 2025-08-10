using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class DoubleGlobal : GlobalItem
    {
        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            // Only apply to players, never NPCs
            if (player.whoAmI < 0 || player.whoAmI >= Main.maxPlayers)
            {
                return base.CanConsumeAmmo(weapon, ammo, player);
            }

            // First, let vanilla decide if ammo can be consumed normally.
            // If vanilla says no (e.g., no ammo in inventory), we should also say no.
            if (!base.CanConsumeAmmo(weapon, ammo, player))
            {
                return false;
            }

            // Check if the weapon being used has the DoublePenetrating prefix.
            if (weapon.prefix == ModContent.PrefixType<RangerDP>())
            {
                if (weapon.type == ModContent.ItemType<UnkemptHarold>())
                {
                    for (int i = 0; i < 2; i++)
                    {
                        player.ConsumeItem(ammo.type, false);
                    }
                }

                player.ConsumeItem(ammo.type, false);
            }

            // If the weapon does not have the prefix, let vanilla handle consumption normally (consume 1).
            return true;
        }
    }
}