using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;
using System.IO;
using System.Collections;
using Vaultaria.Common.Systems.GenPasses.Vaults;

namespace Vaultaria.Common.Systems
{
    public class BossDownedSystem : ModSystem
    {
        public static bool torchGod = false;
        public static bool evilBoss = false;
        public static bool skeletron = false;
        public static bool deerClops = false;
        public static bool wallOfFlesh = false;
        public static bool pirateShip = false;
        public static bool twins = false;
        public static bool iceGolem = false;
        public static bool martianSaucerCore = false;

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

        // These 2 methods are used to signal to every client that something in the world just happened
		public override void NetSend(BinaryWriter writer)
        {
			// Order of parameters is important and has to match that of NetReceive
			// writer.WriteFlags(iceGolem);
			// WriteFlags supports up to 8 entries, if you have more than 8 flags to sync, call WriteFlags again.

            writer.WriteFlags(torchGod, evilBoss, skeletron, deerClops, wallOfFlesh, pirateShip, twins, iceGolem);
            writer.WriteFlags(martianSaucerCore, vaultKingSlime, vaultEyeOfCthulhu, vaultQueenBee, vaultDeerClops, vaultSkeletron, vaultQueenSlime, vaultTwins);
            writer.WriteFlags(vaultSkeletronPrime, vaultBetsy, vaultPlantera, vaultGolem, vaultDukeFishron, vaultEmpress, vaultLunaticCultist, vaultMoonLord);
		}

		public override void NetReceive(BinaryReader reader)
        {
			// Order of parameters is important and has to match that of NetSend
			// reader.ReadFlags(out iceGolem);
			// ReadFlags supports up to 8 entries, if you have more than 8 flags to sync, call ReadFlags again.

            reader.ReadFlags(out torchGod, out evilBoss, out skeletron, out deerClops, out wallOfFlesh, out pirateShip, out twins, out iceGolem);
            reader.ReadFlags(out martianSaucerCore, out vaultKingSlime, out vaultEyeOfCthulhu, out vaultQueenBee, out vaultDeerClops, out vaultSkeletron, out vaultQueenSlime, out vaultTwins);
            reader.ReadFlags(out vaultSkeletronPrime, out vaultBetsy, out vaultPlantera, out vaultGolem, out vaultDukeFishron, out vaultEmpress, out vaultLunaticCultist, out vaultMoonLord);
		}

        // Use Save/LoadWorldData to ensure the state persists
        public override void SaveWorldData(TagCompound tag)
        {
            tag["torchGod"] = torchGod;
            tag["evilBoss"] = evilBoss;
            tag["skeletron"] = skeletron;
            tag["deerClops"] = deerClops;
            tag["wallOfFlesh"] = wallOfFlesh;
            tag["pirateShip"] = pirateShip;
            tag["twins"] = twins;
            tag["iceGolem"] = iceGolem;
            tag["martianSaucerCore"] = martianSaucerCore;

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
        
        // Load both flags
        public override void LoadWorldData(TagCompound tag)
        {
            torchGod = tag.GetBool("torchGod");
            evilBoss = tag.GetBool("evilBoss");
            skeletron = tag.GetBool("skeletron");
            deerClops = tag.GetBool("deerClops");
            wallOfFlesh = tag.GetBool("wallOfFlesh");
            pirateShip = tag.GetBool("pirateShip");
            twins = tag.GetBool("twins");
            iceGolem = tag.GetBool("iceGolem");
            martianSaucerCore = tag.GetBool("martianSaucerCore");

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
        
        // This resets the flag when vaultQueenSlime new world is created
        public override void ClearWorld()
        {
            torchGod = false;
            evilBoss = false;
            skeletron = false;
            deerClops = false;
            wallOfFlesh = false;
            pirateShip = false;
            twins = false;
            iceGolem = false;
            martianSaucerCore = false;

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