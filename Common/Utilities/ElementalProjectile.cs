using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Globals;
using Terraria.DataStructures;
using System;

namespace Vaultaria.Common.Utilities
{
    public abstract class ElementalProjectile : ModProjectile
    {
        // ********************************************
        // *------------- Helper Fields -------------*
        // ********************************************

        public static readonly HashSet<int> elementalProjectile = new HashSet<int>
        {
            ElementalID.ShockProjectile,
            ElementalID.SlagProjectile,
            ElementalID.CorrosiveProjectile,
            ElementalID.ExplosiveProjectile,
            ElementalID.IncendiaryProjectile,
            ElementalID.CryoProjectile,
            ElementalID.RadiationProjectile,
            ElementalID.RadiationExplosion,
        };

        public static readonly HashSet<int> elementalPrefix = new HashSet<int>
        {
            ElementalID.ShockPrefix,
            ElementalID.SlagPrefix,
            ElementalID.CorrosivePrefix,
            ElementalID.ExplosivePrefix,
            ElementalID.IncendiaryPrefix,
            ElementalID.CryoPrefix,
            ElementalID.RadiationPrefix,
        };
        
        public static readonly HashSet<int> elementalBuff = new HashSet<int>
        {
            ElementalID.ShockBuff,
            ElementalID.SlagBuff,
            ElementalID.CorrosiveBuff,
            ElementalID.ExplosiveBuff,
            ElementalID.IncendiaryBuff,
            ElementalID.CryoBuff,
            ElementalID.RadiationBuff,
        };

        public static readonly Dictionary<int, short> prefixToProjectile = new Dictionary<int, short>
        {
            {ElementalID.ShockPrefix, ElementalID.ShockProjectile},
            {ElementalID.SlagPrefix, ElementalID.SlagProjectile},
            {ElementalID.CorrosivePrefix, ElementalID.CorrosiveProjectile},
            {ElementalID.ExplosivePrefix, ElementalID.ExplosiveProjectile},
            {ElementalID.IncendiaryPrefix, ElementalID.IncendiaryProjectile},
            {ElementalID.CryoPrefix, ElementalID.CryoProjectile},
            {ElementalID.RadiationPrefix, ElementalID.RadiationProjectile}
        };

        public static readonly Dictionary<int, int> ProjectileToBuff = new Dictionary<int, int>
        {
            {ElementalID.ShockProjectile, ElementalID.ShockBuff},
            {ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff},
            {ElementalID.CorrosiveProjectile, ElementalID.CorrosiveBuff},
            {ElementalID.SlagProjectile, ElementalID.SlagBuff},
            {ElementalID.ExplosiveProjectile, ElementalID.ExplosiveBuff},
            {ElementalID.CryoProjectile, ElementalID.CryoBuff},
            {ElementalID.RadiationProjectile, ElementalID.RadiationBuff}
        };

        // ********************************************
        // *------------- Helper Methods -------------*
        // ********************************************

