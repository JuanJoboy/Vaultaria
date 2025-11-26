using Terraria;
using Terraria.GameContent.ItemDropRules;

public class ShockraCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.player.ZoneSkyHeight || info.player.ZoneRain || Main.invasionType == 4 || Main.raining;
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops in Space, during the Rain or during a Martian Invasion.";

    public bool CanShowItemDropInUI() => true;
}