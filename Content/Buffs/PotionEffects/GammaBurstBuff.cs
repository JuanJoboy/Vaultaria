using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Melee;
using System.Collections.Generic;
using System.Collections;
using Vaultaria.Common.Configs;

namespace Vaultaria.Content.Buffs.PotionEffects
{
    public class GammaBurstBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}