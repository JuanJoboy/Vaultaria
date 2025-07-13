using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Vaultaria.Content.Prefixes.Weapons;
using Vaultaria.Content.Buffs.GunEffects;

namespace Vaultaria.Common.Globals.Prefixes.GunModifier
{
    public class TrickShotProjectile : GlobalProjectile
    {
        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            // First, ensure the projectile belongs to a player.
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
            {
                return true; // Let vanilla handle collision if not player-owned
            }

            Player player = Main.player[projectile.owner];
            Item weapon = player.HeldItem;

            bool isAffectedByTrickshot = weapon != null && weapon.prefix == ModContent.PrefixType<Trickshot>();
            bool isAffectedByOrcEffect = player.HasBuff(ModContent.BuffType<OrcEffect>());

            if (isAffectedByTrickshot || isAffectedByOrcEffect)
            {
                Reflect(projectile, oldVelocity, player, weapon);

                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, projectile.position); // Example: a subtle bounce sound
                Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Stone, 0f, 0f, 0, default(Color), 1f);

                return false; // Return false to tell vanilla that you handled the collision.
            }

            // If the projectile does NOT have the Trickshot prefix,
            // let vanilla handle the collision (e.g., disappear, stick).
            return true;
        }

        private bool Reflect(Projectile projectile, Vector2 oldVelocity, Player player, Item weapon)
        {
            // Reduce penetration count.
            projectile.penetrate = 2;
            projectile.penetrate--;

            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
                return false;
            }

            // Check if the projectile hit a vertical wall (X velocity changed significantly).
            if (Math.Abs(projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                projectile.velocity.X = -oldVelocity.X; // Reverse X velocity
            }

            // Check if the projectile hit a horizontal surface (Y velocity changed significantly).
            if (Math.Abs(projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                projectile.velocity.Y = -oldVelocity.Y; // Reverse Y velocity
            }

            return true;
        }
    }
}