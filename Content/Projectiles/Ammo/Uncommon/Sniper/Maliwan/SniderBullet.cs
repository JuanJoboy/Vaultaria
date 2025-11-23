using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Uncommon.Sniper.Maliwan
{
    public class SniderBullet : ElementalProjectile
    {
        public float incendiaryMultiplier = 0.4f;
        private float incendiaryChance = 40f;
        private short incendiaryProjectile = ElementalID.IncendiaryProjectile;
        private int incendiaryBuff = ElementalID.IncendiaryBuff;
        private int buffTime = 60;

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
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(incendiaryChance))
            {
                SetElementOnNPC(target, hit, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(incendiaryChance))
            {
                SetElementOnPlayer(target, info, incendiaryMultiplier, player, incendiaryProjectile, incendiaryBuff, buffTime);
            }
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Incendiary"
            };
        }
    }
}