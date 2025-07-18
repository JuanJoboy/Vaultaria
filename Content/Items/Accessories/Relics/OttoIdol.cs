using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class OttoIdol : ModRelic
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Get 10% health back on every kill"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Every man for himself.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
            tooltips.Add(new TooltipLine(Mod, "Curse", "Curse of the Sudden-er Death!")
            {
                OverrideColor = new Color(0, 249, 199) // Cyan
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 60;
            player.statDefense += 5;
            player.lifeRegen += -20;
        }
    }
}