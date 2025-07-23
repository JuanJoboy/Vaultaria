using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Epic;
using Vaultaria.Content.Items.Weapons.Ranged.Grenades.Rare;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.Audio;

namespace Vaultaria.Content.Projectiles.Shields
{
    public class HomingSlagBall : ElementalProjectile
    {
        public float slagMultiplier;
        private float elementalChance = 100f;
        private short slagProjectile = ElementalID.SlagProjectile;
        private int slagBuff = ElementalID.SlagBuff;
        private int buffTime = 180;

        public override void SetDefaults()
        {
            // Clone Chlorophytes homing
            Projectile.CloneDefaults(ProjectileID.ChlorophyteBullet);

            // Size
            Projectile.Size = new Vector2(20, 20);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            AIType = ProjectileID.ChlorophyteBullet; // Inherit Chlorophyte AI
            Projectile.ai[0] = 0f;
            Projectile.ai[1] = 0f;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void PostAI()
        {
            base.PostAI();
            Utilities.FrameRotator(4, Projectile);

            int magicMissileEpic = ModContent.ItemType<MagicMissileEpic>();
            int magicMissileRare = ModContent.ItemType<MagicMissileRare>();
            if (IsItem(magicMissileEpic))
            {
                MagicShoot(3);
            }
            if (IsItem(magicMissileRare))
            {
                MagicShoot(0);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item115);

            int numDust = 20;
            for (int i = 0; i < numDust; i++)
            {
                Dust.NewDustPerfect(Projectile.Center, DustID.CorruptSpray).noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, slagMultiplier, player, slagProjectile);
            }

            return false;
        }

        private bool IsItem(int item)
        {
            Player player = Main.player[Projectile.owner];
            Item weapon = player.HeldItem;

            if (weapon != null && weapon.type == item)
            {
                return true;
            }

            return false;
        }

        private void MagicShoot(int numberOfClones)
        {
            if (Projectile.ai[2] == 1f) // Cant use the ai0 cause chlorophyte uses it
            {
                if (Projectile.timeLeft == 580)
                {
                    const float totalSpreadDegrees = 15f;
                    float baseAngle = Projectile.velocity.ToRotation();
                    float angleIncrement;

                    if (numberOfClones == 1)
                    {
                        angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / numberOfClones);
                    }
                    else
                    {
                        angleIncrement = MathHelper.ToRadians(totalSpreadDegrees / (numberOfClones - 1));
                    }

                    baseAngle -= MathHelper.ToRadians(totalSpreadDegrees) / 2f;

                    for (int i = 0; i <= numberOfClones; i++)
                    {
                        float newAngle = baseAngle + (i * angleIncrement);
                        Vector2 newVelocity = newAngle.ToRotationVector2() * Projectile.velocity.Length();

                        Projectile.NewProjectile(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            newVelocity,
                            Projectile.type,
                            15,
                            Projectile.knockBack,
                            Projectile.owner,
                            0f,
                            0f
                        );
                    }

                    Projectile.ai[2] = 0f;
                }
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