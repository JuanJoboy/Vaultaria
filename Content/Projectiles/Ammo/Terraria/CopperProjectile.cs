using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Terraria
{
    public class CopperProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            // Size
            Projectile.Size = new Vector2(20, 2);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
        }
        
        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}