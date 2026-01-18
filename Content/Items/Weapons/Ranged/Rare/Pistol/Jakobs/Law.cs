using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Accessories.Shields;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Jakobs
{
    public class Law : ElementalItem
    {
        protected override Utilities.Sounds[] ItemSounds => [];

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(66, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 1f;
            Item.damage = 15;
            Item.crit = 4;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 10;
            Item.autoReuse = true;
            Item.useTurn = false;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.SetItemSound(Item, Utilities.Sounds.JakobsPistol, 60);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2f, 0f);
        }

        public override bool AltFunctionUse(Player player)
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click melee
            {
                Item.damage = 18;
                Item.crit = 4;
                Item.DamageType = DamageClass.Melee;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = false;
                Item.shootSpeed = 0f;
                Item.shoot = ProjectileID.None;
                Item.useAmmo = AmmoID.None;
                Item.UseSound = SoundID.Item1;

                Item.useTime = 10;
                Item.useAnimation = 10;
                Item.reuseDelay = 0;
                Item.autoReuse = true;
                Item.useTurn = false;

            }
            else // Left-click ranged
            {
                Item.damage = 15;
                Item.crit = 4;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 10f;
                Item.shoot = ProjectileID.Bullet;
                Item.useAmmo = AmmoID.Bullet;
                Utilities.SetItemSound(Item, Utilities.Sounds.JakobsPistol, 60);

                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.reuseDelay = 10;
                Item.autoReuse = false;
                Item.useTurn = false;
            }

            return base.CanUseItem(player);
        }
        
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            HasOrderOn(player, damageDone);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip2", "Right-Click to do a melee attack");
            Utilities.Text(tooltips, Mod, "Tooltip3", "Gives you lifesteal if Order is also equipped", Utilities.VaultarianColours.Healing);
            Utilities.RedText(tooltips, Mod, "De Da.");
        }

        private void HasOrderOn(Player player, int damageDone)
        {
            bool hasOrderEquipped = false;

            for (int i = 0; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].type == ModContent.ItemType<Order>())
                {
                    hasOrderEquipped = true;
                    break;
                }
            }

            if (hasOrderEquipped)
            {
                int heal = (int) (damageDone * 0.25f);
                player.statLife += heal;
                player.HealEffect(heal);
            }
        }
    }
}