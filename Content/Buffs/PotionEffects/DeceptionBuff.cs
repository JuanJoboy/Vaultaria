using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Vaultaria.Content.Buffs.PotionEffects
{
    public class DeceptionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Invisibility, 1);
            player.AddBuff(BuffID.Swiftness, 1);
            player.AddBuff(BuffID.Regeneration, 1);

            player.GetDamage(DamageClass.Ranged) += 2.0f; // +200% Ranged/Gun Damage
            player.GetDamage(DamageClass.Melee) += 3.0f; // +300% Melee Damage
        }
    }
}