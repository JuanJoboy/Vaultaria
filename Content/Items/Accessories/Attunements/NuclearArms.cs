using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.AccessoryEffects;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Accessories.Attunements
{
    public class NuclearArms : ModAttunement
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Adds 20% Radiation damage to all attacks")
            {
                OverrideColor = new Color(227, 205, 109) // Light Yellow
            });
        }
    }
}