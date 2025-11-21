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
            Item.Size = new Vector2(60, 20);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Master;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 16;
            Item.shoot = ProjectileID.Bubble;
            Item.useAmmo = ModContent.ItemType<Eridium>();

            // Combat properties
            Item.knockBack = 2.3f;
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
            FabricateAnItem(player);

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
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "Uses 250 eridium"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Shoots a Legendary or Rare gun")
            {
                OverrideColor = new Color(228, 227, 105) // Light Yellow
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "A Gun... Gun?")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
        }

        private int FabricateAnItem(Player player)
        {
            int itemIndex = Main.rand.Next(0, Utilities.itemArray.Count); // Picks a random index from 0 to the end of the array
            ModItem item = (ModItem) Utilities.itemArray[itemIndex]; // Get whatever item is at that index

            return player.QuickSpawnItem(player.GetSource_DropAsItem(), item.Type); // Spawn the item at the player
        }
    }
}