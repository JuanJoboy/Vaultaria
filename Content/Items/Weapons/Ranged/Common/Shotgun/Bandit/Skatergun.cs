using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Weapons.Ranged.Common.Shotgun.Bandit
{
    public class Skatergun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(60, 26);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.White;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 14;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 2f;
            Item.damage = 5;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 15;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(silver: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.BanditShotgun, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 6, 5, 2, 9);
            
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 6);
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Found in Wooden Chests", Utilities.VaultarianColours.Information);
        }
    }
}