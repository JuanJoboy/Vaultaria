using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;

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
        }
        
        // This resets the flag when a new world is created
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
        }
    }
}