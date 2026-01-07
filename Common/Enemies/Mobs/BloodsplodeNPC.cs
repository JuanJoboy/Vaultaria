using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;
using Vaultaria.Content.Items.Accessories.Skills;
using Vaultaria.Content.Buffs.SkillEffects;

public class BloodsplodeNPC : GlobalNPC
{
    public override void HitEffect(NPC npc, NPC.HitInfo hit)
    {
        base.HitEffect(npc, hit);

        int playerWhoKilledNPC = npc.lastInteraction;
        Player player = Main.player[playerWhoKilledNPC];

        int incendiary = ModContent.BuffType<IncendiaryBuff>();
        int shock = ModContent.BuffType<ShockBuff>();
        int corrosive = ModContent.BuffType<CorrosiveBuff>();
        int explosive = ModContent.BuffType<ExplosiveBuff>();
        int slag = ModContent.BuffType<SlagBuff>();
        int cryo = ModContent.BuffType<CryoBuff>();
        int radiation = ModContent.BuffType<RadiationBuff>();

        if(Utilities.IsWearing(player, ModContent.ItemType<Bloodsplosion>()))
        {
            if(npc.HasBuff(incendiary))
            {
                BloodSplosion(npc, hit, incendiary);
            }
            if(npc.HasBuff(shock))
            {
                BloodSplosion(npc, hit, shock);
            }
            if(npc.HasBuff(corrosive))
            {
                BloodSplosion(npc, hit, corrosive);
            }
            if(npc.HasBuff(explosive))
            {
                BloodSplosion(npc, hit, explosive);
            }
            if(npc.HasBuff(slag))
            {
                BloodSplosion(npc, hit, slag);
            }
            if(npc.HasBuff(cryo))
            {
                BloodSplosion(npc, hit, cryo);
            }
            if(npc.HasBuff(radiation))
            {
                BloodSplosion(npc, hit, radiation);
            }
            
            if(!npc.HasBuff(incendiary) && !npc.HasBuff(shock) &&!npc.HasBuff(corrosive) &&!npc.HasBuff(slag) &&!npc.HasBuff(cryo) &&!npc.HasBuff(radiation))
            {
                BloodSplosion(npc, hit, explosive);
            }
        }
    }

    private void BloodSplosion(NPC npc, NPC.HitInfo hit, int buff)
    {
        float multiplier = hit.DamageType == DamageClass.Melee ? 2 : 1;

        if(npc.life <= 2)
        {
            ElementalProjectile.BloodSplode(npc, hit, 1 * multiplier, (short) ElementalProjectile.WhatSplosionDoICreate(buff), buff, 240);
            ElementalProjectile.BloodSplodeNearbyNPCs(npc, hit, (short) ElementalProjectile.WhatSplosionDoICreate(buff), buff, 0.4f * multiplier);
        }
    }
}