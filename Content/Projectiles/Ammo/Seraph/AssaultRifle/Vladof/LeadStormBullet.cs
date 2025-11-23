using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Seraph.AssaultRifle.Vladof
{
    public class LeadStormBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 2);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 1;

            Projectile.timeLeft = 36000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}