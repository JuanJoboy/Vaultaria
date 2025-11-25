using Terraria;
using Terraria.GameContent.ItemDropRules;

public class CobraCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return NPC.downedMoonlord && info.player.ZoneDungeon;
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops in the Dungeon after defeating the Moonlord.";

    public bool CanShowItemDropInUI() => true;
}