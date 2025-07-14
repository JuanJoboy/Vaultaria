using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using System.Collections.Generic; // For Lists

public abstract class ElementalProjectile : ModProjectile
{
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
    public virtual List<string> getElement()
    {
        return new List<string>(); // Default: no elements
    }

    /// <summary>
    /// On proc, spawns an Electrosphere projectile dealing shock elemental damage to an NPC.
    /// <br/> target = The NPC that was hit.
    /// <br/> hit = The NPC.HitInfo containing details about the hit, including source damage modifiers.
    /// <br/> elementalMultiplier = The specific damage multiplier for this Shock effect.
    /// <br/> buffTime = The amount of time the base Shock buff will last for. It's calculated in ticks so 60 ticks is 1 second.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="hit"></param>
    /// <param name="elementalMultiplier"></param>
    /// <param name="buffTime"></param>
    public void SetShockOnNPC(NPC target, NPC.HitInfo hit, float elementalMultiplier, int buffTime)
    {
        Player player = Main.player[Projectile.owner];
        int elementalDamage = 0;
        float baseDamage = hit.SourceDamage;

        SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

        Projectile.NewProjectile(
            player.GetSource_OnHit(target),
            target.Center,
            Vector2.Zero,
            ProjectileID.Electrosphere,
            elementalDamage,
            0f,
            player.whoAmI
        );

        target.AddBuff(ModContent.BuffType<ShockBuff>(), buffTime);
    }

    public static void SetShockOnNPC(NPC target, NPC.HitInfo hit, float elementalMultiplier, int buffTime, Player player)
    {
        int elementalDamage = 0;
        float baseDamage = hit.SourceDamage;

        SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

        Projectile.NewProjectile(
            player.GetSource_OnHit(target),
            target.Center,
            Vector2.Zero,
            ProjectileID.Electrosphere,
            elementalDamage,
            0f,
            player.whoAmI
        );

        target.AddBuff(ModContent.BuffType<ShockBuff>(), buffTime);
    }

    /// <summary>
    /// On proc, spawns an Electrosphere projectile dealing shock elemental damage to an NPC.
    /// <br/> target = The NPC that was hit.
    /// <br/> info = The Player.HurtInfo containing details about the hit, including source damage modifiers.
    /// <br/> elementalMultiplier = The specific damage multiplier for this Shock effect.
    /// <br/> buffTime = The amount of time the base Shock buff will last for. It's calculated in ticks so 60 ticks is 1 second.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="info"></param>
    /// <param name="elementalMultiplier"></param>
    /// <param name="buffTime"></param>
    public void SetShockOnPlayer(Player target, Player.HurtInfo info, float elementalMultiplier, int buffTime)
    {
        Player player = Main.player[Projectile.owner];
        int elementalDamage = 0;
        float baseDamage = info.SourceDamage;

        SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

        Projectile.NewProjectile(
            player.GetSource_OnHit(target),
            target.Center,
            Vector2.Zero,
            ProjectileID.Electrosphere,
            elementalDamage,
            0f,
            player.whoAmI
        );

        target.AddBuff(ModContent.BuffType<ShockBuff>(), buffTime);
    }

    public void SetSlagOnNPC(NPC target, NPC.HitInfo hit, float elementalMultiplier, int buffTime)
    {
        Player player = Main.player[Projectile.owner];
        int elementalDamage = 0;
        float baseDamage = hit.SourceDamage;

        SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

        Projectile.NewProjectile(
            player.GetSource_OnHit(target),
            target.Center,
            Vector2.Zero,
            ProjectileID.ShadowFlame,
            elementalDamage,
            0f,
            player.whoAmI
        );

        target.AddBuff(ModContent.BuffType<SlagBuff>(), buffTime);
    }

    public void SetSlagOnPlayer(Player target, Player.HurtInfo info, float elementalMultiplier, int buffTime)
    {
        Player player = Main.player[Projectile.owner];
        int elementalDamage = 0;
        float baseDamage = info.SourceDamage;

        SetElementalDamage(baseDamage, elementalMultiplier, out elementalDamage);

        Projectile.NewProjectile(
            player.GetSource_OnHit(target),
            target.Center,
            Vector2.Zero,
            ProjectileID.ShadowFlame,
            elementalDamage,
            0f,
            player.whoAmI
        );

        target.AddBuff(ModContent.BuffType<SlagBuff>(), buffTime);
    }
}