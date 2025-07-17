using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Common.Utilities;

namespace Vaultaria.Common.Globals.Prefixes.Elements
{
    public class ElementalGlobalProjectile : GlobalProjectile
    {
        // Allows storing firedWeaponPrefixID even if the projectile is:
        // Vanilla (ProjectileID.Bullet, etc.)
        // Not derived from ElementalProjectile
        // Ensures prefix data survives on any projectile instance:

        public int firedWeaponPrefixID; // Stores the weapon prefix ID snapped at the time of projectile creation. It provides a place to store data specific to each projectile instance.
        public override bool InstancePerEntity => true; // This property ensures that a unique instance of ElementalGlobalProjectile is created for every single projectile in the game (both vanilla and modded).

        // Data Separation (Snapshotting) Imagine you have a GlobalProjectile that needs to store information specific to each individual projectile. For example, as in your code, you want each bullet to remember the prefix of the gun that fired it. If InstancePerEntity was false (the default behavior), tModLoader would create only one shared instance of ElementalGlobalProjectile. If you tried to store firedWeaponPrefixID in that single instance, every new projectile fired would overwrite the previous one's data. All bullets in the air would suddenly reflect the prefix of the last gun fired. By setting InstancePerEntity => true, tModLoader creates a new, dedicated ElementalGlobalProjectile object for every projectile that spawns. This means each bullet gets its own firedWeaponPrefixID field, allowing it to store its unique "snapshot" of the weapon's prefix without affecting other bullets.

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Only apply this logic to specific projectiles (e.g., ChlorophyteBullet)
            // that were fired by your turret (indicated by a non-zero prefix in ai[0]).
            // projectile.ai[0] is where the turret passed the prefix.
            if (projectile.type == ProjectileID.ChlorophyteBullet && projectile.localAI[0] != 0)
            {
                int prefix = (int)projectile.localAI[0]; // Retrieve the prefix

                float elementalMultiplier = 0.4f; 
                float elementalChance = 100f;
                int buffTime = 180;

                if (ElementalProjectile.SetElementalChance(elementalChance))
                {
                    Player player = Main.player[projectile.owner];
                    short elementalProjectileType = ElementalProjectile.WhatElementDoICreate(prefix);
                    int buffType = ElementalProjectile.WhatBuffDoICreate(elementalProjectileType);

                    ElementalProjectile.SetElementOnNPC(target, hit, elementalMultiplier, player, elementalProjectileType, buffType, buffTime);
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (projectile.type == ProjectileID.ChlorophyteBullet && projectile.localAI[0] != 0)
            {
                int prefix = (int)projectile.localAI[0];

                float elementalMultiplier = 0.4f;
                float elementalChance = 100f;
                int buffTime = 180;

                if (ElementalProjectile.SetElementalChance(elementalChance))
                {
                    Player player = Main.player[projectile.owner];
                    short elementalProjectileType = ElementalProjectile.WhatElementDoICreate(prefix);
                    int buffType = ElementalProjectile.WhatBuffDoICreate(elementalProjectileType);

                    ElementalProjectile.SetElementOnPlayer(target, info, elementalMultiplier, player, elementalProjectileType, buffType, buffTime);
                }
            }
        }
    }
}