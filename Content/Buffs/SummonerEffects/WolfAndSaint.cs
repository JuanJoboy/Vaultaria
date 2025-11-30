using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Build.Construction;
using Vaultaria.Content.Projectiles.Summoner.Minion;

namespace Vaultaria.Content.Buffs.SummonerEffects
{
    public class WolfAndSaint : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<Wolf>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Saint>()] > 0)
            {
                player.buffTime[buffIndex] = 1000000000;
                return;
            }

            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}