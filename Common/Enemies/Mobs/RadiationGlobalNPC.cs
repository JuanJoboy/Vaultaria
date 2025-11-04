using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;

public class RadiationGlobalNPC : GlobalNPC
{
    // Must be true so that every NPC has a unique instance
    public override bool InstancePerEntity => true;
    public NPC.HitInfo globalHit;

    public override void HitEffect(NPC npc, NPC.HitInfo hit)
    {
        base.HitEffect(npc, hit);

        if(npc.life <= 0)
        {
            globalHit = hit;
        }
    }

    public override bool CheckDead(NPC npc)
    {
        if (npc.HasBuff(ElementalID.RadiationBuff))
        {
            ElementalProjectile.SetRadiation(npc, globalHit, 1000, ElementalID.RadiationExplosion, ElementalID.RadiationBuff, 60);
            IrradiateNearbyNPCs(npc);
        }

        return base.CheckDead(npc);
    }

    private void IrradiateNearbyNPCs(NPC explodingNPC)
    {
        // Loops through every NPC in the world
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            float dist = Vector2.Distance(explodingNPC.Center, npc.Center); // Measures the distance from the exploding npc to other npc's
            
            if (!npc.townNPC) // Filters to only hostile targets
            {
                if(dist < 500)
                {
                    ElementalProjectile.SetRadiation(npc, globalHit, 1000, ElementalID.RadiationProjectile, ElementalID.RadiationBuff, 60); // Set radiation again on nearby npc's
                }
            }
        }
    }
}