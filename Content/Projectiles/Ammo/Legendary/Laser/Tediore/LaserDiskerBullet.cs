using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Laser.Tediore
{
    public class LaserDiskerBullet : ElementalProjectile
    {
        public float explosiveMultiplier = 0.4f;
        private float explosiveChance = 100f;
        private short explosiveProjectile = ElementalID.RoundExplosiveProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 60;

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Size
            Projectile.Size = new Vector2(28, 6);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
        }
        
        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(explosiveChance))
            {
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(explosiveChance))
            {
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(explosiveChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);
            }

            return false;
        }
        
        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(240, 226, 172);
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive"
            };
        }
    }
}