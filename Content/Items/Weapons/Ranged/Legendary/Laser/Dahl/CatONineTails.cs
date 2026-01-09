using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.SMG.Maliwan;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Dahl
{
    public class CatONineTails : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(69, 30);
            Item.scale = 0.95f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 20;
            Item.shoot = ProjectileID.HeatRay;
            Item.mana = 10;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 20;
            Item.crit = 6;
            Item.DamageType = DamageClass.Magic;

            Item.useTime = 7;
            Item.useAnimation = 21;
            Item.reuseDelay = 5;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.ItemSound(Item, Utilities.Sounds.GenericLaser, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                ProjectileID.HeatRay,
                damage,
                knockback,
                player.whoAmI,
                1f,
                1f
            );        
                
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-17f, 3f);
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<MagicTrickshot>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "ToolTip1", "Shoots a burst of piercing Incendiary lasers that ricochet on surface impact", Utilities.VaultarianColours.Incendiary);
            Utilities.RedText(tooltips, Mod, "The cat's out of the bag.");
        }
    }
}