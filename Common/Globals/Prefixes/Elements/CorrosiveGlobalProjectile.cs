using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Buffs.Prefixes.Elements;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class CorrosiveGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Make sure the projectile came from a player
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
            {
                return;
            }
            if (projectile.type == ProjectileID.SporeCloud)
            {
                return;
            }

            // Check the player's currently held item
            Player player = Main.player[projectile.owner];
            Item held = player.HeldItem;

            if (held != null && held.prefix == ModContent.PrefixType<Corrosive>())
            {
                if (Main.rand.Next(0, 5) <= 1) // 40% Chance
                {
                    Projectile.NewProjectile(
                        player.GetSource_OnHit(target),
                        target.Center,
                        Vector2.Zero,
                        ProjectileID.SporeCloud,
                        (int)(damageDone * 0.4f),
                        0f,
                        player.whoAmI
                    );

                    target.AddBuff(ModContent.BuffType<CorrosiveBuff>(), 120);
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            // Make sure the projectile came from a player
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
            {
                return;
            }
            if (projectile.type == ProjectileID.SporeCloud)
            {
                return;
            }

            // Check the player's currently held item
            Player player = Main.player[projectile.owner];
            Item held = player.HeldItem;

            if (held != null && held.prefix == ModContent.PrefixType<Corrosive>())
            {
                if (Main.rand.Next(0, 5) <= 1) // 40% Chance
                {
                    Projectile.NewProjectile(
                        player.GetSource_OnHit(target),
                        target.Center,
                        Vector2.Zero,
                        ProjectileID.SporeCloud,
                        (int)(info.SourceDamage * 0.4f),
                        0f,
                        player.whoAmI
                    );

                    target.AddBuff(ModContent.BuffType<CorrosiveBuff>(), 120);
                }
            }
        }
    }
}