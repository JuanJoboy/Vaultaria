using Terraria;
using Terraria.GameContent.ItemDropRules;

public class ColdHeartedCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return Main.hardMode && (info.player.ZoneSnow || Main.invasionType == 2 || Main.snowMoon);
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops in Hardmode in either the Snow Biome or during the Frost Moon or Frost Legion.";

    public bool CanShowItemDropInUI() => true;
}