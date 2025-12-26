using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Grenades.Legendary
{
    public class Breath : ElementalProjectile
    {
        public float incendiaryMultiplier = 0.3f;
        private float elementalChance = 100f;
        private short incendiaryProjectile = ElementalID.IncendiaryProjectile;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int buffTime = 300;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(10, 18);
            Projectile.scale = 1.6f;

            // Damage
            Projectile.damage = 1;
            Projectile.CritChance = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 2;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            base.AI();
            Utilities.FrameRotator(3, Projectile);

            Projectile.velocity.Y += 0.185f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Shoot();

            Projectile.velocity.X *= 0.0f;
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            RepeatedlyShoot();

            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            RepeatedlyShoot();

            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override void OnKill(int timeLeft)
        {
            Shoot();

            SoundEngine.PlaySound(SoundID.Item20, Projectile.position);

            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 3f);
            }
        }

        private void Shoot()
        {
            if(Projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    Projectile.velocity,
                    ProjectileID.ApprenticeStaffT3Shot,
                    Projectile.damage,
                    Projectile.knockBack
                );   
            }
        }

        private void RepeatedlyShoot()
        {
            for (int i = 0; i < 20; i++)
            {
                Shoot();
            }
        }
    }
}