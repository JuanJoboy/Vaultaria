using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Items.Materials;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.PotionEffects;

namespace Vaultaria.Content.Items.Weapons.Melee
{
    public class ZerosSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.3f;
            Item.damage = 200;
            Item.crit = 6;
            Item.DamageType = DamageClass.Melee;
            Item.scale = 1.25f;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.autoReuse = false;
            Item.useTurn = true;

            // Other properties
            Item.value = Item.buyPrice(copper: 20);
            Item.rare = ItemRarityID.Master;
            Item.UseSound = SoundID.Item15;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(player, target, hit, damageDone);

            if (player.HasBuff(ModContent.BuffType<DeceptionBuff>()))
            {
                if (target.life <= 2)
                {
                    player.AddBuff(ModContent.BuffType<DeceptionBuff>(), 300);
                }
            }
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this)) // Filters to only hostile and valid targets
                {
                    if (player.Hitbox.Intersects(npc.Hitbox) && player.HasBuff(ModContent.BuffType<DeceptionBuff>()))
                    {
                        player.velocity *= 0.8f;
                    }
                }

                player.immune = true; // Just to stabilize the player
                player.immuneTime = 5;
            }
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

        public override bool? CanHitNPC(Player player, NPC target)
        {
            Rectangle mouse = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 20, 20);

            if (player.HasBuff(ModContent.BuffType<DeceptionBuff>()))
            {
                if (Main.mouseLeft && target.Hitbox.Intersects(mouse))
                {
                    Utilities.MoveToPosition(player, Main.MouseWorld, 20, 3f);
                }
            }

            return true;
        }

        // Teleports the player to the closest npc
        private void Dash1(Player player)
        {
            float range = 500f;
            NPC closest = null;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this)) // Filters to only hostile and valid targets
                {
                    float dist = Vector2.Distance(player.Center, npc.Center);
                    if (dist < range) // Checks if the NPC is closer than any previously checked NPC and if there's a clear line of sight
                    {
                        closest = npc;
                    }
                }
            }

            if (closest != null)
            {
                // Calculate the absolute difference in Y positions.
                float yDifference = Math.Abs(player.position.Y - closest.position.Y);

                // If the player is within the acceptable vertical range (TOLERANCE)
                if (yDifference <= 50)
                {
                    // Only move the player's X position to the NPC's X position
                    player.position.X = closest.position.X + 30;
                }
                else
                {
                    player.Center = closest.Center;
                }

                player.immune = true; // Just to stabilize the player
                player.immuneTime = 5;
            }
        }
    }
}