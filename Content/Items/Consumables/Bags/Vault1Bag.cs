using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Seraph.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Shotgun.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Sniper.Maliwan;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Jakobs;

namespace Vaultaria.Content.Items.Consumables.Bags
{
    public class Vault1Bag : VaultBag
    {
        public Vault1Bag()
        {
            MinRarity = "Blue";
            MaxRarity = "Light Red";
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Items
            itemLoot.Add(ItemDropRule.OneFromOptions(1, ItemID.Terragrim, ItemID.Arkhalis, ItemID.FalconBlade, ItemID.MoneyTrough, ItemID.SlimeStaff, ItemID.DirtiestBlock, ItemID.LeafWings, ItemID.DemonHeart, ItemID.HardySaddle, ItemID.SuperheatedBlood, ItemID.HellMinecart, ItemID.GoldenFishingRod, ModContent.ItemType<FollowThrough>(), ModContent.ItemType<Killer>(), ModContent.ItemType<Quad>(), ModContent.ItemType<UnkemptHarold>(), ModContent.ItemType<Volcano>(), ModContent.ItemType<FirstBlood>()));

            // Potions
            itemLoot.Add(ItemDropRule.Common(ItemID.GreaterHealingPotion, 10, 10, 20));
            itemLoot.Add(ItemDropRule.Common(ItemID.GreaterManaPotion, 10, 10, 20));

            // Money
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(NPCID.WallofFlesh));
        }

        public override bool ItemRarityIsValid(Item item)
        {
            if(item.rare >= ItemRarityID.Blue && item.rare <= ItemRarityID.LightRed)
            {
                return true;
            }

            return false;
        }
    }
}