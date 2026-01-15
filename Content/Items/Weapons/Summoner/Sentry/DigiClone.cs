using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Projectiles.Summoner.Sentry;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;
using Vaultaria.Common.Systems.GenPasses.Vaults;

namespace Vaultaria.Content.Items.Weapons.Summoner.Sentry
{
    public class DigiClone : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Type] = true; // For game pads
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(30, 30);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Combat properties
            Item.damage = 10;
            Item.crit = 4;
            Item.DamageType = DamageClass.Summon;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 2;
            Item.knockBack = 0f;
            Item.autoReuse = true;
            Item.mana = 20;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Utilities.ItemSound(Item, Utilities.Sounds.DigiCloneSpawn, 120);

            Item.noMelee = true;
            Item.shootSpeed = 0f;
            Item.shoot = ModContent.ProjectileType<Clone>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
            {
                player.AddBuff(Item.buffType, 2);

                Projectile projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, 0, 0);
                projectile.originalDamage = Item.damage;
            }

            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld; // Spawns the minion at the mouse
        }

        public override bool AltFunctionUse(Player player)
        {
            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                return false;
            }
            
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Teleport to Clone
            {
                Item.damage = 10;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;

                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.reuseDelay = 8;
                Item.autoReuse = true;
                Item.useTurn = false;

                TeleportToClone(player);

                Utilities.ItemSound(Item, Utilities.Sounds.DigiCloneSwap, 120);
            }
            else // Summon Clone
            {
                Item.damage = 10;
                Item.crit = 0;
                Item.DamageType = DamageClass.Generic;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 0f;
                Item.shoot = ModContent.ProjectileType<Clone>();

                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.autoReuse = true;
                Item.useTurn = false;

                Utilities.ItemSound(Item, Utilities.Sounds.DigiCloneSpawn, 120);
            }

            return base.CanUseItem(player);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, -0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Your Digi-Clone shoots a copy of whatever item your player is currently holding");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Digi-Clone won't consume your ammo, but still requires ammo to shoot the weapon");
            Utilities.Text(tooltips, Mod, "Tooltip3", "Only Magic and Ranged weapons can be copied");
            Utilities.Text(tooltips, Mod, "Tooltip4", $"If holding an incompatible item, the damage defaults to your defense / 2 ({Main.LocalPlayer.statDefense / 2})");

            if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
            {
                Utilities.Text(tooltips, Mod, "Tooltip5", "Right-Clicking to swap with Digi-Clone is disabled while inside either Vault");
            }
            else
            {
                Utilities.Text(tooltips, Mod, "Tooltip5", "Right-Click to swap positions with Digi-Clone");
            }

            Utilities.RedText(tooltips, Mod, "I know that fella. We went to the same assassin bars.");
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<MagicTrickshot>() &&
                   pre != ModContent.PrefixType<MagicDP>() &&
                   pre != ModContent.PrefixType<Incendiary>() &&
                   pre != ModContent.PrefixType<Shock>() &&
                   pre != ModContent.PrefixType<Corrosive>() &&
                   pre != ModContent.PrefixType<Explosive>() &&
                   pre != ModContent.PrefixType<Slag>() &&
                   pre != ModContent.PrefixType<Cryo>() &&
                   pre != ModContent.PrefixType<Radiation>();
        }

        private void TeleportToClone(Player player)
        {
            foreach(Projectile p in Main.ActiveProjectiles)
            {
                if(p.type == ModContent.ProjectileType<Clone>() && p != null && p.owner == player.whoAmI)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Dust.NewDust(player.position, player.width, player.height, DustID.Cloud, 0f, 0f, 0, default(Color), 0.7f);
                        Dust.NewDust(p.position, p.width, p.height, DustID.Cloud, 0f, 0f, 0, default(Color), 0.7f);
                    }

                    if(player.whoAmI == Main.myPlayer || p.owner == Main.myPlayer)
                    {
                        // Swaps the values of these 2 variables
                        (p.Center, player.Center) = (player.Center, p.Center);

                        if(Main.netMode != NetmodeID.SinglePlayer)
                        {
                            NetMessage.SendData(MessageID.SyncProjectile, number: p.whoAmI);
                        }
                        
                        return;
                    }
                }
            }
        }
    }
}