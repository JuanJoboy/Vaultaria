using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Items.Weapons.Melee;

namespace Vaultaria.Content.Buffs.PotionEffects
{
    public class DeceptionCooldownBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}