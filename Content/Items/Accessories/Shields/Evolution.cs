using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Prefixes.Shields;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Evolution : ModShield
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(20, 20);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Regenerates health rapidly"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Grants immunity to all the elements and most debuffs")
            {
                OverrideColor = new Color(245, 252, 175) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Strength through adversity.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 200;
            player.statDefense += 6;
            player.lifeRegen += 40;

            // Ankh Shield
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Stoned] = true;

            // Elemental Immunities
            player.buffImmune[ModContent.BuffType<IncendiaryBuff>()] = true;
            player.buffImmune[ModContent.BuffType<ShockBuff>()] = true;
            player.buffImmune[ModContent.BuffType<CorrosiveBuff>()] = true;
            player.buffImmune[ModContent.BuffType<ExplosiveBuff>()] = true;
            player.buffImmune[ModContent.BuffType<SlagBuff>()] = true;

            // What I want to add
            player.buffImmune[BuffID.Blackout] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.AnkhShield, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<Inflammable>() &&
                   pre != ModContent.PrefixType<Grounded>() &&
                   pre != ModContent.PrefixType<Alkaline>() &&
                   pre != ModContent.PrefixType<BlastProof>() &&
                   pre != ModContent.PrefixType<Evolved>();;
        }
    }
}