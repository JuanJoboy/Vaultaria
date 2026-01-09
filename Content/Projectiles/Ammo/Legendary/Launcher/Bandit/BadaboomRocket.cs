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
        private short explosiveProjectile = ElementalID.RoundExplosiveProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 30;

        public override void SetDefaults()
        {
            base.SetDefaults();
            // Size
            Projectile.Size = new Vector2(24, 13);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 36000;
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
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.YellowTorch).noGravity = true;
            }

            Utilities.RocketJump(Projectile, ModContent.ItemType<Badaboom>(), 6.5f, 12f);
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
            if (Projectile.owner != target.whoAmI)
            {
                if (SetElementalChance(elementalChance))
                {
                    Player player = Main.player[Projectile.owner];
                    SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
                }
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

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(253, 221, 3);
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