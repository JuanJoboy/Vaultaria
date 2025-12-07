using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Materials
{
    public class SeraphCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 10;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(9, 30);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(gold: 50);
            Item.rare = ItemRarityID.Pink;
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "The blood of fallen Seraphs -- lovely.", Utilities.VaultarianColours.Healing);
        }
    }
}