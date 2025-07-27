using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Accessories.Shields;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Jakobs
{
    public class Law : ModItem
    {
        private bool isInMeleeMode = false;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(60, 20);
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 4f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2.3f;
            Item.damage = 18;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.reuseDelay = 0;
            Item.autoReuse = true;
            Item.useTurn = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Item.UseSound = SoundID.Item11;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8f, 0f);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) // Right-click melee
            {
                isInMeleeMode = true;

                Item.damage = 18;
                Item.crit = 6;
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
                Item.useTurn = true;
            }
            else // Left-click ranged
            {
                isInMeleeMode = false;

                Item.damage = 18;
                Item.crit = 6;
                Item.DamageType = DamageClass.Ranged;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shootSpeed = 4f;
                Item.shoot = ProjectileID.Bullet;
                Item.useAmmo = AmmoID.Bullet;
                Item.UseSound = SoundID.Item11;

                Item.useTime = 3;
                Item.useAnimation = 3;
                Item.reuseDelay = 1;
                Item.autoReuse = false;
                Item.useTurn = true;
            }

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int prefix = Item.prefix;
            ElementalProjectile.ElementalPrefixCorrector(player, source, position, velocity, type, damage, knockback, prefix);

            return !isInMeleeMode; // Only shoot if not in melee mode
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            HasOrderOn(player, damageDone);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Tooltip2", "Uses any normal bullet type as ammo\nRight-Click to do a melee attack"));
            tooltips.Add(new TooltipLine(Mod, "Tooltip1", "+25% melee life-steal if Order is also equipped")
            {
                OverrideColor = new Color(245, 201, 239) // Pink   
            });
            tooltips.Add(new TooltipLine(Mod, "Red Text", "De Da.")
            {
                OverrideColor = new Color(198, 4, 4) // Red
            });
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