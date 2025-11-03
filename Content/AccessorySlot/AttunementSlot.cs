using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Vaultaria.Content.AccessorySlot
{
    public class AttunementSlot : ModAccessorySlot
    {
        public override bool CanAcceptItem(Terraria.Item checkItem, AccessorySlotType context)
        {
            if (checkItem.ModItem is ModAttunement modAttunement)
            {
                return true;
            }

            return false;
        }

        public override void BackgroundDrawColor(AccessorySlotType context, ref Color color)
        {
            base.BackgroundDrawColor(context, ref color);

            color = new Color(90, 183, 120); // Light Green
        }
    }
}