        /// <summary>
        /// Determines if an elemental effect should proc based on a given chance.
        /// <br/> To use chance, put in a float from 1 - 100. So if you put in 23.5, there would be a 23.5% elemental chance.
        /// </summary>
        /// <param name="chance"></param>
        /// <returns>True if the elemental effect procs, and false otherwise.</returns>
        public static bool SetElementalChance(float chance)
        {
            if (Main.rand.Next(1, 101) <= chance)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Calculates the integer elemental damage based on a base damage value and a multiplier.
        /// <br/> baseDamage = The base damage value to be multiplied (e.g., the projectile's damage).
        /// <br/> multiplier = The multiplier to apply to the base damage (e.g., 0.4 for 40% damage).
        /// <br/> elementalDamage = The calculated elemental damage that's outputted as an integer.
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="multiplier"></param>
        /// <param name="elementalDamage"></param>
        private static void SetElementalDamage(float baseDamage, float multiplier, out int elementalDamage)
        {
            elementalDamage = (int)(baseDamage * multiplier);
        }

        /// <summary>
        /// Returns a list of elemental types natively supported by this projectile.
        /// This helps avoid duplicate elemental procs from global effects.
        /// </summary>
        public virtual List<string> GetElement()
        {
            return new List<string>(); // Default: no elements
        }

        /// <summary>
        /// Maps an elemental projectile ID to its corresponding elemental buff ID.
        /// </summary>
        /// <param name="elementalProjectile"></param>
        /// <returns>The corresponding buff ID, or 0 if no match is found.</returns>
        public static int WhatBuffDoICreate(int elementalProjectile)
        {
            if (ProjectileToBuff.TryGetValue(elementalProjectile, out int buffType))
            {
                return buffType;
            }

            return 0;
        }

        // ******************************************************
        // *------------- Setter Elemental Methods -------------*
        // ******************************************************

        /// <summary>
        /// On proc, spawns an elemental projectile (eg. Electrosphere) that deals elemental damage to an NPC.
        /// <br/> target = The NPC that was hit.
        /// <br/> hit = The NPC.HitInfo containing details about the hit, including source damage modifiers.
        /// <br/> elementalMultiplier = The specific damage multiplier for this Shock effect.
        /// <br/> player = The player shooting the elemental projectiles.
        /// <br/> elementalProjectile = The new projectile that deals the elemental damage.
        /// <br/> buffType = The additional base buff that's added on top.
        /// <br/> buffTime = The amount of time the base buff will last for. It's calculated in ticks so 60 ticks is 1 second.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="hit"></param>
        /// <param name="elementalMultiplier"></param>
        /// <param name="player"></param>
        /// <param name="elementalProjectile"></param>
        /// <param name="buffType"></param>
        /// <param name="buffTime"></param>
        public static void SetElementOnNPC(NPC target, NPC.HitInfo hit, float elementalMultiplier, Player player, short elementalProjectile, int buffType, int buffTime)
        {
            int elementalDamage = 0;
            float baseDamage = hit.SourceDamage;

            SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

            Projectile.NewProjectile(
                player.GetSource_OnHit(target),
                target.Center,
                Vector2.Zero,
                elementalProjectile,
                elementalDamage,
                0f,
                player.whoAmI
            );

            // A complete freeze only has a 20% chance to happen after the initial 40% chance of the element being produced.
            if (WhatBuffDoICreate(elementalProjectile) == ElementalID.CryoBuff)
            {
                if (SetElementalChance(20))
                {
                    target.AddBuff(buffType, buffTime);
                }
            }
            else
            {
                target.AddBuff(buffType, buffTime);
            }
        }

        /// <summary>
        /// On proc, spawns an elemental projectile (eg. Electrosphere) that deals elemental damage to an NPC.
        /// <br/> target = The Player that was hit.
        /// <br/> info = The Player.HurtInfo containing details about the hit, including source damage modifiers.
        /// <br/> elementalMultiplier = The specific damage multiplier for this Shock effect.
        /// <br/> player = The player shooting the elemental projectiles.
        /// <br/> elementalProjectile = The new projectile that deals the elemental damage.
        /// <br/> buffType = The additional base buff that's added on top.
        /// <br/> buffTime = The amount of time the base buff will last for. It's calculated in ticks so 60 ticks is 1 second.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="info"></param>
        /// <param name="elementalMultiplier"></param>
        /// <param name="player"></param>
        /// <param name="elementalProjectile"></param>
        /// <param name="buffType"></param>
        /// <param name="buffTime"></param>
        public static void SetElementOnPlayer(Player target, Player.HurtInfo info, float elementalMultiplier, Player player, short elementalProjectile, int buffType, int buffTime)
        {
            int elementalDamage = 0;
            float baseDamage = info.SourceDamage;

            SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

            Projectile.NewProjectile(
                player.GetSource_OnHit(target),
                target.Center,
                Vector2.Zero,
                elementalProjectile,
                elementalDamage,
                0f,
                player.whoAmI
            );

            if (WhatBuffDoICreate(elementalProjectile) == ElementalID.CryoBuff)
            {
                if (SetElementalChance(20))
                {
                    target.AddBuff(buffType, buffTime);
                }
            }
            else
            {
                target.AddBuff(buffType, buffTime);
            }
        }

        /// <summary>
        /// On proc, spawns an elemental projectile (eg. Electrosphere) that deals elemental damage to an NPC.
        /// <br/> target = The Player that was hit.
        /// <br/> info = The Player.HurtInfo containing details about the hit, including source damage modifiers.
        /// <br/> elementalMultiplier = The specific damage multiplier for this Shock effect.
        /// <br/> player = The player shooting the elemental projectiles.
        /// <br/> elementalProjectile = The new projectile that deals the elemental damage.
        /// <br/> buffType = The additional base buff that's added on top.
        /// <br/> buffTime = The amount of time the base buff will last for. It's calculated in ticks so 60 ticks is 1 second.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="info"></param>
        /// <param name="elementalMultiplier"></param>
        /// <param name="player"></param>
        /// <param name="elementalProjectile"></param>
        /// <param name="buffType"></param>
        /// <param name="buffTime"></param>
        public static bool SetElementOnTile(Projectile projectile, float elementalMultiplier, Player player, short elementalProjectile)
        {
            int elementalDamage = 0;
            float baseDamage = projectile.damage;

            SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

            Projectile.NewProjectile(
                projectile.GetSource_FromThis(),
                projectile.Center,
                Vector2.Zero,
                elementalProjectile,
                elementalDamage,
                0f,
                player.whoAmI
            );

            projectile.Kill();
            return false;
        }

        /// <summary>
        /// This method is needed to fill in the gaps. Non-Vaultarian items that are projectile-based don't work with the ElementalGlobal files, so this method allows them to work. It does pretty much the same thing as the standard chain used in those files, but it's just cut down a tiny bit. It checks the prefix, gets the snapshot, sees if its the correct prefix, then sets the element.
        /// <br/> projectile = the projectile being shot.
        /// <br/> player = The player that shoots the projectile.
        /// <br/> target = The NPC that was hit.
        /// <br/> hit = The NPC.HitInfo containing details about the hit, including source damage modifiers.
        /// <br/> elementalChance = The chance for the element to spawn.
        /// <br/> elementalMultiplier = The specific damage multiplier.
        /// <br/> elementalPrefix = The desired prefix to spawn the element.
        /// <br/> elementalProjectile = The new projectile that deals the elemental damage.
        /// <br/> elementalBuff = The additional base buff that's added on top.
        /// <br/> elementalBuffTime = The amount of time the base buff will last for. It's calculated in ticks so 60 ticks is 1 second.
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="player"></param>
        /// <param name="target"></param>
        /// <param name="hit"></param>
        /// <param name="elementalChance"></param>
        /// <param name="elementalMultiplier"></param>
        /// <param name="elementalPrefix"></param>
        /// <param name="elementalProjectile"></param>
        /// <param name="elementalBuff"></param>
        /// <param name="elementalBuffTime"></param>
        public static void HandleElementalProjOnNPC(Projectile projectile, Player player, NPC target, NPC.HitInfo hit, float elementalChance, float elementalMultiplier, int elementalPrefix, short elementalProjectile, int elementalBuff, int elementalBuffTime)
        {
            int prefixID = projectile.GetGlobalProjectile<ElementalGlobalProjectile>().firedWeaponPrefixID;

            if (prefixID == elementalPrefix)
            {
                if (SetElementalChance(elementalChance))
                {
                    SetElementOnNPC(target, hit, elementalMultiplier, player, elementalProjectile, elementalBuff, elementalBuffTime);
                }
            }
        }

        /// <summary>
        /// Spawns Radiation on the NPC that was targeted
        /// <br/> target = The NPC that was hit.
        /// <br/> hit = The NPC.HitInfo containing details about the hit, including source damage modifiers.
        /// <br/> elementalMultiplier = The specific damage multiplier for the Radiation effect.
        /// <br/> elementalProjectile = The new projectile that deals the elemental damage.
        /// <br/> buffType = The additional base buff that's added on top.
        /// <br/> buffTime = The amount of time the base buff will last for. It's calculated in ticks so 60 ticks is 1 second.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="hit"></param>
        /// <param name="elementalMultiplier"></param>
        /// <param name="elementalProjectile"></param>
        /// <param name="buffType"></param>
        /// <param name="buffTime"></param>
        public static void SetRadiation(NPC target, NPC.HitInfo hit, float elementalMultiplier, short elementalProjectile, int buffType, int buffTime)
        {
            int elementalDamage = 0;
            float baseDamage = hit.SourceDamage;

            SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

            Projectile.NewProjectile(
                target.GetSource_OnHit(target),
                target.Center,
                Vector2.Zero,
                elementalProjectile,
                elementalDamage,
                0f,
                target.whoAmI
            );

            target.AddBuff(buffType, buffTime);
        }
    }
}