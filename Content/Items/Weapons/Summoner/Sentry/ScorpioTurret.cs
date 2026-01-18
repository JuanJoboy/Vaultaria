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

namespace Vaultaria.Content.Items.Weapons.Summoner.Sentry
{
    public class ScorpioTurret : ModItem
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
            Item.Size = new Vector2(40, 19);
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Combat properties
            Item.damage = 20;
            Item.DamageType = DamageClass.Summon;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 2;
            Item.knockBack = 2.3f;
            Item.autoReuse = true;
            Item.mana = 20;

            // Other properties
            Item.value = Item.buyPrice(gold: 10);
            Item.UseSound = SoundID.Item46;

            Item.noMelee = true;
            Item.shootSpeed = 0f;
            Item.shoot = ModContent.ProjectileType<RolandTurret>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);

            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, player.whoAmI);
            projectile.originalDamage = Item.damage;

            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld; // Spawns the minion at the mouse
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(4f, 0f);
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

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Deploy a Scorpio Turret that targets and fires upon enemies");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots Silver bullets every second", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Spawns ammo randomly every 5 seconds", Utilities.VaultarianColours.Information);
            Utilities.Text(tooltips, Mod, "Tooltip4", "Spawns hearts randomly every 10 seconds", Utilities.VaultarianColours.Healing);
            Utilities.Text(tooltips, Mod, "Tooltip5", "Found in Locked Shadow Chests", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "LOOK OUT! BADASS LOADER!");
        }
    }
}