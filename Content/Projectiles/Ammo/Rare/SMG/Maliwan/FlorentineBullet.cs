using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.SMG.Maliwan
{
    public class FlorentineBullet : ElementalProjectile
    {
        public float shockMultiplier;
        public float slagMultiplier;
        private float elementalChance = 20f;
        private short shockProjectile = ElementalID.ShockProjectile;
        private short slagProjectile = ElementalID.SlagProjectile;
        private int shockBuff = ElementalID.ShockBuff;
        private int slagBuff = ElementalID.SlagBuff;
        private int buffTime = 180;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 20);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation();

            // This will cycle through all of the frames in the sprite sheet
            int frameSpeed = 4; // How fast you want it to animate (lower = faster)
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.PureSpray).noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
                SetElementOnNPC(target, hit, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
                SetElementOnPlayer(target, info, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Shock",
                "Slag"
            };
        }
    }
}