using Terraria;
using Terraria.ModLoader;

namespace Vaultaria.Content.Buffs.AccessoryEffects
{
    public class DeathEffect : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.statLife <= (player.statLifeMax2 * 0.5f))
            {
                player.statLife = player.statLifeMax2 * 100;
            }
        }
    }
}