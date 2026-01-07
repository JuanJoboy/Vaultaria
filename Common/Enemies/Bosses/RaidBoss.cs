using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Vaultaria.Common.Systems.GenPasses.Vaults;

public class RaidBoss : GlobalNPC
{
    public override void ModifyTypeName(NPC npc, ref string typeName)
    {
        base.ModifyTypeName(npc, ref typeName);

        if(SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>())
        {
            if(npc.boss || npc.type == NPCID.Pumpking || npc.type == NPCID.IceQueen)
            {
                typeName = $"{typeName} The Invincible";
            }
        }
    }
}