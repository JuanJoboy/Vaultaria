using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Buffs.SkillEffects
{
    public class OnslaughtKillSkill : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            float bonusDamage = Utilities.SkillBonus(50f, 0.05f);
            float bonusSpeed = Utilities.SkillBonus(30f, 0.1f);

            player.GetDamage(DamageClass.Ranged) *= bonusDamage;

            player.moveSpeed *= bonusSpeed; 
            player.runAcceleration *= bonusSpeed;
            player.accRunSpeed *= bonusSpeed;
            player.maxRunSpeed *= bonusSpeed;
        }
    }
}