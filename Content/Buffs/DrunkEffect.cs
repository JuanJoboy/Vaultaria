using Terraria;
using Terraria.ModLoader;

namespace Vaultaria.Content.Buffs
{
    public class DrunkEffect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // Image is 27x34 Resolution
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}