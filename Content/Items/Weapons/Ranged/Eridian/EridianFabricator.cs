using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Materials;

namespace Vaultaria.Content.Items.Weapons.Ranged.Eridian
{
    public class EridianFabricator : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(83, 30);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Master;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 16;
            Item.shoot = ProjectileID.Bubble;
            Item.useAmmo = ModContent.ItemType<Eridium>();

            // Combat properties
            Item.knockBack = 0f;
            Item.damage = 0;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.LegendaryDrop, 60);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int itemIndex = Main.rand.Next(0, Utilities.gunGunItemArray.Count); // Picks a random index from 0 to the end of the array
            ModItem item = (ModItem) Utilities.gunGunItemArray[itemIndex]; // Get whatever item is at that index

            player.QuickSpawnItem(player.GetSource_DropAsItem(), item.Type); // Spawn the item at the player

            return false;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            for (int i = 0; i < 250; i++)
            {
                player.ConsumeItem(ammo.type, false);
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-20f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses 250 eridium", Utilities.VaultarianColours.Slag);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots out a Legendary gun", Utilities.VaultarianColours.Information);
            Utilities.RedText(tooltips, Mod, "A Gun... Gun?");
        }
    }
}