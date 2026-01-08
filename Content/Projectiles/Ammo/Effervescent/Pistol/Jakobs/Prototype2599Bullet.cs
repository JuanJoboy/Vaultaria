using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.Collections.Generic; // For Lists
using Vaultaria.Common.Utilities;
using Terraria.ModLoader;

namespace Vaultaria.Content.Projectiles.Ammo.Effervescent.Pistol.Jakobs
{
    public class Prototype2599Bullet : ElementalProjectile
    {
        public float radiationMultiplier = 0.5f;
        private float elementalChance = 50;
        private short radiationProjectile = ElementalID.RadiationProjectile;
        private int radiationBuff = ElementalID.RadiationBuff;
        private int buffTime = 90;

        public override void SetDefaults()
        {
            // Size
            Projectile.Size = new Vector2(20, 2);

            // Damage
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;

            // Bullet Config
            Projectile.timeLeft = 600;
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
            Utilities.DustMaker(2, Projectile, DustID.CursedTorch, false);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnNPC(target, hit, radiationMultiplier, player, radiationProjectile, radiationBuff, buffTime);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnPlayer(target, info, radiationMultiplier, player, radiationProjectile, radiationBuff, buffTime);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (SetElementalChance(elementalChance))
            {
                Player player = Main.player[Projectile.owner];
                SetElementOnTile(Projectile, radiationMultiplier, player, radiationProjectile);
            }

            return true;
        }

        public override Vector3 SetProjectileLightColour()
        {
            return new Vector3(243, 255, 93);
        }

        public override List<string> GetElement()
        {
            return new List<string>
            {
                "Radiation"
            };
        }
    }
}