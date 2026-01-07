using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Hyperion;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Hyperion
{
    public class LogansGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(45, 30);
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<LoganBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 18;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.HyperionPistol, 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Eridium>(35)
                .AddIngredient(ItemID.FlintlockPistol, 1)
                .AddIngredient(ItemID.IllegalGunParts, 1)
                .AddIngredient(ItemID.Grenade, 60)
                .AddIngredient(ItemID.HellstoneBar, 8)
                .AddTile(ModContent.TileType<Tiles.VendingMachines.MarcusVendingMachine>())
                .Register();
        }   

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1f, 3f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Pistol Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots Explosive-Fire Rockets", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "Gun, Gunner!");
        }
    }
}