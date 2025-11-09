using Terraria;
using Terraria.GameContent.ItemDropRules;

public class MindBlownCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return Main.hardMode && (Main.invasionType == 3 || info.player.ZoneOldOneArmy);
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops in Hardmode during a Pirate Invasion or during the Old Ones Army.";

    public bool CanShowItemDropInUI() => true;
}