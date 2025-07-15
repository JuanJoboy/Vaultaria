using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Buffs.Prefixes.Elements
{
    public class CryoBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        // For PvE
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.lifeRegen -= 30; // 30 Damage cause (30 / 120 npc.lifeRegen ticks) * 120 buff time ticks = 30
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.IceTorch, 0f, 0f, 0, default(Color), 1.3f);
        }

        // For PvP
        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 30;
            Dust.NewDust(player.position, player.width, player.height, DustID.IceTorch, 0f, 0f, 0, default(Color), 1.3f);
        }
    }
}