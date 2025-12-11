using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Evaluation;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare;

namespace Vaultaria.Content.Projectiles.Grenades.Rare
{
    public class SingularityModule : ElementalProjectile
    {
        public float explosiveMultiplier = 0.15f;
        private float elementalChance = 100f;
        private short explosiveProjectile = ElementalID.SmallExplosiveProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 90;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(8, 17);
            Projectile.scale = 1.4f;

            // Damage
            Projectile.damage = 1;
            Projectile.CritChance = 1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 36000;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.velocity.Y += 0.175f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }

            PullInEnemies();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }

            PullInEnemies();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);
            }

            PullInEnemies();

            return false;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DeerclopsRubbleAttack, Projectile.position);

            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.HallowSpray, 0f, 0f, 100, default(Color), 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.CorruptSpray, 0f, 0f, 100, default(Color), 2f);
            }
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive"
            };
        }

        private void PullInEnemies()
        {
            foreach(NPC npc in Main.ActiveNPCs)
            {
                if(Vector2.Distance(npc.Center, Projectile.Center) < 1000)
                {
                    Utilities.MoveToPosition(npc, Projectile.Center, 30f, 6f);
                }
            }
        }
    }
}