using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Vaultaria.Content.Buffs.Prefixes.Elements;
using Terraria.ID;
using Vaultaria.Common.Utilities;
using Microsoft.Xna.Framework.Graphics;

public class RadiationGlobalNPC : GlobalNPC
{
    public override void HitEffect(NPC npc, NPC.HitInfo hit)
    {
        base.HitEffect(npc, hit);

        if(npc.life <= 2 && npc.HasBuff(ElementalID.RadiationBuff))
        {
            ElementalProjectile.BloodSplode(npc, hit, 1, ElementalID.RadiationExplosion, ElementalID.RadiationBuff, 240);
            ElementalProjectile.BloodSplodeNearbyNPCs(npc, hit, ElementalID.RadiationProjectile, ElementalID.RadiationBuff);
        }
    }
}