using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Buffs.SkillEffects
{
    public class MetalStormKillSkill : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}