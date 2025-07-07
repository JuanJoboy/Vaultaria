using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

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
            Item.buyPrice(gold: 2, silver: 50);
            Item.rare = ItemRarityID.Blue;
        }
    }
}