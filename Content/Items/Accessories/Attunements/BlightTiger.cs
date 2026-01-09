using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.AccessoryEffects;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Accessories.Attunements
{
    public class BlightTiger : ModAttunement
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Adds 20% Corrosive damage to all attacks", Utilities.VaultarianColours.Corrosive);
        }
    }
}