using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class ExplosiveGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Make sure the projectile came from a player
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
            {
                return;
            }
            if (projectile.type == ProjectileID.DD2ExplosiveTrapT2Explosion)
            {
                return;
            }

            // Check the player's currently held item
            Player player = Main.player[projectile.owner];
            Item held = player.HeldItem;

            if (held != null && held.prefix == ModContent.PrefixType<Explosive>())
            {
                if (Main.rand.Next(0, 5) <= 1) // 40% Chance
                {
                    Projectile.NewProjectile(
                        player.GetSource_OnHit(target),
                        target.Center,
                        new Vector2(0, -4),

                        ProjectileID.DD2ExplosiveTrapT2Explosion,
                        (int)(damageDone * 0.4f),
                        0f,
                        player.whoAmI
                    );

                    target.AddBuff(ModContent.BuffType<ExplosiveBuff>(), 60);
                }
            }
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
            {
                return true;
            }
            if (projectile.type == ProjectileID.DD2ExplosiveTrapT2Explosion)
            {
                return true;
            }

            Player player = Main.player[projectile.owner];
            Item held = player.HeldItem;

            if (held != null && held.prefix == ModContent.PrefixType<Explosive>())
            {
                if (Main.rand.Next(0, 5) <= 1) // 40% Chance
                {
                    Projectile.NewProjectile(
                        projectile.GetSource_FromThis(),
                        projectile.Center,
                        Vector2.Zero,
                        ProjectileID.DD2ExplosiveTrapT2Explosion,
                        (int)(player.dpsDamage * 0.4f),
                        0f,
                        player.whoAmI
                    );

                    projectile.Kill();
                    return false;
                }
            }

            return true;
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            // Make sure the projectile came from a player
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
            {
                return;
            }
            if (projectile.type == ProjectileID.DD2ExplosiveTrapT2Explosion)
            {
                return;
            }

            // Check the player's currently held item
            Player player = Main.player[projectile.owner];
            Item held = player.HeldItem;

            if (held != null && held.prefix == ModContent.PrefixType<Explosive>())
            {
                if (Main.rand.Next(0, 5) <= 1) // 40% Chance
                {
                    Projectile.NewProjectile(
                        player.GetSource_OnHit(target),
                        target.Center,
                        Vector2.Zero,
                        ProjectileID.DD2ExplosiveTrapT2Explosion,
                        (int)(info.SourceDamage * 0.4f),
                        0f,
                        player.whoAmI
                    );

                    target.AddBuff(ModContent.BuffType<ExplosiveBuff>(), 60);
                }
            }
        }
    }
}