using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;

public class RadiationGlobalNPC : GlobalNPC
{
    public override void HitEffect(NPC npc, NPC.HitInfo hit)
    {
        base.HitEffect(npc, hit);

        if(npc.life <= 2 && npc.HasBuff(ElementalID.RadiationBuff))
        {
            ElementalProjectile.SetRadiation(npc, hit, 1, ProjectileID.DD2ExplosiveTrapT3Explosion, ElementalID.RadiationBuff, 60);
            IrradiateNearbyNPCs(npc, hit);
        }
    }

    private void IrradiateNearbyNPCs(NPC explodingNPC, NPC.HitInfo hit)
    {
        // Loops through every NPC in the world
        for (int i = 0; i < Main.maxNPCs; i++)
        {
            NPC npc = Main.npc[i];
            float dist = Vector2.Distance(explodingNPC.Center, npc.Center); // Measures the distance from the exploding npc to other npc's
            
            if (!npc.townNPC) // Filters to only hostile targets
            {
                if(dist < 200)
                {
                    ElementalProjectile.SetRadiation(npc, hit, 0.2f, ElementalID.RadiationProjectile, ElementalID.RadiationBuff, 60); // Set radiation again on nearby npc's
                }
            }
        }
    }
}