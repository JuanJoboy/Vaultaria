using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.AssaultRifle.Vladof
{
    public class HailBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;

            // Bullet Config
            Projectile.timeLeft = 36000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Utilities.HealOnNPCHit(target, damageDone, 0.25f, Projectile);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Utilities.HealOnPlayerHit(target, info.SourceDamage, 0.25f, Projectile);
        }
    }
}