using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Laser.Tediore
{
    public class LaserDiskerGrenade : ElementalProjectile
    {
        public float explosiveMultiplier = 2f;
        private float elementalChance = 100f;
        private short explosiveProjectile = ElementalID.LargeExplosiveProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 60;


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Size
            Projectile.Size = new Vector2(54, 54);
            Projectile.scale = 1f;

            // Damage
            Projectile.damage = 0;
            Projectile.CritChance = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
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
            Utilities.FrameRotator(6, Projectile);

            Projectile.velocity.Y += 0.25f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);
            }

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            // Create an explosion effect when the projectile dies
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            // Create dust particles for the explosion
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default(Color), 1f);
            }
        }

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(0, 0, 0);
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