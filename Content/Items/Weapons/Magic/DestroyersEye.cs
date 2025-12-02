using Vaultaria.Content.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Placeables.VendingMachines;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Content.Items.Weapons.Magic
{
	public class DestroyersEye : ModItem
	{
		public override void SetDefaults()
		{
			// Start by using CloneDefaults to clone all the basic item properties from the vanilla Last Prism.
			// For example, this copies sprite size, use style, sell price, and the item being a magic weapon.
			Item.CloneDefaults(ItemID.LastPrism);
			Item.mana = 40;
			Item.damage = 200;
			Item.shoot = ModContent.ProjectileType<DestroyerLaserHoldout>();
			Item.shootSpeed = 30f;
			Item.UseSound = SoundID.NPCDeath52;

			// Change the item's draw color so that it is visually distinct from the vanilla Last Prism.
		}

		// Because this weapon fires a holdout projectile, it needs to block usage if its projectile already exists.
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<DestroyerLaserHoldout>()] <= 0;
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
            Utilities.Text(tooltips, Mod, "Tooltip1", "Shoots a destructive beam of energy");
            Utilities.RedText(tooltips, Mod, "What is the Destroyer?");
        }
	}
}