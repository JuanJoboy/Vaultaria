using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Sniper.Maliwan
{
    public class VolcanoBullet : ElementalProjectile
    {
        public float incendiaryMultiplier = 1f;
        public float explosiveMultiplier = 1f;
        private float elementalChance = 100f;
        private short incendiaryProjectile = ProjectileID.SolarWhipSwordExplosion;
        private short explosiveProjectile = ElementalID.ExplosiveProjectile;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int explosiveBuff = ElementalID.ExplosiveBuff;
        private int buffTime = 180;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(22, 6);

            // Damage
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

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
                SetElementOnNPC(target, hit, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
                SetElementOnPlayer(target, info, explosiveMultiplier, player, explosiveProjectile, explosiveBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, incendiaryMultiplier, player, incendiaryProjectile);
                SetElementOnTile(Projectile, explosiveMultiplier, player, explosiveProjectile);
            }

            return false;
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Incendiary",
                "Explosive"
            };
        }
    }
}