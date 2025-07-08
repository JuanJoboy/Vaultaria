using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Jakobs;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Order : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "When under 30% health, melee attacks do 40% bonus damage"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "25% melee life-steal if Law is also equipped")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Chung-gunk!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 25;
            player.statDefense += 3;
            player.lifeRegen += 1;

            if (player.statLife <= (player.statLifeMax2 * 0.3f))
            {
                // Increases Melee damage by 40%
                player.GetDamage(DamageClass.Melee) += 0.4f;
            }
        }
    }
}