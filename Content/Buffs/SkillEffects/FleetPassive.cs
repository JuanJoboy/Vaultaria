using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Buffs.SkillEffects
{
    public class FleetPassive : ModBuff
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

            if(player.statLife <= player.statLifeMax2 * 0.3)
            {
                float bonusSpeed = Utilities.SkillBonus(27f, 0.1f);

                player.moveSpeed *= bonusSpeed; 
                player.runAcceleration *= bonusSpeed;
                player.accRunSpeed *= bonusSpeed;
                player.maxRunSpeed *= bonusSpeed;   
            }
        }
    }
}