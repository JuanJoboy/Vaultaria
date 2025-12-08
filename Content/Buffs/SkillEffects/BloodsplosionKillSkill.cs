using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Buffs.SkillEffects
{
    public class BloodsplosionKillSkill : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}