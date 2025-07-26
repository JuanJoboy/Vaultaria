using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Epic;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Projectiles.Shields
{
    public class Meteor : ElementalProjectile
    {
        public float explosiveMultiplier = 2f;
        private float elementalChance = 100;
        private short slagProjectile = ElementalID.ExplosiveProjectile;
        private int slagBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 60;

        public override void SetDefaults()
        {
            // Clone Chlorophytes homing
            Projectile.CloneDefaults(ProjectileID.ChlorophyteBullet);

            // Size
            Projectile.Size = new Vector2(20, 20);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;

            // Bullet Config
            Projectile.timeLeft = 3600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;

            AIType = ProjectileID.ChlorophyteBullet; // Inherit Chlorophyte AI
            Projectile.ai[0] = 0f;
            Projectile.ai[1] = 0f;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void PostAI()
        {
            base.PostAI();
            Utilities.FrameRotator(4, Projectile);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item89, Projectile.position);

            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Torch).noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, explosiveMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, explosiveMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, explosiveMultiplier, player, slagProjectile);
            }

            return false;
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