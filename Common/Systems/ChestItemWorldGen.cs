using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Accessories.Relics;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Items.Materials;
using Vaultaria.Content.Items.Weapons.Ranged.Common.AssaultRifle.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Pistol.Tediore;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Shotgun.Bandit;
using Vaultaria.Content.Items.Weapons.Ranged.Common.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Shotgun.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.SMG.Maliwan;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Sniper.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Dahl;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.AssaultRifle.Jakobs;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Shotgun.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Uncommon.Sniper.Maliwan;
using Vaultaria.Content.Items.Weapons.Summoner.Sentry;

namespace Vaultaria.Common.Systems
{
	public class ChestItemWorldGen : ModSystem
	{
		// We use PostWorldGen for this because we want to ensure that all chests have been placed before adding items.
		public override void PostWorldGen()
        {
            PlaceInWoodenChests();
            
            PlaceInGoldenChests();
            
            PlaceInShadowChests();
            
            PlaceInJungleChests();

            PlaceInFrozenChests();
            
            PlaceInSkyWareChests();

			PlaceInWebChests();
            
            PlaceInWaterChests();

			PlaceVaultKeysInChests();
        }

        private void PlaceInWoodenChests()
        {
			// Place some additional items in Wooden Chests:
			// These are the new items we will place.
			int[] itemsToPlaceInChest = [ModContent.ItemType<LumpyRoot>(), ModContent.ItemType<Aegis>(), ModContent.ItemType<Handgun>(), ModContent.ItemType<Skatergun>(), ModContent.ItemType<SmoothFox>(), ModContent.ItemType<Inconceivable>(), ModContent.ItemType<Incite>(), ModContent.ItemType<OutOfBubblegum>()];
			// This variable will help cycle through the items so that different Wooden Chests get different items
			int itemsToPlaceInChestChoice = 0;
			// Rather than place items in each chest, we'll place up to the array's length * 30 (30 of each). 
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 0;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInGoldenChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<FlushRifle>(), ModContent.ItemType<Snider>(), ModContent.ItemType<ThreeWayHulk>(), ModContent.ItemType<CloudOfLead>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length / 3;
			int chest = 1;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInShadowChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Quad>(), ModContent.ItemType<OrphanMaker>(), ModContent.ItemType<ScorpioTurret>(), ModContent.ItemType<Onslaught>(), ModContent.ItemType<Reaper>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length * 3;
			int chest = 4;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInJungleChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Revenant>(), ModContent.ItemType<InspiringTransaction>(), ModContent.ItemType<AgilityRelic>(), ModContent.ItemType<PackTactics>(), ModContent.ItemType<Killer>(), ModContent.ItemType<Impact>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length * 10;
			int chest = 8;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInFrozenChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<TooScoops>(), ModContent.ItemType<NightSniper>(), ModContent.ItemType<Carbine>(), ModContent.ItemType<ViolentSpeed>(), ModContent.ItemType<Fleet>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length * 8;
			int chest = 11;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInSkyWareChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<OlPainful>(), ModContent.ItemType<Boomacorn>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = itemsToPlaceInChest.Length * 3;
			int chest = 13;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInWebChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<NightHawkin>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 15;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

        private void PlaceInWaterChests()
        {
			int[] itemsToPlaceInChest = [ModContent.ItemType<Lascaux>()];
			int itemsToPlaceInChestChoice = 0;
			int itemsPlaced = 0;
			int maxItems = Main.chest.Length;
			int chest = 17;

            Utilities.Utilities.PlaceItemsInChest(itemsToPlaceInChest, itemsToPlaceInChestChoice, itemsPlaced, maxItems, chest);
        }

		private void PlaceVaultKeysInChests()
        {
			int waterChest = 17;
			int lockedGoldChest = 2;
			int lockedShadowChest = 4;

			int skyWareChest = 13;
			int lihzahrdChest = 16;

            Utilities.Utilities.PlaceItemsInChest([ModContent.ItemType<VaultFragment1>()], 0, 0, 1, waterChest);
            Utilities.Utilities.PlaceItemsInChest([ModContent.ItemType<VaultFragment2>()], 0, 0, 1, lockedGoldChest);
            Utilities.Utilities.PlaceItemsInChest([ModContent.ItemType<VaultFragment3>()], 0, 0, 1, lockedShadowChest);

            Utilities.Utilities.PlaceItemsInChest([ModContent.ItemType<VaultFragment4>()], 0, 0, 1, skyWareChest);
            Utilities.Utilities.PlaceItemsInChest([ModContent.ItemType<VaultFragment5>()], 0, 0, 1, lihzahrdChest);
        }
	}
}