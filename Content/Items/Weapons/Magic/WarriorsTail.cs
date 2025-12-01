using Vaultaria.Content.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Placeables.VendingMachines;
using Vaultaria.Common.Utilities;
using System.Collections.Generic;

namespace Vaultaria.Content.Items.Weapons.Magic
{
	public class WarriorsTail : ModItem
	{
		// You can use a vanilla texture for your item by using the format: "Terraria/Item_<Item ID>".
		public override string Texture => "Terraria/Images/Item_" + ItemID.LastPrism;
		public static Color OverrideColor = new(161, 113, 240);

		public override void SetDefaults() {
			// Start by using CloneDefaults to clone all the basic item properties from the vanilla Last Prism.
			// For example, this copies sprite size, use style, sell price, and the item being a magic weapon.
			Item.CloneDefaults(ItemID.LastPrism);
			Item.mana = 30;
			Item.damage = 15;
			Item.shoot = ModContent.ProjectileType<WarriorLaserHoldout>();
			Item.shootSpeed = 30f;

			// Change the item's draw color so that it is visually distinct from the vanilla Last Prism.
			Item.color = OverrideColor;
			Item.UseSound = SoundID.Item113;
		}

		// Because this weapon fires a holdout projectile, it needs to block usage if its projectile already exists.
		public override bool CanUseItem(Player player) {
			return player.ownedProjectileCounts[ModContent.ProjectileType<WarriorLaserHoldout>()] <= 0;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Shoots a powerful beam of Slag");
            Utilities.RedText(tooltips, Mod, "WARRIOR!!!... Kill.");
        }
	}
}