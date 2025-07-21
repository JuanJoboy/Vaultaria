using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Vaultaria.Common.Globals.Prefixes.Elements;
using Terraria.DataStructures;
using System;

namespace Vaultaria.Common.Utilities
{
    public abstract class ElementalProjectile : ModProjectile
    {
        // ********************************************
        // *------------- Helper Fields -------------*
        // ********************************************

        private static readonly HashSet<int> elementalProjectile = new HashSet<int>
        {
            ElementalID.ShockProjectile,
            ElementalID.SlagProjectile,
            ElementalID.CorrosiveProjectile,
            ElementalID.ExplosiveProjectile,
            ElementalID.IncendiaryProjectile,
            ElementalID.CryoProjectile,
        };

        public static readonly HashSet<int> elementalPrefix = new HashSet<int>
        {
            ElementalID.ShockPrefix,
            ElementalID.SlagPrefix,
            ElementalID.CorrosivePrefix,
            ElementalID.ExplosivePrefix,
            ElementalID.IncendiaryPrefix,
            ElementalID.CryoPrefix,
        };

        public static readonly HashSet<int> elementalBuff = new HashSet<int>
        {
            ElementalID.ShockBuff,
            ElementalID.SlagBuff,
            ElementalID.CorrosiveBuff,
            ElementalID.ExplosiveBuff,
            ElementalID.IncendiaryBuff,
            ElementalID.CryoBuff,
        };

        public static readonly Dictionary<int, short> prefixToProjectile = new Dictionary<int, short>
        {
            {ElementalID.ShockPrefix, ElementalID.ShockProjectile},
            {ElementalID.SlagPrefix, ElementalID.SlagProjectile},
            {ElementalID.CorrosivePrefix, ElementalID.CorrosiveProjectile},
            {ElementalID.ExplosivePrefix, ElementalID.ExplosiveProjectile},
            {ElementalID.IncendiaryPrefix, ElementalID.IncendiaryProjectile},
            {ElementalID.CryoPrefix, ElementalID.CryoProjectile}
        };

        public static readonly Dictionary<int, int> ProjectileToBuff = new Dictionary<int, int>
        {
            {ElementalID.ShockProjectile, ElementalID.ShockBuff},
            {ElementalID.IncendiaryProjectile, ElementalID.IncendiaryBuff},
            {ElementalID.CorrosiveProjectile, ElementalID.CorrosiveBuff},
            {ElementalID.SlagProjectile, ElementalID.SlagBuff},
            {ElementalID.ExplosiveProjectile, ElementalID.ExplosiveBuff},
            {ElementalID.CryoProjectile, ElementalID.CryoBuff}
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
        /// Performs an initial check for elemental projectile hits in GlobalProjectile hooks.
        /// Returns true if the processing should stop (e.g., not from player, self-proccing projectile).
        /// The out parameter allows the player to be initialized too.
        /// <br/> projectile = The projectile that hit.
        /// <br/> projectileToStop = The ProjectileID of elemental projectiles that should not self-proc (e.g., ProjectileID.Electrosphere).
        /// <br/> player = The player who owns the projectile (out parameter).
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="projectileToStop"></param>
        /// <param name="player"></param>
        /// <returns>True if the processing should stop, false otherwise.</returns>
        private static bool StopElementalClones(Projectile projectile, short projectileToStop, out Player player)
        {
            player = Main.player[projectile.owner];

            // Skip if the projectile isn't owned by a valid player
            if (projectile.owner < 0 || projectile.owner >= Main.maxPlayers)
            {
                return true;
            }

            // Prevent recursive proc from projectile
            if (projectile.type == projectileToStop)
            {
                return true;
            }

            // Goes through the HashSet and checks if any of them are present and returns true
            if (elementalProjectile.Contains(projectile.type))
            {
                return true;
            }

            // If the projectile was summoned by a sentry, pet, NPC, or other source, skip
            // projectile.friendly = false = allows friendly NPC's and players to do elemental damage
            // projectile.hostile = true = doesn't allow hostile NPC's to do elemental damage
            // projectile.trap = true = doesn't allow hostile traps to do elemental damage
            if (!projectile.friendly || projectile.hostile || projectile.trap)
            {
                return true;
            }

            // // If it's a minion/sentry/summon (not a held weapon), skip
            // if (projectile.minion || projectile.sentry)
            // {
            //     return true;
            // }

            return false;
        }

        /// <summary>
        /// Maps an elemental prefix ID to its corresponding elemental projectile ID.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns>The corresponding elemental projectile ID, or 0 if no match is found.</returns>
        public static short WhatElementDoICreate(int prefix)
        {
            // Use TryGetValue for safe lookup. It avoids errors if the key isn't found.
            if (prefixToProjectile.TryGetValue(prefix, out short projectileType))
            {
                return projectileType;
            }

            return 0;
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

        /// <summary>
        /// Takes the parameters of an item's Shoot() method along with it's prefix (Item.prefix) to snapshot what element the item is truly meant to be. This ensures that the element isn't being checked by what's currently being held, as that can lead to unwanted element swapping.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="source"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="type"></param>
        /// <param name="damage"></param>
        /// <param name="knockback"></param>
        /// <param name="prefix"></param>
        public static void ElementalPrefixCorrector(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback, int prefix)
        {
            int projectileIndex = Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
            Projectile projectile = Main.projectile[projectileIndex];
            projectile.GetGlobalProjectile<ElementalGlobalProjectile>().firedWeaponPrefixID = prefix;
        }

        /// <summary>
        /// Checks what prefix the item has, and if it equals the elementalPrefix parameter.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="elementalPrefix"></param>
        /// <returns>True if the item's prefix equals the elementalPrefix</returns>
        public static bool PrefixIs(Item item, int elementalPrefix)
        {
            if (item.prefix == elementalPrefix)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks that it's not a self-triggered clone and that the held item has the expected prefix.
        /// </summary>
        /// <param name="chance"></param>
        /// <param name="elementalProjectile"></param>
        /// <param name="player"></param>
        /// <param name="elementalPrefix"></param>
        /// <returns>True if the projectile is allowed to trigger its elemental effect.</returns>
        public static bool AbleToProc(Projectile projectile, short elementalProjectile, out Player player, int elementalPrefix)
        {
            // First, perform general checks to determine if processing for elemental effects should stop.
            // This includes checks for invalid owners, recursive procs, existing elemental types, etc.
            if (!StopElementalClones(projectile, elementalProjectile, out player))
            {
                // If the projectile is a vanilla bullet or a modded bullet, access the 'firedWeaponPrefixID' stored on its attached ElementalGlobalProjectile instance. This makes the system compatible with any projectile type.
                int prefixID = projectile.GetGlobalProjectile<ElementalGlobalProjectile>().firedWeaponPrefixID;

                // Now, compare the 'snapped' prefixID (from the weapon that fired it) with the 'elementalPrefix' that this particular elemental proc check is for.
                // If they match, it means this projectile (with its specific originating prefix) is allowed to trigger the associated elemental effect.
                if (prefixID == elementalPrefix)
                {
                    return true;
                }
            }

            // If any of the 'StopElementalClones' conditions were met, or if the prefix didn't match, prevent the elemental effect from proccing.
            return false;
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
    }
}