using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Melee
{
    public class BuzzAxeBombardier : ElementalProjectile
    {
        public float explosiveMultiplier = 1f;
        public float incendiaryMultiplier = 0.25f;
        private float elementalChance = 100f;
        private short explosiveProjectile = ElementalID.ExplosiveProjectile;
        private short incendiaryProjectile = ElementalID.IncendiaryProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int buffTime = 60;


        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 8);
            Projectile.scale = 2.1f;

            // Damage
            Projectile.damage = 25;
            Projectile.CritChance = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            base.AI();
            Utilities.FrameRotator(6, Projectile);

            Projectile.velocity.Y += 0.15f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
                SetElementOnNPC(target, hit, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
                SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);
                SetElementOnTile(Projectile, incendiaryMultiplier, player, incendiaryProjectile);
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

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive",
                "Incendiary"
            };
        }
    }
}