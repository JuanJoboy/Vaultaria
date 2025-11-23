using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Common.Pistol.Maliwan
{
    public class AegisBullet : ElementalProjectile
    {
        public float shockMultiplier = 0.2f;
        private float shockChance = 40f;
        private short shockProjectile = ElementalID.ShockProjectile;
        private int shockBuff = ElementalID.ShockBuff;
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

            if (SetElementalChance(shockChance))
            {
                SetElementOnNPC(target, hit, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(shockChance))
            {
                SetElementOnPlayer(target, info, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
            }
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Shock"
            };
        }
    }
}