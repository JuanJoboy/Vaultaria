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
            Item.Size = new Vector2(55, 25);

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
                    Rectangle npcRectangle = new Rectangle((int)npc.Center.X, (int)npc.Center.Y, npc.width + 20, npc.height + 20);

                    if (player.Hitbox.Intersects(npcRectangle) && player.HasBuff(ModContent.BuffType<DeceptionBuff>()))
                    {
                        player.velocity *= 0f;
                    }
                }
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

        public override bool? CanHitNPC(Player player, NPC target)
        {
            if (target.townNPC)
            {
                return false;
            }

            Rectangle mouse = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 20, 20);

            if (player.HasBuff(ModContent.BuffType<DeceptionBuff>()))
            {
                if (Main.mouseLeft && target.Hitbox.Intersects(mouse))
                {
                    Utilities.MoveToPosition(player, Main.MouseWorld, 20, 3f);
                    Utilities.ItemSound(Item, Utilities.Sounds.Execute, 60);
                }
            }

            return true;
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "0", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip1", "When paired with the Deception potion's buff,\nyou gain the ability to dash towards enemies at your mouse", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "How hilarious\nYou just set off my trap card\nYour death approaches.");
        }
    }
}