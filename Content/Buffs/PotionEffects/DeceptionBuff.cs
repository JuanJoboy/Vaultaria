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
                player.GetDamage(DamageClass.Melee) += 1.0f; // +100% Melee Damage
            }
            else
            {
                player.GetDamage(DamageClass.Melee) += 0.75f; // +75% Melee Damage
                player.GetDamage(DamageClass.Ranged) += 0.5f; // +50% Ranged/Gun Damage
            }
        }
    }
}