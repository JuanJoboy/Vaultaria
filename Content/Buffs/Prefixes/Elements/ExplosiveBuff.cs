using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Buffs.Prefixes.Elements
{
    public class ExplosiveBuff : ModBuff
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
            npc.AddBuff(BuffID.Weak, 120);
            npc.AddBuff(BuffID.BrokenArmor, 120);

            Dust.NewDust(npc.position, npc.width, npc.height, DustID.SolarFlare, 0f, 0f, 0, default(Color), 1f);
        }

        // For PvP
        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Weak, 120);
            player.AddBuff(BuffID.BrokenArmor, 120);

            Dust.NewDust(player.position, player.width, player.height, DustID.SolarFlare, 0f, 0f, 0, default(Color), 1f);
        }
    }
}