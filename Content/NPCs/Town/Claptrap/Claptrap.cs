using Vaultaria.Common;
using Vaultaria.Common.Systems;
using Vaultaria.Content.Items;
using Vaultaria.Content.Items.Accessories;
using Vaultaria.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Vladof;
using Vaultaria.Content.Items.Accessories.Relics;
using Vaultaria.Content.Items.Weapons.Ranged.Common.SMG.Hyperion;
using Vaultaria.Content.Items.Weapons.Ranged.Common.AssaultRifle.Vladof;
using Vaultaria.Content.Items.Weapons.Ranged.Common.Sniper.Jakobs;
using Vaultaria.Content.Items.Accessories.Attunements;
using Vaultaria.Content.Items.Materials;
using System.Collections;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Torgue;
using Vaultaria.Content.Items.Weapons.Ranged.Rare.Pistol.Hyperion;
using Vaultaria.Content.Items.Accessories.Shields;
using Vaultaria.Content.Items.Weapons.Ammo;
using Vaultaria.Common.Configs;
using Vaultaria.Content.Items.Placeables.Vaults;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Laser.Tediore;

namespace Vaultaria.Content.NPCs.Town.Claptrap
{
	// [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
	[AutoloadHead]
	public class Claptrap : ModNPC
	{
		public const string ShopName = "Gearbox Premiere Club";
		public int NumberOfTimesTalkedTo = 0;

		private static int ShimmerHeadIndex;
		private static Profiles.StackedNPCProfile? NPCProfile;

		private int currentIndex = 0;
		private readonly (string buttonTitle, int cornerPicture)[] pages =
		{
            ("MainMenu", ItemID.None),
            ("Vaultaria", ItemID.None),
            ("Eridium", ModContent.ItemType<Eridium>()),
            ("Incendiary", ModContent.ItemType<SoulFire>()),
            ("Shock", ModContent.ItemType<Shockra>()),
            ("Corrosive", ModContent.ItemType<BlightTiger>()),
            ("Explosive", ModContent.ItemType<MindBlown>()),
            ("Slag", ModContent.ItemType<CorruptedSpirit>()),
            ("Cryo", ModContent.ItemType<ColdHearted>()),
            ("Radiation", ModContent.ItemType<NuclearArms>()),
            ("DoublePenetrating", ModContent.ItemType<UnkemptHarold>()),
            ("Trickshot", ModContent.ItemType<Fibber>()),
            ("Accessories", ModContent.ItemType<Sham>()),
            ("Ammo", ModContent.ItemType<LauncherAmmo>()),
            ("Vaults", ModContent.ItemType<VaultKey1>()),
		};

		// Sets a unique message when the NPC dies.
		// See also NPCID.Sets.IsTownChild if you just want the message used by Angler and Princess.
		// See ModifyDeathMessage() way below for more details
		public override LocalizedText DeathMessage => this.GetLocalization("DeathMessage");

		public override void Load() {
			// Adds our Shimmer Head to the NPCHeadLoader.
			ShimmerHeadIndex = Mod.AddNPCHeadTexture(Type, Texture + "_Shimmer_Head");
		}

		public override void SetStaticDefaults() {
			Main.npcFrameCount[Type] = 25; // The total amount of frames the NPC has

			NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
			NPCID.Sets.AttackFrameCount[Type] = 4; // The amount of frames in the attacking animation.
			NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
			NPCID.Sets.AttackType[Type] = 1; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
			NPCID.Sets.AttackTime[Type] = 90; // The amount of time it takes for the NPC's attack animation to be over once it starts.
			NPCID.Sets.AttackAverageChance[Type] = 30; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
			NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
			NPCID.Sets.ShimmerTownTransform[NPC.type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

			NPCID.Sets.ShimmerTownTransform[Type] = true; // Allows for this NPC to have a different texture after touching the Shimmer liquid.

			// Influences how the NPC looks in the Bestiary
			NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() {
				Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
				Direction = -1 // -1 is left and 1 is right. NPCs are drawn facing the left by default
				// Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
				// If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
			};

			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

			// Set Example Person's biome and neighbor preferences with the NPCHappiness hook. You can add happiness text and remarks with localization (See an example in Vaultaria/Localization/en-US.lang).
			// NOTE: The following code uses chaining - a style that works due to the fact that the SetXAffection methods return the same NPCHappiness instance they're called on.
			NPC.Happiness
				.SetBiomeAffection<ForestBiome>(AffectionLevel.Love) // Example Person prefers the forest.
				.SetBiomeAffection<SnowBiome>(AffectionLevel.Like) // Example Person likes the snow.
				.SetNPCAffection(NPCID.Dryad, AffectionLevel.Love) // Loves living near the dryad.
				.SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Love) // Loves living near the party girl.
				.SetNPCAffection(NPCID.Nurse, AffectionLevel.Like) // Likes living near the nurse.
				.SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Like) // Likes living near the arms dealer.
				.SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate) // Hates living near the demolitionist.
			; // < Mind the semicolon!

