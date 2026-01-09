using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue;

namespace Vaultaria.Content.Items.Accessories.Relics
{
    public class MysteriousAmulet : ModRelic
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(38, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            ItemID.Sets.ShimmerTransformToItem[Item.type] = ModContent.ItemType<SwordSplosion>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.RedText(tooltips, Mod, "While I fight with thee, dear friend, all losses are restored and sorrows end.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statDefense += 4;
            player.luck += 0.5f;
        }
    }
}