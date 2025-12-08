using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Shotgun.Jakobs
{
    public class TooScoopsBullet : ElementalProjectile
    {
        public float cryoMultiplier = 1f;
        public float explosiveMultiplier = 1f;
        private float cryoChance = 100f;
        private short cryoProjectile = ElementalID.CryoProjectile;
        private short explosiveProjectile = ElementalID.ExplosiveProjectile;
        private int cryoBuff = ElementalID.CryoBuff;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 60;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(14, 14);

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

        public override void OnKill(int timeLeft)
        {
            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.IceTorch).noGravity = false;
                Dust.NewDustPerfect(Projectile.Center, DustID.Smoke).noGravity = false;
            }
        }


        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(cryoChance))
            {
                SetElementOnNPC(target, hit, cryoMultiplier, player, cryoProjectile, cryoBuff, buffTime);
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(cryoChance))
            {
                SetElementOnPlayer(target, info, cryoMultiplier, player, cryoProjectile, cryoBuff, buffTime);
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Cryo"
            };
        }
    }
}