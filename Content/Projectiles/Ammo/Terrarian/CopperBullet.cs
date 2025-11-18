using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Terrarian
{
    public class CopperBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;

            // Bullet Config
            Projectile.timeLeft = 36000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}