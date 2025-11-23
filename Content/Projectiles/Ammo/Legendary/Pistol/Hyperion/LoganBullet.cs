using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;
using Terraria.ModLoader;
using Vaultaria.Content.Items.Weapons.Ranged.Legendary.Pistol.Hyperion;
using Terraria.Audio;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Pistol.Hyperion
{
    public class LoganBullet : ElementalProjectile
    {
        public float explosiveMultiplier = 0.35f;
        public float incendiaryMultiplier = 0.25f;
        private float elementalChance = 40f;
        private short explosiveProjectile = ElementalID.ExplosiveProjectile;
        private short incendiaryProjectile = ElementalID.IncendiaryProjectile;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int buffTime = 120;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(14, 7);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 0;
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
                Dust.NewDustPerfect(Projectile.Center, DustID.OrangeTorch).noGravity = false;
            }

            Utilities.RocketJump(Projectile, ModContent.ItemType<LogansGun>(), 0.5f, 2f);
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
            if (Projectile.owner != target.whoAmI)
            {
                if (SetElementalChance(elementalChance))
                {
                    Player player = Main.player[Projectile.owner];
                    SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
                    SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.hostile = true;
            Projectile.timeLeft = 2;

            return false;
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "explosive",
                "incendiary"
            };
        }
    }
}