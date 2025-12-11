using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Materials
{
    public class Moonstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(28, 28);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(gold: 50);
            Item.rare = ItemRarityID.Cyan;
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "An invaluable, high-grade mineral granted by the Moon Lord, essential for advanced Vault Hunter gear", Utilities.VaultarianColours.CursedText);
        }
    }
}