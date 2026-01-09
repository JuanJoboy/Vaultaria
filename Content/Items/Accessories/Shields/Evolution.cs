using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Prefixes.Shields;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Content.Items.Accessories.Shields
{
    public class Evolution : ModShield
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.Size = new Vector2(41, 35);
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 30);
            Item.rare = ItemRarityID.Pink;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "+50 HP\n+6 Defense\nRegenerates health rapidly");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Grants immunity to all the elements and most debuffs", Utilities.VaultarianColours.Master);
            Utilities.RedText(tooltips, Mod, "Strength through adversity.");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 50;
            player.statDefense += 6;
            player.lifeRegen += 5;

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
            player.noKnockback = true;

            // Elemental Immunities
            player.buffImmune[ModContent.BuffType<IncendiaryBuff>()] = true;
            player.buffImmune[ModContent.BuffType<ShockBuff>()] = true;
            player.buffImmune[ModContent.BuffType<CorrosiveBuff>()] = true;
            player.buffImmune[ModContent.BuffType<ExplosiveBuff>()] = true;
            player.buffImmune[ModContent.BuffType<SlagBuff>()] = true;
            player.buffImmune[ModContent.BuffType<CryoBuff>()] = true;
            player.buffImmune[ModContent.BuffType<RadiationBuff>()] = true;

            player.buffImmune[BuffID.OnFire] = true;
            player.buffImmune[BuffID.OnFire3] = true;
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[BuffID.Electrified] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[BuffID.Stinky] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Frostburn] = true;
            player.buffImmune[BuffID.Frostburn2] = true;
            player.buffImmune[BuffID.Ichor] = true;
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Bleeding] = true;

            // What I want to add
            player.buffImmune[BuffID.Blackout] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(100)
                .AddIngredient(ItemID.AnkhShield, 1)
                .AddIngredient(ItemID.LunarBar, 50)
                .AddIngredient(ItemID.FragmentSolar, 25)
                .AddIngredient(ItemID.FragmentVortex, 25)
                .AddIngredient(ItemID.FragmentStardust, 25)
                .AddIngredient(ItemID.FragmentNebula, 25)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.ZedVendingMachine>())
                .Register();
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<Inflammable>() &&
                   pre != ModContent.PrefixType<Grounded>() &&
                   pre != ModContent.PrefixType<Alkaline>() &&
                   pre != ModContent.PrefixType<BlastProof>() &&
                   pre != ModContent.PrefixType<Evolved>() &&
                   pre != ModContent.PrefixType<Thermo>() &&
                   pre != ModContent.PrefixType<RedSuit>();
        }
    }
}