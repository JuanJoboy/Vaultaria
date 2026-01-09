using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Projectiles.Grenades.Legendary;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Grenades.Legendary
{
    public class GhastCall : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(20, 35);
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.Yellow;
            Item.maxStack = 9999;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2.3f;
            Item.damage = 60;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = false;

            Item.shoot = ModContent.ProjectileType<GhastCallModule>();
            Item.consumable = true;
            Item.ammo = Item.type;
            Item.shootSpeed = 10;

            // Other properties
            Item.value = Item.buyPrice(silver: 25);
            Item.UseSound = SoundID.DD2_GoblinBomberThrow;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 5);

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }
        
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Throws 3 Corrosive homing skulls", Utilities.VaultarianColours.Corrosive);
            Utilities.RedText(tooltips, Mod, "To you I am past, a story to tell.");
        }
    }
}