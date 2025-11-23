using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Melee
{
    public class BerserkerFists : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(40, 30);
            Projectile.scale = 1.6f;

            // Damage
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = ProjAIStyleID.ShortSword;
            Projectile.damage = 2;
            Projectile.CritChance = 10;
            Projectile.ownerHitCheck = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 0;

            // Fist Config
            Projectile.timeLeft = 20;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            base.AI();
            Utilities.FrameRotator(8, Projectile);
        }
    }
}