			// This creates a "profile" for Claptrap, which allows for different textures during a party and/or while the NPC is shimmered.
			NPCProfile = new Profiles.StackedNPCProfile(
				new Profiles.DefaultNPCProfile(Texture, NPCHeadLoader.GetHeadSlot(HeadTexture), Texture + "_Party"),
				new Profiles.DefaultNPCProfile(Texture + "_Shimmer", ShimmerHeadIndex, Texture + "_Shimmer_Party")
			);

			ContentSamples.NpcBestiaryRarityStars[Type] = 3; // We can override the default bestiary star count calculation by setting this.

		}

		public override void SetDefaults()
		{
			NPC.townNPC = true; // Sets NPC to be a Town NPC
			NPC.friendly = true; // NPC Will not attack player
			NPC.width = 18;
			NPC.height = 40;
			NPC.aiStyle = NPCAIStyleID.Passive;
			NPC.damage = 20;
			NPC.defense = 15;
			NPC.lifeMax = 250;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.5f;

			AnimationType = NPCID.Guide;
		}
		
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange([
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets your NPC's flavor text in the bestiary. (use localization keys)
				new FlavorTextBestiaryInfoElement("Mods.Vaultaria.NPCs.Claptrap.Bestiary.Claptrap_1"),

				// You can add multiple elements if you really wanted to
				new FlavorTextBestiaryInfoElement("Mods.Vaultaria.NPCs.Claptrap.Bestiary.Claptrap_2"),
			]);
		}

		// // The PreDraw hook is useful for drawing things before our sprite is drawn or running code before the sprite is drawn
		// // Returning false will allow you to manually draw your NPC
		// public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
		// 	// This code slowly rotates the NPC in the bestiary
		// 	// (simply checking NPC.IsABestiaryIconDummy and incrementing NPC.Rotation won't work here as it gets overridden by drawModifiers.Rotation each tick)
		// 	if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers)) {
		// 		drawModifiers.Rotation += 0.001f;

		// 		// Replace the existing NPCBestiaryDrawModifiers with our new one with an adjusted rotation
		// 		NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
		// 		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
		// 	}

		// 	return true;
		// }

		public override void HitEffect(NPC.HitInfo hit) {
			int num = NPC.life > 0 ? 1 : 5;

			for (int k = 0; k < num; k++) {
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.SparksMech);
			}

			// Create gore when the NPC is killed.
			if (Main.netMode != NetmodeID.Server && NPC.life <= 0) {
				// Retrieve the gore types. This NPC has shimmer and party variants for head, arm, and leg gore. (12 total gores)
				string variant = "";
				if (NPC.IsShimmerVariant)
				{
					variant += "_Shimmer";
				}
				if (NPC.altTexture == 1)
				{
					variant += "_Party";
				}
				int hatGore = NPC.GetPartyHatGore();
				int headGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Head").Type;
				int armGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Arm").Type;
				int legGore = Mod.Find<ModGore>($"{Name}_Gore{variant}_Leg").Type;

				// Spawn the gores. The positions of the arms and legs are lowered for a more natural look.
				if (hatGore > 0) {
					Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, hatGore);
				}
				Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, headGore, 1f);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 20), NPC.velocity, armGore);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
				Gore.NewGore(NPC.GetSource_Death(), NPC.position + new Vector2(0, 34), NPC.velocity, legGore);
			}
		}

		public override void OnSpawn(IEntitySource source) {
			if (source is EntitySource_SpawnNPC) {
				// A TownNPC is "unlocked" once it successfully spawns into the world.
				TownNPCRespawnSystem.unlockedClaptrapSpawn = true;
			}
		}

		// Requirements for the town NPC to spawn.
		public override bool CanTownNPCSpawn(int numTownNPCs)
        {
			return true;
        }

		public override ITownNPCProfile? TownNPCProfile() {
			return NPCProfile;
		}

		public override List<string> SetNPCNameList()
		{
			if (NPC.IsShimmerVariant)
			{
				return new List<string>() {
					"Shadowtrap",
					"5H4D0W-TP"
				};
			}
			else
			{
				return new List<string>() {
					"Claptrap",
					"CL4P-TP",
					"Fragtrap",
					"FR4G-TP",
					"Useless",
					"Moron",
					"Piece Of Junk"
				};
            }
		}
		
		public override void FindFrame(int frameHeight) {
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}

		public override string GetChat()
		{
			// If the player talks to the npc and the chat isnt on the main menu section, then this if-statement gets the text and picture related to what the player left the npc on. Otherwise it would show the main dialogues and no picture on a page related to something else.
			if (pages[currentIndex] != ("MainMenu", ItemID.None))
			{
				Main.npcChatCornerItem = pages[currentIndex].cornerPicture;
				return Language.GetTextValue($"Mods.Vaultaria.NPCs.Claptrap.VaultarianInfo.{pages[currentIndex].buttonTitle}");
            }

			WeightedRandom<string> chat = new WeightedRandom<string>();

			if(NPC.IsShimmerVariant)
            {
				int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
				if (partyGirl >= 0 && Main.rand.NextBool(4))
				{
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.PartyGirlDialogue1", Main.npc[partyGirl].GivenName));
				}
				
				// These are things that the NPC has a chance of telling you when you talk to it.
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue1"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue2"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue3"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue4"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.CommonDialogue"), 0.8f);
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.RareDialogue"), 0.5f);

				VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();
				if(config.EnableProfanity == true)
				{
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.VeryRareDialogueExp"), 0.4f);
				}
				else
				{
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.VeryRareDialogueSafe"), 0.4f);
				}

				NumberOfTimesTalkedTo++;
				if (NumberOfTimesTalkedTo >= 10)
				{
					// This counter is linked to a single instance of the NPC, so if Claptrap is killed, the counter will reset.
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.TalkALot"));
				}
            }
			else
            {
				int partyGirl = NPC.FindFirstNPC(NPCID.PartyGirl);
				if (partyGirl >= 0 && Main.rand.NextBool(4))
				{
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.PartyGirlDialogue1", Main.npc[partyGirl].GivenName));
				}
				
				// These are things that the NPC has a chance of telling you when you talk to it.
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue1"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue2"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue3"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue4"));
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.CommonDialogue"), 0.8f);
				chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.RareDialogue"), 0.5f);

				VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();
				if(config.EnableProfanity == true)
				{
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.VeryRareDialogueExp"), 0.4f);
				}
				else
				{
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.VeryRareDialogueSafe"), 0.4f);
				}

				NumberOfTimesTalkedTo++;
				if (NumberOfTimesTalkedTo >= 10)
				{
					// This counter is linked to a single instance of the NPC, so if Claptrap is killed, the counter will reset.
					chat.Add(Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.TalkALot"));
				}
            }

			string chosenChat = chat; // chat is implicitly cast to a string. This is where the random choice is made.

			// // Here is some additional logic based on the chosen chat line. In this case, we want to display an item in the corner for StandardDialogue4.
			// if (chosenChat == Language.GetTextValue("Mods.Vaultaria.NPCs.Claptrap.Dialogue.StandardDialogue1")) {
			// 	// Main.npcChatCornerItem shows a single item in the corner, like the Angler Quest chat.
			// 	Main.npcChatCornerItem = ItemID.HiveBackpack;
			// }

			return chosenChat;
		}

		public override void SetChatButtons(ref string button1, ref string button2)
		{
			if (currentIndex == 0)
			{
				ChatButtons(ref button1, "Shop", ref button2, "Vaultaria");
			}
			else if (currentIndex == 1)
			{
				ChatButtons(ref button1, "MainMenu", ref button2, "Eridium");
			}
			else if (currentIndex == 2)
			{
				ChatButtons(ref button1, "Back", ref button2, "Incendiary");
			}
			else if (currentIndex == 3)
			{
				ChatButtons(ref button1, "Back", ref button2, "Shock");
			}
			else if (currentIndex == 4)
			{
				ChatButtons(ref button1, "Back", ref button2, "Corrosive");
			}
			else if (currentIndex == 5)
			{
				ChatButtons(ref button1, "Back", ref button2, "Explosive");
			}
			else if (currentIndex == 6)
			{
				ChatButtons(ref button1, "Back", ref button2, "Slag");
			}
			else if (currentIndex == 7)
			{
				ChatButtons(ref button1, "Back", ref button2, "Cryo");
			}
			else if (currentIndex == 8)
			{
				ChatButtons(ref button1, "Back", ref button2, "Radiation");
			}
			else if (currentIndex == 9)
			{
				ChatButtons(ref button1, "Back", ref button2, "DoublePenetrating");
			}
			else if (currentIndex == 10)
			{
				ChatButtons(ref button1, "Back", ref button2, "Trickshot");
			}
			else if (currentIndex == 11)
			{
				ChatButtons(ref button1, "Back", ref button2, "Accessories");
			}
			else if (currentIndex == 12)
			{
				ChatButtons(ref button1, "Back", ref button2, "Ammo");
			}
			else if (currentIndex == 13)
			{
				ChatButtons(ref button1, "Back", ref button2, "Vaults");
			}
			else if (currentIndex == pages.Length - 1)
			{
				ChatButtons(ref button1, "Back", ref button2, "MainMenu");
			}
		}

		// Sets what text should be shown when a button is pressed
		private void ChatButtons(ref string button1, string text1, ref string button2, string text2)
		{
			button1 = Language.GetTextValue($"Mods.Vaultaria.NPCs.Claptrap.Buttons.{text1}");
			button2 = Language.GetTextValue($"Mods.Vaultaria.NPCs.Claptrap.Buttons.{text2}");
		}

        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
			if (firstButton)
			{
				if (pages[currentIndex] == ("MainMenu", ItemID.None))
				{
					shop = ShopName;
				}
				else
				{
					currentIndex--;
				}
			}
			else
			{
				currentIndex++;
				if (currentIndex >= pages.Length)
				{
					currentIndex = 0; // Loop forwards
				}
			}

			string chatText;
			if (pages[currentIndex] == ("MainMenu", ItemID.None)) // Fixes the issue with going back to the main menu and it not showing a proper dialogue message
			{
				chatText = GetChat();
			}
			else // Else shows the regular vaultaria pages
			{
				chatText = Language.GetTextValue($"Mods.Vaultaria.NPCs.Claptrap.VaultarianInfo.{pages[currentIndex].buttonTitle}");
			}

			Main.npcChatText = chatText;
			Main.npcChatCornerItem = pages[currentIndex].cornerPicture;
        }
		
		// Not completely finished, but below is what the NPC will sell
		public override void AddShops() {
			var npcShop = new NPCShop(Type, ShopName)
				.Add<GearboxProjectileConvergence>()
				.Add<GearboxRenegade>()
				.Add<GearboxMuckamuck>()
				.Add<VaultHuntersRelic>();

			// if (ModContent.GetInstance<VaultariaConfig>().ExampleWingsToggle) {
			// 	npcShop.Add<ExampleWings>(ExampleConditions.InExampleBiome);
			// }

			// if (ModContent.TryFind("SummonersAssociation/BloodTalisman", out ModItem bloodTalisman)) {
			// 	npcShop.Add(bloodTalisman.Type);
			// }

			npcShop.Register(); // Name of this shop tab
		}

		public override void ModifyActiveShop(string shopName, Item[] items) {
			foreach (Item item in items) {
				// Skip 'air' items and null items.
				if (item == null || item.type == ItemID.None) {
					continue;
				}

				// If NPC is shimmered then reduce all prices by 50%.
				if (NPC.IsShimmerVariant) {
					int value = item.shopCustomPrice ?? item.value;

					if(value > 0)
                    {
						item.shopCustomPrice = value / 2;
                    }
				}
			}
		}

		// Make this Town NPC teleport to the King and/or Queen statue when triggered. Return toKingStatue for only King Statues. Return !toKingStatue for only Queen Statues. Return true for both.
		public override bool CanGoToStatue(bool toKingStatue) => true;

		// Create a square of pixels around the NPC on teleport.
		public void StatueTeleport() {
			for (int i = 0; i < 30; i++) {
				Vector2 position = Main.rand.NextVector2Square(-20, 21);
				if (Math.Abs(position.X) > Math.Abs(position.Y)) {
					position.X = Math.Sign(position.X) * 20;
				}
				else {
					position.Y = Math.Sign(position.Y) * 20;
				}

				Dust.NewDustPerfect(NPC.Center + position, DustID.SparksMech, Vector2.Zero).noGravity = true;
			}
		}

		public override bool ModifyDeathMessage(ref NetworkText customText, ref Color color) {
			// This example shows how you would further customize the message, in this case just for the shimmer variant.
			if (NPC.IsShimmerVariant) {
				customText = NetworkText.FromKey(this.GetLocalizationKey("DeathMessageAlt"), NPC.GetFullNetName());
				color = Color.Yellow;
			}
			return true;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
			cooldown = 10;
			randExtraCooldown = 10;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
			projType = ProjectileID.Bullet;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) {
			multiplier = 12f;
			randomOffset = 2f;
			// SparklingBall is not affected by gravity, so gravityCorrection is left alone.
		}

		public override void LoadData(TagCompound tag) {
			NumberOfTimesTalkedTo = tag.GetInt("numberOfTimesTalkedTo");
		}

		public override void SaveData(TagCompound tag) {
			tag["numberOfTimesTalkedTo"] = NumberOfTimesTalkedTo;
		}
	}
}