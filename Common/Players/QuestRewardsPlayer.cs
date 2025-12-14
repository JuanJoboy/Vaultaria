using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Vaultaria.Common.Players
{
    public class QuestRewardsPlayer : ModPlayer
    {
        public bool ladyFistCollected;
        public bool swordSplosionCollected;

        public override void SaveData(TagCompound tag)
        {
            tag.Add("ladyFistCollected", ladyFistCollected);
            tag.Add("swordSplosionCollected", swordSplosionCollected);
        }

        public override void LoadData(TagCompound tag)
        {
            ladyFistCollected = tag.GetBool("ladyFistCollected");
            swordSplosionCollected = tag.GetBool("swordSplosionCollected");
        }
    }
}