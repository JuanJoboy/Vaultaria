using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.IO;

namespace Vaultaria.Common.Systems
{
    public class BossDownedSystem : ModSystem
    {
        public static bool skeletron = false;
        public static bool deerClops = false;
        public static bool wallOfFlesh = false;
        public static bool twins = false;
        public static bool iceGolem= false;

        // Use Save/LoadWorldData to ensure the state persists
        public override void SaveWorldData(TagCompound tag)
        {
            tag["skeletron"] = skeletron;
            tag["deerClops"] = deerClops;
            tag["wallOfFlesh"] = wallOfFlesh;
            tag["twins"] = twins;
            tag["iceGolem"] = iceGolem;
        }
        
        // Load both flags
        public override void LoadWorldData(TagCompound tag)
        {
            skeletron = tag.GetBool("skeletron");
            deerClops = tag.GetBool("deerClops");
            wallOfFlesh = tag.GetBool("wallOfFlesh");
            twins = tag.GetBool("twins");
            iceGolem = tag.GetBool("iceGolem");
        }
        
        // This resets the flag when a new world is created
        public override void ClearWorld()
        {
            skeletron = false;
            deerClops = false;
            wallOfFlesh = false;
            twins = false;
            iceGolem = false;
        }
    }
}