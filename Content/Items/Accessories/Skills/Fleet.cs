using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Buffs.SkillEffects;

namespace Vaultaria.Content.Items.Accessories.Skills
{
    public class Fleet : ModSkill
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(30, 30);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);

            if(player.statLife <= player.statLifeMax2 * 0.3f)
            {
                player.AddBuff(ModContent.BuffType<FleetPassive>(), 120);
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int numberOfBossesDefeated = Utilities.DownedBossCounter();

            float baseSpeed = 0.1f;

            float bonusSpeed = (int) (100 * + ((numberOfBossesDefeated / 27f) + baseSpeed));

            Utilities.Text(tooltips, Mod, "Tooltip1", "While under 30% health you gain increased Movement Speed");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Movement Speed increases as you progress");
            Utilities.Text(tooltips, Mod, "Tooltip3", $"+{bonusSpeed}% Movement Speed");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(10)
                .AddIngredient(ItemID.SilverBar, 12)
                .AddIngredient(ItemID.Bomb, 100)
                .AddIngredient(ItemID.Grenade, 100)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient<Eridium>(10)
                .AddIngredient(ItemID.TungstenBar, 12)
                .AddIngredient(ItemID.Bomb, 100)
                .AddIngredient(ItemID.Grenade, 100)
                .AddIngredient(ItemID.Dynamite, 100)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }
    }
}