using Terraria;
using Terraria.GameContent.ItemDropRules;
using Vaultaria.Common.Systems.GenPasses.Vaults;

public class VaultCondition : IItemDropRuleCondition, IProvideItemConditionDescription
{
    // CanDrop runs right before the item is dropped.
    public bool CanDrop(DropAttemptInfo info)
    {
        return SubworldLibrary.SubworldSystem.IsActive<Vault1Subworld>() || SubworldLibrary.SubworldSystem.IsActive<Vault2Subworld>();
    }

    // This describes the condition for the Bestiary/Recipe Browser.
    public string GetConditionDescription() => "Drops inside the Vaults";

    public bool CanShowItemDropInUI() => true;
}