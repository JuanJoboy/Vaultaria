using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Vaultaria.Content.Projectiles.Shields;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Projectiles.Grenades.Rare;

namespace Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare
{
    public class LobbedSingularity : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(17, 30);
            Item.scale = 1.2f;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 9999;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.knockBack = 1f;
            Item.damage = 30;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = false;

            Item.shoot = ModContent.ProjectileType<SingularityModule>();
            Item.consumable = true;
            Item.ammo = Item.type;
            Item.shootSpeed = 10;

            // Other properties
            Item.value = Item.buyPrice(silver: 10);
            Item.UseSound = SoundID.DD2_GoblinBomberThrow;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-14, -7);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Creates a singularity on impact, pulling in nearby enemies", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Suck! Suck! Suck!");
        }
    }
}