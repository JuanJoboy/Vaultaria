using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Items.Materials;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.MagicEffects;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Content.Items.Weapons.Magic
{
    public class PhaselockSpell : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);

            // Combat properties
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.knockBack = 0f;
            Item.damage = 10;
            Item.crit = 6;
            Item.DamageType = DamageClass.Magic;
            Item.scale = 1f;
            Item.mana = 10; // This item uses 10 mana

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.reuseDelay = 40;
            Item.autoReuse = false;
            Item.useTurn = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item15;
        }

        public override bool? CanHitNPC(Player player, NPC target)
        {
            if(target.HasBuff(ModContent.BuffType<Phaselocked>()))
            {
                return false;
            }

            if (target.townNPC)
            {
                return false;
            }

            Rectangle mouse = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 20, 20);

            if (Main.mouseLeft && target.Hitbox.Intersects(mouse))
            {
                target.AddBuff(ModContent.BuffType<Phaselocked>(), 300);
                ElementalProjectile.SetElements(player, target);
            }

            return true;
        }

        public override bool? CanMeleeAttackCollideWithNPC(Rectangle meleeAttackHitbox, Player player, NPC target)
        {
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(75)
                .AddIngredient(ItemID.FragmentSolar, 50)
                .AddIngredient(ItemID.LunarBar, 25)
                .AddIngredient(ItemID.Tabi, 1)
                .AddIngredient(ItemID.Muramasa, 1)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "0")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "How hilarious\nYou just set off my trap card\nYour death approaches.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}