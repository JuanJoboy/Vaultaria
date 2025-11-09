using Terraria;
using Terraria.GameContent.ItemDropRules;

public class CorruptedSpiritCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return Main.hardMode && info.player.ZoneDungeon;
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops in the Hardmode Dungeon.";

    public bool CanShowItemDropInUI() => true;
}