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
using Terraria.Audio;
using Vaultaria.Common.Configs;

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
            Item.Size = new Vector2(28, 30);

            // Combat properties
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.knockBack = 0f;
            Item.damage = 20;
            Item.crit = 6;
            Item.DamageType = DamageClass.Magic;
            Item.scale = 1f;
            Item.mana = 0;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 60;
            Item.autoReuse = false;
            Item.useTurn = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
            Utilities.ItemSound(Item, Utilities.Sounds.Phaselock, 300);
        }
        
        public override bool? UseItem(Player player)
        {
            Rectangle mouse = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 1, 1);
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            // Loops through every NPC in the world
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this)) // Filters to only hostile and valid targets
                {
                    if (Main.mouseLeftRelease && npc.Hitbox.Intersects(mouse))
                    {
                        npc.AddBuff(ModContent.BuffType<Phaselocked>(), 300);
                        Item.mana = player.statManaMax2;

                        if(config.GetRuinFirst && Main.hardMode)
                        {
                            ElementalProjectile.SetElements(player, npc);
                        }
                        else if(!config.GetRuinFirst && Main.hardMode && NPC.downedMoonlord)
                        {
                            ElementalProjectile.SetElements(player, npc);
                        }

                        return true;
                    }
                }
            }

            return base.UseItem(player);
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

            return true;
        }

        public override bool? CanMeleeAttackCollideWithNPC(Rectangle meleeAttackHitbox, Player player, NPC target)
        {
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            Utilities.Text(tooltips, Mod, "Tooltip1", "Summons a bubble at your cursor, that locks in place the npc that was clicked on", Utilities.VaultarianColours.Healing);

			if(config.GetRuinFirst == true)
            {
                Utilities.Text(tooltips, Mod, "Tooltip1", "Defeat the Wall of Flesh to get Ruin", Utilities.VaultarianColours.Information);
                Utilities.Text(tooltips, Mod, "Tooltip1", "Defeat the Moon Lord to get Sub-Sequence", Utilities.VaultarianColours.Information);
            }
            else
            {
                Utilities.Text(tooltips, Mod, "Tooltip1", "Defeat the Wall of Flesh to get Sub-Sequence", Utilities.VaultarianColours.Information);
                Utilities.Text(tooltips, Mod, "Tooltip1", "Defeat the Moon Lord to get Ruin", Utilities.VaultarianColours.Information);
            }

            Utilities.RedText(tooltips, Mod, "(giggles) I'm really good at this!");
        }
    }
}