using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Vaultaria.Content.Buffs.GunEffects;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using System.Diagnostics.CodeAnalysis;

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
        if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
        {
            npc.AddBuff(BuffID.Ichor, 300);
            modifiers.SourceDamage *= 1.3f;
            modifiers.Defense *= 0.7f;
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