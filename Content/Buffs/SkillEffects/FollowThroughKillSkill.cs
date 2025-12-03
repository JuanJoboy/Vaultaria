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

            int numberOfBossesDefeated = Utilities.DownedBossCounter();

            float baseDamage = 0.15f;
            float baseSpeed = 0.1f;

            float bonusDamage = (numberOfBossesDefeated / 30f) + baseDamage;
            float bonusSpeed = (numberOfBossesDefeated / 20f) + baseSpeed;

            player.GetDamage(DamageClass.Generic) += bonusDamage;

            player.moveSpeed += bonusSpeed; 
            player.runAcceleration += bonusSpeed;
            player.accRunSpeed += bonusSpeed;
            player.maxRunSpeed += bonusSpeed;
            player.wingRunAccelerationMult += bonusSpeed;
            player.wingAccRunSpeed += bonusSpeed;
        }
    }
}