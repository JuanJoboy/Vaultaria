using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Banshee : ModSkill
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int bonusDamage = Utilities.DisplaySkillBonusText(20f, 0.1f);
            int bonusSpeed = Utilities.DisplaySkillBonusText(27f, 0.1f);

            Utilities.Text(tooltips, Mod, "Tooltip1", "Gives all the previous bonuses in one Class Mod");
            Utilities.Text(tooltips, Mod, "Tooltip2", "While under 20% health, your magic attacks deal bonus Incendiary Damage", Utilities.VaultarianColours.Incendiary);
            Utilities.Text(tooltips, Mod, "Tooltip3", "While under 30% health you gain increased Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"+{bonusDamage}% Incendiary Damage");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusSpeed}% Movement Speed");
            Utilities.RedText(tooltips, Mod, "Rawr.");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.SpectreBar, 20)
                .AddIngredient(ItemID.SoulofMight, 10)
                .AddIngredient(ItemID.SoulofSight, 10)
                .AddIngredient(ItemID.SoulofFright, 10)
                .AddIngredient<Immolate>(1)
                .AddIngredient<Fleet>(1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}