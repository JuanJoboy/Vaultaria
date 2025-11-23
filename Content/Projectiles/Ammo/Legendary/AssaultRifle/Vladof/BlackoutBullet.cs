using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.AssaultRifle.Vladof
{
    public class BlackoutBullet : ElementalProjectile
    {
        public float explosiveMultiplier = 1f;
        public float slagMultiplier = 0.5f;
        private float elementalChance = 10f;
        private short explosiveProjectile = ElementalID.ExplosiveProjectile;
        private short slagProjectile = ElementalID.SlagProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int slagBuff = ElementalID.SlagBuff;
        private int buffTime = 90;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(26, 4);

            // Damage
            Projectile.damage = 15;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item62, Projectile.position);

            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.YellowTorch).noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            
            if (SetElementalChance(elementalChance))
            {
                SetElementOnNPC(target, hit, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }

            SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];
            
            if (SetElementalChance(elementalChance))
            {
                SetElementOnPlayer(target, info, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }

            SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[Projectile.owner];
            
            if (SetElementalChance(elementalChance))
            {
                SetElementOnTile(Projectile, slagMultiplier, player, slagProjectile);
            }

            SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);

            return false;
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive",
                "Slag"
            };
        }
    }
}