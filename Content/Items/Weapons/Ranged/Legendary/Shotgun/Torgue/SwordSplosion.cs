using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Materials;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Shotgun.Torgue;
using Vaultaria.Content.Items.Accessories.Relics;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue
{
    public class SwordSplosion : ModItem
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
            Item.rare = ItemRarityID.LightPurple;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<SwordSplosionKnife>();
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 15;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 35;
            Item.autoReuse = true;
            ItemID.Sets.ShimmerTransformToItem[Item.type] = ModContent.ItemType<MysteriousAmulet>();

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.TorgueShotgun, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Utilities.CloneShots(player, source, position, velocity, type, damage, knockback, 3, 5);
            
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 3);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Shotgun Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots out swords that explode on contact", Utilities.VaultarianColours.Explosive);
            Utilities.Text(tooltips, Mod, "Tooltip3", "Given after completing 50 Angler quests", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "Because Mister Torgue said so.");
        }
    }
}