using Terraria;
using Terraria.ModLoader;

namespace Vaultaria.Content.Buffs.GunEffects
{
    public class DrunkEffect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
    }
}