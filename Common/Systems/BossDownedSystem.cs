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
        public static bool eyeOfCthulhu = false;
        public static bool evilBoss = false;
        public static bool skeletron = false;
        public static bool deerClops = false;
        public static bool wallOfFlesh = false;
        public static bool pirateShip = false;
        public static bool twins = false;
        public static bool iceGolem = false;
        public static bool martianSaucerCore = false;

        // These 2 methods are used to signal to every client that something in the world just happened
		public override void NetSend(BinaryWriter writer)
        {
			// Order of parameters is important and has to match that of NetReceive
			// writer.WriteFlags(iceGolem);
			// WriteFlags supports up to 8 entries, if you have more than 8 flags to sync, call WriteFlags again.

            writer.WriteFlags(eyeOfCthulhu, evilBoss, skeletron, deerClops, wallOfFlesh, pirateShip, twins, iceGolem);
            writer.WriteFlags(martianSaucerCore);
		}

		public override void NetReceive(BinaryReader reader)
        {
			// Order of parameters is important and has to match that of NetSend
			// reader.ReadFlags(out iceGolem);
			// ReadFlags supports up to 8 entries, if you have more than 8 flags to sync, call ReadFlags again.

            reader.ReadFlags(out eyeOfCthulhu, out evilBoss, out skeletron, out deerClops, out wallOfFlesh, out pirateShip, out twins, out iceGolem);
            reader.ReadFlags(out martianSaucerCore);
		}

        // Use Save/LoadWorldData to ensure the state persists
        public override void SaveWorldData(TagCompound tag)
        {
            tag["eyeOfCthulhu"] = eyeOfCthulhu;
            tag["evilBoss"] = evilBoss;
            tag["skeletron"] = skeletron;
            tag["deerClops"] = deerClops;
            tag["wallOfFlesh"] = wallOfFlesh;
            tag["pirateShip"] = pirateShip;
            tag["twins"] = twins;
            tag["iceGolem"] = iceGolem;
            tag["martianSaucerCore"] = martianSaucerCore;
        }
        
        // Load both flags
        public override void LoadWorldData(TagCompound tag)
        {
            eyeOfCthulhu = tag.GetBool("eyeOfCthulhu");
            evilBoss = tag.GetBool("evilBoss");
            skeletron = tag.GetBool("skeletron");
            deerClops = tag.GetBool("deerClops");
            wallOfFlesh = tag.GetBool("wallOfFlesh");
            pirateShip = tag.GetBool("pirateShip");
            twins = tag.GetBool("twins");
            iceGolem = tag.GetBool("iceGolem");
            martianSaucerCore = tag.GetBool("martianSaucerCore");
        }
        
        // This resets the flag when vaultQueenSlime new world is created
        public override void ClearWorld()
        {
            eyeOfCthulhu = false;
            evilBoss = false;
            skeletron = false;
            deerClops = false;
            wallOfFlesh = false;
            pirateShip = false;
            twins = false;
            iceGolem = false;
            martianSaucerCore = false;
        }
    }
}