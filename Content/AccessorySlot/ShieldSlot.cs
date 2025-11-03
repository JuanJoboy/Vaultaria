using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Vaultaria.Content.AccessorySlot
{
    public class ShieldSlot : ModAccessorySlot
    {
        public override bool CanAcceptItem(Terraria.Item checkItem, AccessorySlotType context)
        {
            base.CanAcceptItem(checkItem, context);

            if (checkItem.ModItem is ModShield modShield)
            {
                return true;
            }

            return false;
        }

        public override void BackgroundDrawColor(AccessorySlotType context, ref Color color)
        {
            base.BackgroundDrawColor(context, ref color);

            color = new Color(90, 183, 237); // Light Blue
        }
    }
}