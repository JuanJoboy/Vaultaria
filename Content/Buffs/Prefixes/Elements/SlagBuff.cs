using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Buffs.Prefixes.Elements
{
    public class SlagBuff : ModBuff
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
            npc.AddBuff(BuffID.Ichor, 120);
            npc.AddBuff(BuffID.Slow, 120);

            npc.color = Color.Purple;
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.VilePowder, 0f, 0f, 0, default(Color), 1f);
        }

        // For PvP
        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Ichor, 120);
            player.AddBuff(BuffID.Slow, 120);

            player.skinColor = Color.Purple;
            Dust.NewDust(player.position, player.width, player.height, DustID.VilePowder, 0f, 0f, 0, default(Color), 1f);
        }
    }
}