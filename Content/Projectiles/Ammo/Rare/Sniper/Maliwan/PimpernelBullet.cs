using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Utilities;
using Terraria.DataStructures;

namespace Vaultaria.Content.Projectiles.Ammo.Rare.Sniper.Maliwan
{
    public class PimpernelBullet : ElementalProjectile
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
            Projectile.penetrate = 2;
            Projectile.aiStyle = 0;

            // Bullet Config
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
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

        // Could be good for Bore cause it infinitely creates projectiles that one shot anything. Killed Dungeon Guardian in a few seconds
        // public override void OnKill(int timeLeft)
        // {
        //     base.OnKill(timeLeft);

        //     Projectile.aiStyle = 8;

        //     Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, Projectile.type, Projectile.damage, 1, Projectile.owner);
        //     Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, Projectile.type, Projectile.damage, 1, Projectile.owner);
        //     Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, Projectile.type, Projectile.damage, 1, Projectile.owner);
        //     Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, Projectile.type, Projectile.damage, 1, Projectile.owner);
        // }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            CreateFourProjectiles(Projectile.velocity);

            if (SetElementalChance(slagChance))
            {
                SetElementOnNPC(target, hit, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];
            CreateFourProjectiles(Projectile.velocity);

            if (SetElementalChance(slagChance))
            {
                SetElementOnPlayer(target, info, slagMultiplier, player, slagProjectile, slagBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            CreateFourProjectiles(oldVelocity);
            return base.OnTileCollide(oldVelocity);
        }
        
        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Slag"
            };
        }

        private void CreateFourProjectiles(Vector2 oldVelocity)
        {
            int horizontal = 30;
            int vertical = 30;

            // Backwards
            SpawnChildren(horizontal, -vertical, -oldVelocity / 3);
            SpawnChildren(horizontal * 2, -vertical * 2, -oldVelocity / 2);

            // Forwards
            if(Projectile.direction == 1)
            {
                float oldVelX = oldVelocity.X;
                oldVelocity.X = oldVelocity.Y;
                oldVelocity.Y = -oldVelX;
                Projectile.rotation = oldVelocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            else
            {
                float oldVelX = oldVelocity.X;
                oldVelocity.X = -oldVelocity.Y;
                oldVelocity.Y = oldVelX;
                Projectile.rotation = oldVelocity.ToRotation() - MathHelper.ToRadians(90f);
            }

            SpawnChildren(horizontal, -vertical, oldVelocity / 3);
            SpawnChildren(horizontal * 2, -vertical * 2, oldVelocity / 2);
        }

        private void SpawnChildren(int posX, int posY, Vector2 vel)
        {
            Vector2 positionToGetTo = Projectile.position + new Vector2(posX, posY);
            Vector2 projectileTraveling = Vector2.Lerp(Projectile.position, positionToGetTo, 0.1f);

            Projectile.NewProjectileDirect(Projectile.GetSource_Death(), projectileTraveling, vel, ModContent.ProjectileType<PimpernelChildBullet>(), Projectile.damage, 2);
        }
    }
}