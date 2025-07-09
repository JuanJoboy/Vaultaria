using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

public abstract class ModShield : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        for (int i = 0; i < 8 + player.extraAccessorySlots; i++)
        {
            if (i == slot)
            {
                continue; // Without this, you can't replace a shield, you have to manually take the active one out before replacing it
            }

            if (player.armor[i].ModItem is ModShield)
            {
                return false;
            }
        }

        return true;
    }
}