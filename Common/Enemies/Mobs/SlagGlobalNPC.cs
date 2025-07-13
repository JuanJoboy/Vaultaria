using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using System.Diagnostics.CodeAnalysis;
using Vaultaria.Content.Prefixes.Weapons;
using System.Configuration;
using Vaultaria.Content.Projectiles.Ammo.Rare.SMG.Maliwan;

public class SlagGlobalNPC : GlobalNPC
{
    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
    {
        if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
        {
            npc.AddBuff(BuffID.Ichor, 300);
            modifiers.SourceDamage *= 1.3f;
            modifiers.Defense *= 0.7f;
        }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        Player player = Main.player[projectile.owner];
        int elementalDamage = 0;
        float finalDamage = modifiers.FinalDamage.ApplyTo(projectile.damage);

        if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
        {
            npc.AddBuff(BuffID.Ichor, 300);
            modifiers.SourceDamage *= 1.3f;
            modifiers.Defense *= 0.7f;
        }

        if (npc.HasBuff(ModContent.BuffType<ShockBuff>()))
        {
            if (projectile.ModProjectile is FlorentineBullet florentineBullet)
            {
                ElementalProjectile.SetElementalDamage(finalDamage, florentineBullet.elementalMultiplier, out elementalDamage);
            }

            Projectile.NewProjectile(
                player.GetSource_OnHit(npc),
                npc.Center,
                Vector2.Zero,
                ProjectileID.Electrosphere,
                elementalDamage,
                0f,
                player.whoAmI
            );
        }
    }

    public override void PostAI(NPC npc)
    {
        // Main.npc[npc.whoAmI].GivenName or npc.TypeName might be more user-friendly.
        // npc.FullName is the internal name.
        string npcName = npc.TypeName; // Or npc.GivenName if you want to check custom names

        // Check if the name contains "Slime" (case-insensitive for robustness)
        bool isSlime = npcName.Contains("Slime", System.StringComparison.OrdinalIgnoreCase);

        // --- Apply color logic based on SlagBuff and slime status ---
        if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
        {
            // If the NPC is slagged AND it's NOT a slime, then turn it purple.
            if (!isSlime)
            {
                npc.color = Color.Purple;
            }
        }
        else
        {
            if (!isSlime)
            {
                npc.color = Color.Transparent;
            }
        }
    }
}