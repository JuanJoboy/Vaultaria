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
using Vaultaria.Content.Prefixes.Weapons;
using Humanizer;

namespace Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue
{
    public class Flakker : ModItem
    {
        private int flakTimer = 0;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            // Visual properties
            Item.Size = new Vector2(73, 30);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Yellow;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 10;
            Item.shoot = ProjectileID.Volcano;
            Item.useAmmo = ModContent.ItemType<ShotgunAmmo>();

            // Combat properties
            Item.knockBack = 2.3f;
            Item.damage = 35;
            Item.crit = 6;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.reuseDelay = 15;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 5);
            Utilities.ItemSound(Item, Utilities.Sounds.TorgueShotgun, 60);
        }

        public override void HoldItem(Player player)
        {
            base.HoldItem(player);

            if(flakTimer < 180)
            {
                flakTimer++;
            }

            if(flakTimer >= 180)
            {
                flakTimer = 0;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int area = 100;
            Rectangle mouse = new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, area, area);

            for(int i = 0; i < 6; i++)
            {
                // if(flakTimer % 30 == 0)
                // {
                    float flakShotX = Main.rand.NextFloat(mouse.BottomLeft().X, mouse.BottomLeft().X + area);
                    float flakShotY = Main.rand.NextFloat(mouse.TopLeft().Y, mouse.TopLeft().Y + area);
                    Vector2 flakShotZone = new Vector2(flakShotX, flakShotY);

                    Vector2 randomVel = new Vector2(Main.rand.NextFloat(-2f, 2f), Main.rand.NextFloat(-2f, 2f));

                    Projectile projectile = Projectile.NewProjectileDirect(source, flakShotZone, randomVel, type, damage, knockback, player.whoAmI, ai0: i);

                    projectile.usesLocalNPCImmunity = true;
                    projectile.localNPCHitCooldown = 30;
                // }
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13f, 0f);
        }

        public override bool AllowPrefix(int pre)
        {
            return pre != ModContent.PrefixType<MagicTrickshot>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.MultiShotText(tooltips, Item, 5);
            Utilities.Text(tooltips, Mod, "Tooltip1", "Uses Shotgun Ammo");
            Utilities.Text(tooltips, Mod, "Tooltip2", "Shoots a chain of random Explosive Projectiles", Utilities.VaultarianColours.Explosive);
            Utilities.RedText(tooltips, Mod, "Flak the world.");
        }
    }
}