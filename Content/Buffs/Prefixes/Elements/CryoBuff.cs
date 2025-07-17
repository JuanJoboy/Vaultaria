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
            npc.AddBuff(BuffID.Frozen, 120);
            npc.AddBuff(BuffID.Frostburn2, 120);

            // Set velocity to zero to freeze the NPC
            npc.velocity.X = 0;
            npc.velocity.Y = 0;

            npc.netUpdate = true; // Ensures the frozen state is synced in multiplayer

            Dust.NewDust(npc.position, npc.width, npc.height, DustID.IceTorch, 0f, 0f, 0, default(Color), 1.3f);
        }

        // For PvP
        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Frozen, 120);
            player.AddBuff(BuffID.Frostburn2, 120);

            // Set velocity to zero to freeze the player
            player.velocity.X = 0;
            player.velocity.Y = 0;

            // Prevent player movement
            player.controlLeft = false;
            player.controlRight = false;
            player.controlUp = false;
            player.controlDown = false;
            player.controlJump = false;
            player.controlHook = false;
            player.controlUseItem = false; // Prevent attacking while frozen

            Dust.NewDust(player.position, player.width, player.height, DustID.IceTorch, 0f, 0f, 0, default(Color), 1.3f);
        }
    }
}