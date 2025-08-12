using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Grenades.Rare;

namespace Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare
{
    public class BasicGrenade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.75f;
            Item.rare = ItemRarityID.White;
            Item.maxStack = 9999;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 2.3f;
            Item.damage = 25;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = false;

            Item.shoot = ModContent.ProjectileType<BasicModule>();
            Item.consumable = true;
            Item.ammo = Item.type;
            Item.shootSpeed = 13;

            // Other properties
            Item.value = Item.buyPrice(silver: 10);
            Item.UseSound = SoundID.Item169;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Double your fun")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}