using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Buffs.SkillEffects;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Jakobs;
using Vaultaria.Content.Prefixes.Weapons;

namespace Vaultaria.Common.Globals
{
    public class GlobalItems : GlobalItem
    {
        public int firedWeaponPrefixID;
        public override bool InstancePerEntity => true;

        private int colCounter = 0;

        public override void HoldItem(Item item, Player player)
        {
            base.HoldItem(item, player);
        }

        public override bool? UseItem(Item item, Player player)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<CloudOfLead>()))
            {
                colCounter++;
            }

            return base.UseItem(item, player);
        }

        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            CloudOfLead(item, player, source, position, velocity, damage, knockback);

            Redistribution(item, player);

            return base.Shoot(item, player, source, position, velocity, type, damage, knockback);
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo, Player player)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<CloudOfLead>()))
            {
                if(colCounter == CloudOfLeadCounter())
                {
                    colCounter = 0;
                    return false;
                }
            }

            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<Inconceivable>()))
            {
                float bonusShot = Utilities.Utilities.ComparativeBonus(player.statLifeMax2, player.statLife, 1.2f) + Utilities.Utilities.SkillBonus(300f, 0.05f);
                float chance = 100 * (bonusShot - 1);

                if(Utilities.Utilities.Randomizer(chance) && weapon.DamageType == DamageClass.Ranged)
                {
                    return false;
                }
            }

            return base.CanConsumeAmmo(weapon, ammo, player);
        }

        private void CloudOfLead(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int damage, float knockback)
        {
            if(Utilities.Utilities.IsWearing(player, ModContent.ItemType<CloudOfLead>()) && colCounter == CloudOfLeadCounter())
            {
                Projectile.NewProjectile(source, position, velocity * 2, ElementalID.IncendiaryProjectile, damage, knockback);
            }
        }

        private float CloudOfLeadCounter()
        {
            float numberOfBossesDefeated = Utilities.Utilities.DownedBossCounter();

            if(numberOfBossesDefeated > 25)
            {
                return 4;
            }
            else if(numberOfBossesDefeated > 19)
            {
                return 5;
            }
            else if(numberOfBossesDefeated > 13)
            {
                return 6;
            }
            else if(numberOfBossesDefeated > 7)
            {
                return 7;
            }
            else if(numberOfBossesDefeated > 1)
            {
                return 8;
            }
            else
            {
                return 9;
            }
        }

        private void Redistribution(Item item, Player player)
        {
            if(player.HasBuff(ModContent.BuffType<RedistributionPassiveSkill>()))
            {
                Item ammo = player.ChooseAmmo(item);

                if(ammo != null)
                {
                    if(ammo.stack < 9999)
                    {
                        ammo.stack++;
                    }
                }
            }
        }
    }
}