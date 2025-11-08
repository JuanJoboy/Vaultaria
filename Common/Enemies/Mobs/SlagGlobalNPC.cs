using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Common.Utilities;

public class SlagGlobalNPC : GlobalNPC
{
    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
    {
        if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
        {
            modifiers.SourceDamage *= 1.1f;
        }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
        {
            modifiers.SourceDamage *= 1.1f;
        }
    }

    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        base.DrawEffects(npc, ref drawColor);

        // Check if the name contains "Slime" (case-insensitive for robustness)
        bool isSlime = npc.TypeName.Contains("Slime", System.StringComparison.OrdinalIgnoreCase);

        // --- Apply color logic based on buff and slime status ---
        if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
        {
            npc.color = Color.Purple;
        }
        else
        {
            if (!isSlime)
            {
                npc.color = drawColor;
            }
        }
    }

    // public override void PostAI(NPC npc)
    // {
    //     // Main.npc[npc.whoAmI].GivenName or npc.TypeName might be more user-friendly.
    //     // npc.FullName is the internal name.
    //     string npcName = npc.TypeName; // Or npc.GivenName if you want to check custom names

    //     // Check if the name contains "Slime" (case-insensitive for robustness)
    //     bool isSlime = npcName.Contains("Slime", System.StringComparison.OrdinalIgnoreCase);

    //     // --- Apply color logic based on SlagBuff and slime status ---
    //     if (npc.HasBuff(ModContent.BuffType<SlagBuff>()))
    //     {
    //         // If the NPC is slagged AND it's NOT a slime, then turn it purple.
    //         if (!isSlime)
    //         {
    //             npc.color = Color.Purple;
    //         }
    //     }
    //     else
    //     {
    //         if (!isSlime)
    //         {
    //             npc.color = Color.Transparent;
    //         }
    //     }
    // }
}