using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

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
            base.SetDefaults();
            Item.Size = new Vector2(22, 24);
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(silver: 10);
            Item.rare = ItemRarityID.Purple;
            Item.ammo = ModContent.ItemType<Eridium>();
            Item.consumable = true;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.createTile = ModContent.TileType<Tiles.Bars.Eridium>();
            Item.placeStyle = 0;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "An alien material used to turn ordinary items into legendary gear", Utilities.VaultarianColours.Slag);
        }
    }
}