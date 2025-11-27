using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Vaultaria.Content.NPCs.Town.Claptrap;

public class LaserDiskerCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        if(info.npc.type == ModContent.NPCType<Claptrap>())
        {
            if(info.npc.IsShimmerVariant && NPC.downedFishron)
            {
                return true;
            }
        }
        
        return false;
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Dropped from Claptrap if he's shimmered, and if Duke Fishron has been defeated";

    public bool CanShowItemDropInUI() => true;
}