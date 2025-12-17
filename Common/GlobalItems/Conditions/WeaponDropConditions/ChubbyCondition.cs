using Terraria;
using Terraria.GameContent.ItemDropRules;
using Vaultaria.Common.Systems.GenPasses.Vaults;

public class ChubbyCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        // FIX 1: Check the specific NPC instance for the TubbyNPC global data.
        // info.npc is the specific NPC that died.
        // TryGetGlobalNPC retrieves the *instance* of TubbyNPC associated with that NPC.
        if (info.npc.TryGetGlobalNPC(out TubbyNPC tubbyNpc))
        {
            return tubbyNpc.isChubby;
        }

        return false;
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Dropped from Chubbies";

    public bool CanShowItemDropInUI() => true;
}