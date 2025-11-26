using Terraria;
using Terraria.GameContent.ItemDropRules;

public class NuclearArmsCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return Main.hardMode && (info.player.ZoneCorrupt || info.player.ZoneCrimson);
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops in Hardmode Corruption / Crimson.";

    public bool CanShowItemDropInUI() => true;
}