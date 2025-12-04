using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Buffs.SkillEffects
{
    public class FollowThroughKillSkill : ModBuff
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

            float bonusDamage = Utilities.SkillBonus(30f, 0.15f);
            float bonusSpeed = Utilities.SkillBonus(20f, 0.1f);

            player.GetDamage(DamageClass.Generic) *= bonusDamage;

            player.moveSpeed *= bonusSpeed; 
            player.runAcceleration *= bonusSpeed;
            player.accRunSpeed *= bonusSpeed;
            player.maxRunSpeed *= bonusSpeed;
        }
    }
}