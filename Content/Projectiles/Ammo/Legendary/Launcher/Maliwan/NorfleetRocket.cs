using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Legendary.Launcher.Maliwan
{
    public class NorfleetRocket : ElementalProjectile
    {
        public float shockMultiplier = 1.3f;
        private float elementalChance = 100f;
        private short shockProjectile = ElementalID.ShockProjectile;
        private int shockBuff = ElementalID.ShockBuff;
        private int buffTime = 90;

        private NPC.HitInfo info;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(60, 60);

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
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void AI()
        {
            base.AI();
            Utilities.FrameRotator(6, Projectile);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item94, Projectile.position);

            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.Electric).noGravity = false;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            for(int i = 0; i < Main.npc.Length; i++)
            {
                if (SetElementalChance(elementalChance))
                {
                    if(Vector2.Distance(Main.npc[i].Center, Projectile.Center) < 200 && !Main.npc[i].townNPC)
                    {
                        SetElementOnNPC(Main.npc[i], hit, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
                        info = hit;
                    }
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Projectile.owner != target.whoAmI)
            {
                if (SetElementalChance(elementalChance))
                {
                    Player player = Main.player[Projectile.owner];
                    SetElementOnPlayer(target, info, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[Projectile.owner];
            info.SourceDamage = 400;

            for(int i = 0; i < Main.npc.Length; i++)
            {
                if(Vector2.Distance(Main.npc[i].Center, Projectile.Center) < 200 && !Main.npc[i].townNPC && Main.npc[i].active && Main.npc[i] != null)
                {
                    SetElementOnNPC(Main.npc[i], info, shockMultiplier, player, shockProjectile, shockBuff, buffTime);
                }
            }

            if (SetElementalChance(elementalChance))
            {
                SetElementOnTile(Projectile, shockMultiplier, player, shockProjectile);
            }

            return false;
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