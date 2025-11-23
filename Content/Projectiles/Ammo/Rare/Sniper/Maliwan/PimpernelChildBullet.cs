using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.DataStructures;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Sniper.Maliwan
{
    public class PimpernelChildBullet : ElementalProjectile
    {
        public float slagMultiplier = 0.4f;
        private float slagChance = 70f;
        private short slagProjectile = ElementalID.SlagProjectile;
        private int slagBuff = ElementalID.SlagBuff;
        private int buffTime = 180;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(17, 20);

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
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            base.AI();
            Utilities.FrameRotator(6, Projectile);
            Projectile.velocity.Y += 0.175f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(slagChance))
            {
                SetElementOnNPC(target, hit, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            if (SetElementalChance(slagChance))
            {
                SetElementOnPlayer(target, info, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Slag"
            };
        }
    }
}