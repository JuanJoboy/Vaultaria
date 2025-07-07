using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Items.Placeables.VendingMachines
{
    public class ZedVendingMachine : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(40, 40);

            Item.useTime = 15;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;

            Item.autoReuse = true;
            Item.useTurn = true;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;

            Item.createTile = ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>();
            Item.placeStyle = 0;
            
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }
    }
}