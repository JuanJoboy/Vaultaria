using Terraria;
using Terraria.ModLoader;

namespace Vaultaria.Content.Buffs.GunEffects
{
    public class OrcEffect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }
    }
}