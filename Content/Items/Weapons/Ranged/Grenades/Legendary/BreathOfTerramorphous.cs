using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Grenades.Legendary;

namespace Vaultaria.Content.Items.Weapons.Ranged.Grenades.Legendary
{
    public class BreathOfTerramorphous : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.Master;
            Item.maxStack = 9999;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2.3f;
            Item.damage = 25;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.shoot = ModContent.ProjectileType<Breath>();
            Item.consumable = true;
            Item.ammo = Item.type;
            Item.shootSpeed = 12;

            // Other properties
            Item.value = Item.buyPrice(silver: 50);
            Item.UseSound = SoundID.NPCHit4;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Is highly effective on the floor"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Creates Fire explosions on impact for 5 seconds")
            {
                OverrideColor = new Color(231, 92, 22) // Orange
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "His breath was of fire…")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}