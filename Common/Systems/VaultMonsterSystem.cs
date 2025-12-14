using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Vaultaria.Common.Systems
{
	public class VaultMonsterSystem : ModSystem
	{
        public static bool vaultKingSlime = false;
        public static bool vaultKingSlimeDR = false;
        public static bool vaultEyeOfCthulhu = false;
        public static bool vaultEyeOfCthulhuDR = false;
        public static bool vaultQueenBee = false;
        public static bool vaultQueenBeeDR = false;
        public static bool vaultDeerClops = false;
        public static bool vaultDeerClopsDR = false;
        public static bool vaultSkeletron = false;
        public static bool vaultSkeletronDR = false;

        public static bool vaultQueenSlime = false;
        public static bool vaultQueenSlimeDR = false;
        public static bool vaultTwins = false;
        public static bool vaultTwinsDR = false;
        public static bool vaultSkeletronPrime = false;
        public static bool vaultSkeletronPrimeDR = false;
        public static bool vaultBetsy = false;
        public static bool vaultBetsyDR = false;
        public static bool vaultPlantera = false;
        public static bool vaultPlanteraDR = false;
        public static bool vaultGolem = false;
        public static bool vaultGolemDR = false;
        public static bool vaultDukeFishron = false;
        public static bool vaultDukeFishronDR = false;
        public static bool vaultEmpress = false;
        public static bool vaultEmpressDR = false;
        public static bool vaultLunaticCultist = false;
        public static bool vaultLunaticCultistDR = false;
        public static bool vaultMoonLord = false;
        public static bool vaultMoonLordDR = false;

        public override void PostUpdateWorld()
        {
            base.PostUpdateWorld();

			if(Main.netMode != NetmodeID.MultiplayerClient)
            {
				if (unlockedClaptrapSpawn == false)
				{
					int spawnX = Main.spawnTileX * 16;
					int spawnY = Main.spawnTileY * 16;
					NPC claptrap = NPC.NewNPCDirect(NPC.GetSource_None(), new Vector2(spawnX, spawnY), ModContent.NPCType<Claptrap>());
					
					unlockedClaptrapSpawn = true;
					NetMessage.SendData(MessageID.SyncNPC, number: claptrap.whoAmI);
					NetMessage.SendData(MessageID.WorldData);
				}
            }
        }

		public override void NetSend(BinaryWriter writer)
        {
            writer.WriteFlags(vaultKingSlime, vaultEyeOfCthulhu, vaultQueenBee, vaultDeerClops, vaultSkeletron, vaultQueenSlime, vaultTwins, vaultSkeletronPrime);
            writer.WriteFlags(vaultBetsy, vaultPlantera, vaultGolem, vaultDukeFishron, vaultEmpress, vaultLunaticCultist, vaultMoonLord);
		}

		public override void NetReceive(BinaryReader reader)
        {
            reader.ReadFlags(out vaultKingSlime, out vaultEyeOfCthulhu, out vaultQueenBee, out vaultDeerClops, out vaultSkeletron, out vaultQueenSlime, out vaultTwins, out vaultSkeletronPrime);
            reader.ReadFlags(out vaultBetsy, out vaultPlantera, out vaultGolem, out vaultDukeFishron, out vaultEmpress, out vaultLunaticCultist, out vaultMoonLord);
		}

		public override void SaveWorldData(TagCompound tag)
		{
            tag["vaultKingSlime"] = vaultKingSlime;
            tag["vaultKingSlimeDR"] = vaultKingSlimeDR; // DR = Don't Respawn
            tag["vaultEyeOfCthulhu"] = vaultEyeOfCthulhu;
            tag["vaultEyeOfCthulhuDR"] = vaultEyeOfCthulhuDR;
            tag["vaultQueenBee"] = vaultQueenBee;
            tag["vaultQueenBeeDR"] = vaultQueenBeeDR;
            tag["vaultDeerClops"] = vaultDeerClops;
            tag["vaultDeerClopsDR"] = vaultDeerClopsDR;
            tag["vaultSkeletron"] = vaultSkeletron;
            tag["vaultSkeletronDR"] = vaultSkeletronDR;

            tag["vaultQueenSlime"] = vaultQueenSlime;
            tag["vaultQueenSlimeDR"] = vaultQueenSlimeDR;
            tag["vaultTwins"] = vaultTwins;
            tag["vaultTwinsDR"] = vaultTwinsDR;
            tag["vaultSkeletronPrime"] = vaultSkeletronPrime;
            tag["vaultSkeletronPrimeDR"] = vaultSkeletronPrimeDR;
            tag["vaultBetsy"] = vaultBetsy;
            tag["vaultBetsyDR"] = vaultBetsyDR;
            tag["vaultPlantera"] = vaultPlantera;
            tag["vaultPlanteraDR"] = vaultPlanteraDR;
            tag["vaultGolem"] = vaultGolem;
            tag["vaultGolemDR"] = vaultGolemDR;
            tag["vaultDukeFishron"] = vaultDukeFishron;
            tag["vaultDukeFishronDR"] = vaultDukeFishronDR;
            tag["vaultEmpress"] = vaultEmpress;
            tag["vaultEmpressDR"] = vaultEmpressDR;
            tag["vaultLunaticCultist"] = vaultLunaticCultist;
            tag["vaultLunaticCultistDR"] = vaultLunaticCultistDR;
            tag["vaultMoonLord"] = vaultMoonLord;
            tag["vaultMoonLordDR"] = vaultMoonLordDR;
		}

		public override void LoadWorldData(TagCompound tag)
		{
            vaultKingSlime = tag.GetBool("vaultKingSlime");
            vaultKingSlimeDR = tag.GetBool("vaultKingSlimeDR");
            vaultEyeOfCthulhu = tag.GetBool("vaultEyeOfCthulhu");
            vaultEyeOfCthulhuDR = tag.GetBool("vaultEyeOfCthulhuDR");
            vaultQueenBee = tag.GetBool("vaultQueenBee");
            vaultQueenBeeDR = tag.GetBool("vaultQueenBeeDR");
            vaultDeerClops = tag.GetBool("vaultDeerClops");
            vaultDeerClopsDR = tag.GetBool("vaultDeerClopsDR");
            vaultSkeletron = tag.GetBool("vaultSkeletron");
            vaultSkeletronDR = tag.GetBool("vaultSkeletronDR");

            vaultQueenSlime = tag.GetBool("vaultQueenSlime");
            vaultQueenSlimeDR = tag.GetBool("vaultQueenSlimeDR");
            vaultTwins = tag.GetBool("vaultTwins");
            vaultTwinsDR = tag.GetBool("vaultTwinsDR");
            vaultSkeletronPrime = tag.GetBool("vaultSkeletronPrime");
            vaultSkeletronPrimeDR = tag.GetBool("vaultSkeletronPrimeDR");
            vaultBetsy = tag.GetBool("vaultBetsy");
            vaultBetsyDR = tag.GetBool("vaultBetsyDR");
            vaultPlantera = tag.GetBool("vaultPlantera");
            vaultPlanteraDR = tag.GetBool("vaultPlanteraDR");
            vaultGolem = tag.GetBool("vaultGolem");
            vaultGolemDR = tag.GetBool("vaultGolemDR");
            vaultDukeFishron = tag.GetBool("vaultDukeFishron");
            vaultDukeFishronDR = tag.GetBool("vaultDukeFishronDR");
            vaultEmpress = tag.GetBool("vaultEmpress");
            vaultEmpressDR = tag.GetBool("vaultEmpressDR");
            vaultLunaticCultist = tag.GetBool("vaultLunaticCultist");
            vaultLunaticCultistDR = tag.GetBool("vaultLunaticCultistDR");
            vaultMoonLord = tag.GetBool("vaultMoonLord");
            vaultMoonLordDR = tag.GetBool("vaultMoonLordDR");
		}

		public override void ClearWorld()
		{
            vaultKingSlime = false;
            vaultKingSlimeDR = false;
            vaultEyeOfCthulhu = false;
            vaultEyeOfCthulhuDR = false;
            vaultQueenBee = false;
            vaultQueenBeeDR = false;
            vaultDeerClops = false;
            vaultDeerClopsDR = false;
            vaultSkeletron = false;
            vaultSkeletronDR = false;

            vaultQueenSlime = false;
            vaultQueenSlimeDR = false;
            vaultTwins = false;
            vaultTwinsDR = false;
            vaultSkeletronPrime = false;
            vaultSkeletronPrimeDR = false;
            vaultBetsy = false;
            vaultBetsyDR = false;
            vaultPlantera = false;
            vaultPlanteraDR = false;
            vaultGolem = false;
            vaultGolemDR = false;
            vaultDukeFishron = false;
            vaultDukeFishronDR = false;
            vaultEmpress = false;
            vaultEmpressDR = false;
            vaultLunaticCultist = false;
            vaultLunaticCultistDR = false;
            vaultMoonLord = false;
            vaultMoonLordDR = false;
		}
	}
}