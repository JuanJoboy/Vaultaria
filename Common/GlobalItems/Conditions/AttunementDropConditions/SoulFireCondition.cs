using Terraria;
using Terraria.GameContent.ItemDropRules;

public class SoulFireCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneUnderworldHeight;
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops in Hell.";

    public bool CanShowItemDropInUI() => true;
}