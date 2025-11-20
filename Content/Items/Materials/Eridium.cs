using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Materials
{
    public class Eridium : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(22, 24);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Purple;
            Item.ammo = ModContent.ItemType<Eridium>();
            Item.consumable = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ToolTip1", "An alien material used to turn ordinary items into legendary gear")
            {
                OverrideColor = new Color(142, 94, 235) // Purple
            });
        }
    }
}