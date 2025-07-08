using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class ExplosiveGlobal : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (item.prefix == ModContent.PrefixType<Explosive>())
            {
                if (Main.rand.Next(0, 5) <= 1) // 40% Chance
                {
                    Projectile.NewProjectile(
                        player.GetSource_OnHit(target),
                        target.Center,
                        Vector2.Zero,
                        ProjectileID.DD2ExplosiveTrapT2Explosion,
                        (int)(damageDone * 0.4f),
                        0f,
                        player.whoAmI
                    );

                    target.AddBuff(ModContent.BuffType<ExplosiveBuff>(), 60);
                }
            }
        }

        public override void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
        {
            if (item.prefix == ModContent.PrefixType<Explosive>())
            {
                if (Main.rand.Next(0, 5) <= 1) // 40% Chance
                {
                    Projectile.NewProjectile(
                        player.GetSource_OnHit(target),
                        target.Center,
                        Vector2.Zero,
                        ProjectileID.DD2ExplosiveTrapT2Explosion,
                        (int)(hurtInfo.SourceDamage * 0.4f),
                        0f,
                        player.whoAmI
                    );

                    target.AddBuff(ModContent.BuffType<ExplosiveBuff>(), 60);
                }
            }
        }
    }
}