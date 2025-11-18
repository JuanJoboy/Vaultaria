using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Dahl
{
    public class BasicRepeater : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 4f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 5;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 4;
            Item.useAnimation = 12;
            Item.reuseDelay = 60;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(copper: 60);
            Utilities.ItemSound(Item, Utilities.Sounds.DahlPistolBurst, 60);
            // Item.UseSound = SoundID.Item31;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.LeadBar, 5)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "ToolTip1", "Uses any normal bullet type as ammo\nFires a basic 3 burst round"));
            tooltips.Add(new TooltipLine(Mod, "Red Text", "Wait a minute - you're not dead!\nYES! Now I can get off this glacier!\nClaptrap, your metaphorical ship has finally come in!")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }
    }
}