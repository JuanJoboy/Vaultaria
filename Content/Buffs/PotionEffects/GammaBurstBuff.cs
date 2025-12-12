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

        public override void Update(Player player, ref int buffIndex)
        {
            VaultariaConfig config = ModContent.GetInstance<VaultariaConfig>();

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];

                if(p.active && (p.owner == player.whoAmI) && (p.minion || p.sentry))
                {
                    if(config.KeepMinionSizeTheSameWhenGammaBursting == false)
                    {
                        if(player.HasBuff<GammaBurstBuff>())
                        {
                            p.scale = 2;
                        }
                        else
                        {
                            p.scale = 1;
                        }

                        p.netUpdate = true;
                        NetMessage.SendData(MessageID.SyncProjectile, number: p.whoAmI);
                    }
                }
            }
        }
    }
}