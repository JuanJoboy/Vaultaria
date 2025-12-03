using Terraria;
using Terraria.GameContent.ItemDropRules;

public class VaultCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return SubworldLibrary.SubworldSystem.AnyActive();
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops inside the Vaults";

    public bool CanShowItemDropInUI() => true;
}