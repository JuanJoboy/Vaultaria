using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Buffs.MagicEffects
{
    public class Phaselocked : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Rise(npc);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            Rise(player);
        }

        private void Rise(Entity entity)
        {
            entity.position = entity.oldPosition;
            entity.velocity.Y = 0f; // Go upwards
            entity.velocity.X = 0f;
        }
    }
}