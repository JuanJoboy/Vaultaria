using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Bandit
{
    public class ZimBullet : ElementalProjectile
    {
        public float cryoMultiplier = 1f;
        private float elementalChance = 100;
        private short cryoProjectile = ElementalID.CryoProjectile;
        private int cryoBuff = ElementalID.CryoBuff;
        private int buffTime = 180;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(35, 2);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
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
                Dust.NewDustPerfect(Projectile.Center, DustID.Frost).noGravity = false;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, cryoMultiplier, player, cryoProjectile, cryoBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, cryoMultiplier, player, cryoProjectile, cryoBuff, buffTime);
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