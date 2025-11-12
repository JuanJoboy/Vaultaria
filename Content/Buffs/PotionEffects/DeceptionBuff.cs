using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Melee;

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
            player.invis = true;
            player.moveSpeed += 0.25f;
            player.lifeRegen += 4;

            if (player.HeldItem.type == ModContent.ItemType<ZerosSword>())
            {
                player.GetDamage(DamageClass.Melee) += 3.0f; // +300% Melee Damage
            }
            else
            {
                player.GetDamage(DamageClass.Melee) += 1.5f; // +150% Melee Damage
                player.GetDamage(DamageClass.Ranged) += 1.0f; // +100% Ranged/Gun Damage
            }
        }
    }
}