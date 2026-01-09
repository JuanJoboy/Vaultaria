using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Content.Projectiles.Ammo.Rare.Pistol.Hyperion;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion
{
    public class LadyFist : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Visual properties
            Item.Size = new Vector2(42, 29);
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;

            // Gun properties
            Item.noMelee = true;
            Item.shootSpeed = 11f;
            Item.shoot = ProjectileID.Bullet;
            Item.useAmmo = AmmoID.Bullet;

            // Combat properties
            Item.knockBack = 1f;
            Item.damage = 20;
            Item.crit = -4;
            Item.DamageType = DamageClass.Ranged;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.reuseDelay = 3;
            Item.autoReuse = true;

            // Other properties
            Item.value = Item.buyPrice(gold: 2);
            Utilities.ItemSound(Item, Utilities.Sounds.HyperionPistol, 60);
        }

        public override bool CanUseItem(Player player)
        {
            if(NPC.downedGolemBoss)
            {
                return true;
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7f, 2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Utilities.Text(tooltips, Mod);
            Utilities.Text(tooltips, Mod, "Tooltip1", "+800% Crit");

            if(!NPC.downedGolemBoss)
            {
                Utilities.Text(tooltips, Mod, "Tooltip2", "Can only be used after defeating Golem", Utilities.VaultarianColours.Information);
            }

            Utilities.Text(tooltips, Mod, "Tooltip3", "Given after completing 30 Angler quests", Utilities.VaultarianColours.Information);

            Utilities.RedText(tooltips, Mod, "Love is a Lady Finger. True love is a Lady Fist.");
        }
    }
}