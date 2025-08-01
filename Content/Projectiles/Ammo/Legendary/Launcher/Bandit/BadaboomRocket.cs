using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Launcher.Bandit;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Bandit
{
    public class BadaboomRocket : ElementalProjectile
    {
        public float explosiveMultiplier = 1.3f;
        private float elementalChance = 100f;
        private short explosiveProjectile = ProjectileID.DD2ExplosiveTrapT3Explosion;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 30;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 20);

            // Damage
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
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.YellowTorch).noGravity = true;
            }

            // Rocket Jump Logic
            Player player = Main.player[Projectile.owner];
            if (player.HasItemInAnyInventory(ModContent.ItemType<Badaboom>()) && player.Distance(Projectile.Center) <= 100) // Within explosion radius
            {
                if (player.Center.X > Projectile.Center.X)
                {
                    player.velocity.X += 7.5f;
                }
                else
                {
                    player.velocity.X -= 7.5f;
                }

                if (player.Center.Y > Projectile.Center.Y)
                {
                    player.velocity.Y = +15f;
                }
                else
                {
                    player.velocity.Y = -15f;
                }
            }
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

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Explosive"
            };
        }
    }
}