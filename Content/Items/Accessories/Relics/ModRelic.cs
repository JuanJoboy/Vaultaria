using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

public abstract class ModRelic : ModItem
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
                continue;
            }

            if (player.armor[i].ModItem?.GetType() == this.GetType())
            {
                return false;
            }
        }

        return true;
    }

    public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
    {
        return !(equippedItem.ModItem is ModRelic && incomingItem.ModItem is ModRelic);
    }
}