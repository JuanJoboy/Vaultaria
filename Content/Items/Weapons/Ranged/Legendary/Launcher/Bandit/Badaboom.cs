using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Bandit;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Bandit
{
    public class Badaboom : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 6;
            Item.shoot = ModContent.ProjectileType<BadaboomRocket>();
            Item.useAmmo = ModContent.ItemType<LauncherAmmo>();

            // Combat properties
            Item.knockBack = 3.8f;
            Item.damage = 30;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 4);
            Item.UseSound = SoundID.Item66;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 6, 15);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-45f, 3f);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 24; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine damageLine = tooltips.Find(tip => tip.Name == "Damage");

            if (damageLine != null)
            {
                Player player = Main.LocalPlayer;
                int finalDamage = (int)player.GetTotalDamage(Item.DamageType).ApplyTo(Item.damage);
                damageLine.Text = finalDamage + " x 6 ranged damage";
            }

            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Consumes 25 Launcher Ammo per shot"));

            tooltips.Add(new TooltipLine(Mod, "Red Text", "Multi-kill.\nYoooo, the skip dude.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
        
        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<RangerTrickshot>();
        }
    }
}