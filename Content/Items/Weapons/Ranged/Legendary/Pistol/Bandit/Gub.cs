using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Bandit;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Bandit
{
    public class Gub : ElementalItem
    {
        // override: This "plugs into" the virtual slot created in the base class. It replaces the null with actual data.
        // => [...]: Every time the code asks for ItemSounds, it points to this specific list of two sounds. Because it’s a property, it doesn't "run" until it's called in UseItem.
        protected override Utilities.Sounds[] ItemSounds => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(45, 30);
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ModContent.ProjectileType<GubBullet>();
            Item.useAmmo = ModContent.ItemType<PistolAmmo>();
            ItemID.Sets.ShimmerTransformToItem[Item.type] = ModContent.ItemType<Zim>();

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 40;
            Item.crit = 0;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 0;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 1);
            Utilities.SetItemSound(Item, Utilities.Sounds.BanditPistol, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 0f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Pistol Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots Corrosive bullets", Utilities.VaultarianColours.Corrosive);
            Utilities.RedText(tooltips, Mod, "Abt natural.\nThe Curse of the Gub");
        }
    }
}