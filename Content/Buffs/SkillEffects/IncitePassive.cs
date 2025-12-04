using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Buffs.SkillEffects
{
    public class IncitePassive : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            int numberOfBossesDefeated = Utilities.DownedBossCounter();

            float baseFireRate = 0.15f;
            float baseSpeed = 0.1f;

            float bonusDamage = 1 + ((numberOfBossesDefeated / 30f) + baseFireRate);
            float bonusSpeed = 1 + ((numberOfBossesDefeated / 20f) + baseSpeed);

            player.GetDamage(DamageClass.Generic) *= bonusDamage;

            player.moveSpeed *= bonusSpeed; 
            player.runAcceleration *= bonusSpeed;
            player.accRunSpeed *= bonusSpeed;
            player.maxRunSpeed *= bonusSpeed;
        }
    }
}