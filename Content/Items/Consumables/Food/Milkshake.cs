using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Consumables.Bags
{
    public class Milkshake : GlobalItem
    {
        public override void SetDefaults(Item entity)
        {
            base.SetDefaults(entity);

            if(entity.type == ItemID.Milkshake)
            {
                Utilities.SetItemSound(entity, Utilities.Sounds.RolandsMilkshakes, 420);
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);

            if(item.type == ItemID.Milkshake)
            {
                Utilities.RedText(tooltips, Mod, "Hey buddy, it's me Roland. Lets kill Handsome Jack, and then we'll all go out for milkshakes.");
            }
        }
    }
}